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
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 30 && Core.Me.ClassLevel < 45 && SpellsDefine.HotShot.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.Drill.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.Bioblaster.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.AirAnchor.GetSpell().Cooldown.Milliseconds > 2500 && SpellsDefine.ChainSaw.GetSpell().Cooldown.Milliseconds > 2500 && (Core.Get<IMemApiSpell>().GetLastComboSpellId() == Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HeatedCleanShot.GetSpell().Id)) && !SpellsDefine.Hypercharge.RecentlyUsed(100000)) return 0;
        //不在过热，热量大于50，学会野火了，野火马上好了丢
        //不在过热，热量大于50，学会野火了，野火冷却30秒以上丢
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 45 && SpellsDefine.Wildfire.GetSpell().Cooldown.Milliseconds < 1200 && SpellsDefine.Wildfire.GetSpell().Cooldown.Milliseconds > 30000 && !SpellsDefine.GaussRound.IsMaxChargeReady(1) && !SpellsDefine.Ricochet.IsMaxChargeReady(1) && !SpellsDefine.Hypercharge.RecentlyUsed(10000)) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}