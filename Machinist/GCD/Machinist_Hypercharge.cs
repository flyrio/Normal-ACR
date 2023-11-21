using CombatRoutine;
using Common;
using Common.Define;
using Shiyuvi.Machinist.Ability;
using Trust;

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
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 30 && Core.Me.ClassLevel < 45 && SpellsDefine.HotShot.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.Drill.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.Bioblaster.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.AirAnchor.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.ChainSaw.GetSpell().Cooldown.Milliseconds > 2500 && (Core.Get<IMemApiSpell>().GetLastComboSpellId() == Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HeatedCleanShot.GetSpell().Id))) return 0;
        //不在过热状态，热量大于50时，爆发还有40秒以上时打打
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 45 && SpellsDefine.Wildfire.GetSpell().Cooldown.Milliseconds > 40000 && SpellsDefine.HotShot.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.Drill.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.Bioblaster.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.AirAnchor.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.ChainSaw.GetSpell().Cooldown.Milliseconds > 2500 && (Core.Get<IMemApiSpell>().GetLastComboSpellId() == Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HeatedCleanShot.GetSpell().Id))) return 1;
        //不在过热状态，热量大于50时，野火丢了打
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 45 && SpellsDefine.Wildfire.GetSpell().RecentlyUsed(2000)) return 2;
        //不在过热状态，热量满100，直接打
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() == 100 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 45) return 3;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}