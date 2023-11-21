﻿using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_HotShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HotShot.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        if (Qt.GetQt("攒资源") && !Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HotShot.GetSpell().Id).GetSpell().IsReady()) return -3;
        return 0;
    }

    public void Build(Slot slot)
    {
        if (SpellsDefine.Reassemble.IsReady())
            slot.Add(SpellsDefine.Reassemble.GetSpell());
        slot.Add(GetSpell());
    }
}