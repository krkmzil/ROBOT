#include "../inc/MarlinConfig.h"

#if ENABLED(CONVEYOR_E1)

#include "conveyor.h"

#if NUM_SERVOS > 12
  #error "CONVEYOR_E1 uses Timer4, which the servo library also needs for more than 12 servos."
#endif

// Timer4 serbest sayma modunda, ön bölücü 8 => 2 MHz sayaç. Servo kütüphanesi bu kartta Timer3'ü
// (12'den fazla servoda Timer4'ü) kullanır ve sayacını sıfırladığı için Timer3 kullanılamaz. Timer4'ün
// COMPA kesmesi servo kütüphanesinde tanımlı olduğundan COMPB kullanılır; her kesmede OCR4B bir aralık
// ileri taşınır. Çıkış her yarım adımda (kenar değişiminde) tersine çevrilir.
static constexpr uint16_t MIN_RATE = 20;       // adım/sn (Timer4 16-bit sınırı nedeniyle alt sınır)
static constexpr uint16_t MAX_RATE = 8000;     // adım/sn
static constexpr uint8_t  RAMP_EVERY_STEPS = 8;
static constexpr uint16_t RAMP_DELTA = 40;     // her rampa adımında hız değişimi (adım/sn)

static volatile uint16_t cur_rate = 0;
static volatile uint16_t target_rate = 0;
static volatile uint16_t interval_ticks = 0;   // iki kesme arası sayaç adımı (yarım adım süresi)
static volatile bool running = false;
static volatile bool dir_forward = true;
static volatile bool step_level = false;
static volatile uint8_t step_count = 0;

static volatile bool limited = false;          // belirli adım sayısı kadar hareket
static volatile uint32_t remaining = 0;

static volatile bool restart_pending = false;  // yön değişimi: önce dur, sonra ters yönde başla
static volatile bool restart_forward = true;
static volatile uint16_t restart_rate = 0;

#define CONV_DRIVER(on) WRITE(E1_ENABLE_PIN, (on) ? (E_ENABLE_ON) : !(E_ENABLE_ON))
#define CONV_DIR(fwd)   WRITE(E1_DIR_PIN, (fwd) ^ bool(INVERT_E1_DIR))

static inline uint16_t ticks_for(const uint16_t rate) {
  return uint16_t(1000000UL / rate);
}

static inline void timer_start(const uint16_t rate) {
  TCCR4A = 0;
  TCCR4B = _BV(CS41);                // normal mod, /8
  interval_ticks = ticks_for(rate);
  OCR4B = TCNT4 + interval_ticks;
  TIFR4 = _BV(OCF4B);
  TIMSK4 |= _BV(OCIE4B);
}

static inline void timer_stop() {
  TIMSK4 &= ~_BV(OCIE4B);
  TCCR4B = 0;
}

static inline void stop_hardware() {
  timer_stop();
  WRITE(E1_STEP_PIN, LOW);
  step_level = false;
  cur_rate = 0;
  running = false;
  limited = false;
  remaining = 0;
  CONV_DRIVER(false);
}

// Kesmeler kapalıyken çağrılmalıdır.
static void start_locked(const bool forward, const uint16_t rate) {
  dir_forward = forward;
  CONV_DIR(forward);
  CONV_DRIVER(true);
  cur_rate = MIN_RATE;
  target_rate = rate;
  step_count = 0;
  step_level = false;
  restart_pending = false;
  running = true;
  timer_start(MIN_RATE);
}

static inline uint16_t clamp_rate(const uint16_t rate) {
  return rate < MIN_RATE ? MIN_RATE : (rate > MAX_RATE ? MAX_RATE : rate);
}

void conveyor_init() {
  SET_OUTPUT(E1_STEP_PIN);
  SET_OUTPUT(E1_DIR_PIN);
  SET_OUTPUT(E1_ENABLE_PIN);
  WRITE(E1_STEP_PIN, LOW);
  CONV_DRIVER(false);
}

void conveyor_set(const bool forward, uint16_t rate) {
  const uint8_t sreg = SREG;
  cli();

  limited = false;

  if (rate == 0) {
    // Kademeli dur: hedef 0, kesme rutini hız düşünce durdurur.
    restart_pending = false;
    target_rate = 0;
    if (!running) stop_hardware();
  }
  else {
    rate = clamp_rate(rate);

    if (!running)
      start_locked(forward, rate);
    else if (forward == dir_forward) {
      restart_pending = false;
      target_rate = rate;
    }
    else {
      // Çalışırken yön değişti: önce kademeli dur, durunca ters yönde başla.
      restart_pending = true;
      restart_forward = forward;
      restart_rate = rate;
      target_rate = 0;
    }
  }

  SREG = sreg;
}

void conveyor_move(const bool forward, uint16_t rate, const uint32_t steps) {
  if (steps == 0 || rate == 0) return;

  rate = clamp_rate(rate);

  const uint8_t sreg = SREG;
  cli();

  // Çalışırken ters yöne hareket istenirse önce anında durdurulur.
  if (running && forward != dir_forward) stop_hardware();

  if (!running)
    start_locked(forward, rate);
  else {
    restart_pending = false;
    target_rate = rate;
  }

  remaining = steps;
  limited = true;

  SREG = sreg;
}

void conveyor_stop() {
  const uint8_t sreg = SREG;
  cli();
  restart_pending = false;
  target_rate = 0;
  stop_hardware();
  SREG = sreg;
}

bool conveyor_running() { return running; }
uint16_t conveyor_rate() { return cur_rate; }

ISR(TIMER4_COMPB_vect) {
  OCR4B += interval_ticks;
  step_level = !step_level;
  WRITE(E1_STEP_PIN, step_level);

  if (step_level) return;           // sayma ve rampa adım başına bir kez (düşen kenar)

  if (limited) {
    if (remaining > 0) remaining--;
    if (remaining == 0) {           // istenen adım sayısı tamam
      stop_hardware();
      return;
    }
  }

  if (++step_count < RAMP_EVERY_STEPS) return;
  step_count = 0;

  uint16_t c = cur_rate;
  uint16_t t = target_rate;

  // Sınırlı harekette bitişe yaklaşırken yavaşla.
  if (limited && c > MIN_RATE && remaining <= uint32_t((c - MIN_RATE) / RAMP_DELTA + 1) * RAMP_EVERY_STEPS)
    t = MIN_RATE;

  if (t == 0 && c <= MIN_RATE + RAMP_DELTA) {
    if (restart_pending) {
      restart_pending = false;
      dir_forward = restart_forward;
      CONV_DIR(restart_forward);
      target_rate = restart_rate;
      c = MIN_RATE;
    }
    else {
      stop_hardware();
      return;
    }
  }
  else if (c < t)
    c = (t - c > RAMP_DELTA) ? c + RAMP_DELTA : t;
  else if (c > t)
    c = (c - t > RAMP_DELTA) ? c - RAMP_DELTA : t;

  if (c < MIN_RATE) c = MIN_RATE;
  cur_rate = c;
  interval_ticks = ticks_for(c);
}

#endif // CONVEYOR_E1

