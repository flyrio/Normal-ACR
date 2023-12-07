using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;
using ImGuiNET;
using Shiyuvi.Machinist.Ability;
using Trust;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_Hypercharge : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public Spell GetSpell()
    {
        return SpellsDefine.Hypercharge.GetSpell();
    }
    
    public int Check()
    {
        //不在过热状态，热量大于50时，还没学会野火，无脑丢
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >= 50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 30 && Core.Me.ClassLevel < 45 && SpellsDefine.Hypercharge.IsReady()
            &&
            SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds >= 5000
            && !SpellsDefine.HotShot.IsReady()
            ||
            (Core.Get<IMemApiMCH>().GetHeat() == 100 && !Core.Me.HasMyAura(851) && !Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && SpellsDefine.Hypercharge.IsReady())) return 0;
        //不在过热，热量大于50，学会野火了，野火马上好了丢
        //不在过热，热量大于50，学会野火了，野火冷却20秒以上丢
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >= 50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 45 && Core.Me.ClassLevel < 58 && SpellsDefine.Hypercharge.IsReady()
            &&
            SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds >= 5000
            && !SpellsDefine.HotShot.IsReady()
            && (SpellsDefine.Wildfire.GetSpell().Cooldown.TotalMilliseconds >= 20000 || SpellsDefine.Wildfire.IsReady())
            && Core.Get<IMemApiSpell>().GetCharges(SpellsDefine.Ricochet.GetSpell().Id) < 0.667
            && Core.Get<IMemApiSpell>().GetElapsedGCD() < 600
            ||
            (Core.Get<IMemApiMCH>().GetHeat() == 100 && !Core.Me.HasMyAura(851) && !Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && SpellsDefine.Hypercharge.IsReady())) return 2;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >= 50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 58 && Core.Me.ClassLevel < 74 && SpellsDefine.Hypercharge.IsReady()
            &&
            (SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds >= 5000 && SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds >= 5000 )
            && !SpellsDefine.HotShot.IsReady() && !SpellsDefine.Drill.IsReady()
            && (SpellsDefine.Wildfire.GetSpell().Cooldown.TotalMilliseconds >= 20000 || SpellsDefine.Wildfire.IsReady())
            && Core.Get<IMemApiSpell>().GetCharges(SpellsDefine.Ricochet.GetSpell().Id) < 0.667
            && Core.Get<IMemApiSpell>().GetElapsedGCD() < 600
            ||
            (Core.Get<IMemApiMCH>().GetHeat() == 100 && !Core.Me.HasMyAura(851) && !Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && SpellsDefine.Hypercharge.IsReady())) return 2;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >= 50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 74 && Core.Me.ClassLevel < 76 && SpellsDefine.Hypercharge.IsReady()
            &&
            (SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds >= 5000 && SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds >= 5000 )
            && !SpellsDefine.HotShot.IsReady() && !SpellsDefine.Drill.IsReady()
            && (SpellsDefine.Wildfire.GetSpell().Cooldown.TotalMilliseconds >= 20000 || SpellsDefine.Wildfire.IsReady())
            && Core.Get<IMemApiSpell>().GetCharges(SpellsDefine.Ricochet.GetSpell().Id) < 2
            && Core.Get<IMemApiSpell>().GetElapsedGCD() < 600
            ||
            (Core.Get<IMemApiMCH>().GetHeat() == 100 && !Core.Me.HasMyAura(851) && !Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && SpellsDefine.Hypercharge.IsReady())) return 2;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >= 50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 76 && Core.Me.ClassLevel < 90 && SpellsDefine.Hypercharge.IsReady()
            &&
            (SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds >= 5000 && SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds >= 5000 )
            && !SpellsDefine.Drill.IsReady() && !SpellsDefine.AirAnchor.IsReady()
            && (SpellsDefine.Wildfire.GetSpell().Cooldown.TotalMilliseconds >= 20000 || SpellsDefine.Wildfire.IsReady())
            && Core.Get<IMemApiSpell>().GetCharges(SpellsDefine.Ricochet.GetSpell().Id) < 2
            && Core.Get<IMemApiSpell>().GetElapsedGCD() < 600
            ||
            (Core.Get<IMemApiMCH>().GetHeat() == 100 && !Core.Me.HasMyAura(851) && !Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && SpellsDefine.Hypercharge.IsReady())) return 2;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >= 50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel == 90 && SpellsDefine.Hypercharge.IsReady()
            &&
            (SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds >= 5000 && SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds >= 5000  && SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds >= 5000)
            && !SpellsDefine.Drill.IsReady() && !SpellsDefine.AirAnchor.IsReady() && !SpellsDefine.ChainSaw.IsReady()
            && (SpellsDefine.Wildfire.GetSpell().Cooldown.TotalMilliseconds >= 20000 || SpellsDefine.Wildfire.IsReady())
            && Core.Get<IMemApiSpell>().GetCharges(SpellsDefine.Ricochet.GetSpell().Id) < 2
            && Core.Get<IMemApiSpell>().GetElapsedGCD() < 600
            ||
            (Core.Get<IMemApiMCH>().GetHeat() == 100 && !Core.Me.HasMyAura(851) && !Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && SpellsDefine.Hypercharge.IsReady())) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}