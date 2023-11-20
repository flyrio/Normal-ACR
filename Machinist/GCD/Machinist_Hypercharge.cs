using CombatRoutine;
using Common;
using Common.Define;
using Shiyuvi.Machinist.Ability;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_Hypercharge : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Hypercharge.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        //不在过热状态，热量大于50时，还没学会野火，无脑丢
        //不在过热状态，热量大于50时，爆发还有40秒以上时打打
        if (Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 30 && Core.Me.ClassLevel < 45) return 0;
        if (Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 45 && SpellsDefine.Wildfire.GetSpell().Cooldown.Milliseconds > 40000) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}