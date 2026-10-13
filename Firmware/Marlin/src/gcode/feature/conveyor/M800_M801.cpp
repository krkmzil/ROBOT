#include "../../../inc/MarlinConfig.h"

#if ENABLED(CONVEYOR_E1)

#include "../../gcode.h"
#include "../../../feature/conveyor.h"
#include "../../../core/serial.h"

/**
 * M800: Konveyörü (E1 yuvasındaki motor) planlayıcıdan bağımsız çalıştırır.
 *
 *   S<adım/sn>  Hız. S0 = kademeli durdur.
 *   D<0|1>      Yön (1 = ileri, varsayılan; 0 = geri).
 *   P<adım>     Verilirse tam bu kadar adım ilerleyip durur; verilmezse sürekli çalışır.
 *
 * Parametresiz M800 durumu bildirir.
 */
void GcodeSuite::M800() {
  if (parser.seen('S')) {
    const bool forward = parser.intval('D', 1) != 0;
    const uint16_t rate = parser.ushortval('S');

    if (parser.seen('P'))
      conveyor_move(forward, rate, parser.ulongval('P'));
    else
      conveyor_set(forward, rate);
  }

  SERIAL_ECHO_START();
  if (conveyor_running())
    SERIAL_ECHOPGM("Conveyor: running ");
  else
    SERIAL_ECHOPGM("Conveyor: stopped ");
  SERIAL_ECHOLN(conveyor_rate());
}

/**
 * M801: Konveyörü anında durdurur.
 */
void GcodeSuite::M801() {
  conveyor_stop();
}

#endif // CONVEYOR_E1

