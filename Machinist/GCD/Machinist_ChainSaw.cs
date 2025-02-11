﻿using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_ChainSaw : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return SpellsDefine.ChainSaw.GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.ChainSaw) == 566) return -1;
        if (Qt.GetQt("回转飞锯") && Core.Me.HasMyAura(851) && SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetGCDDuration() && !Core.Me.HasMyAura(2688) && Core.Me.ClassLevel == 90) return 1;
        if (Qt.GetQt("回转飞锯") && !Qt.GetQt("攒资源") && (SpellsDefine.ChainSaw.IsReady() || SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds)  && !Core.Me.HasMyAura(2688) && Core.Me.ClassLevel == 90) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}