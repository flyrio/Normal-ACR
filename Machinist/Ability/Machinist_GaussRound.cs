﻿using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_GaussRound : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiSpell>().GetCharges(2874) >= 1 && Core.Me.ClassLevel >= 15 && Core.Get<IMemApiSpell>().GetCharges(2890) <= Core.Get<IMemApiSpell>().GetCharges(2874)) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       
        slot.Add(SpellsDefine.GaussRound.GetSpell());
    }
    
}