/**
 * Robot mod: bağımsız konveyör sürücüsü (E1 sürücü yuvası).
 *
 * Marlin'in hareket planlayıcısından (planner) tamamen bağımsızdır: kendi zamanlayıcısıyla
 * (Timer4) adım üretir, bu yüzden konveyör dönerken X/Y/Z/E hareketleri aynı anda yapılabilir.
 *
 *   M800 S<adım/sn> D<1=ileri, 0=geri>             Kademeli hızlanarak sürekli çalıştır (S0 = kademeli dur)
 *   M800 S<adım/sn> D<1|0> P<adım sayısı>          Tam P adım ilerleyip durur
 *   M801                                            Anında dur
 */
#pragma once

#include "../inc/MarlinConfig.h"

#if ENABLED(CONVEYOR_E1)

void conveyor_init();
void conveyor_set(const bool forward, const uint16_t steps_per_sec);
void conveyor_move(const bool forward, const uint16_t steps_per_sec, const uint32_t steps);
void conveyor_stop();
bool conveyor_running();
uint16_t conveyor_rate();

#else

inline void conveyor_init() {}
inline void conveyor_stop() {}

#endif

