using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_RookAutoturret : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {//电量超过50好了就放
        //if (!Qt.GetQt("攒资源") && Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.RookAutoturret.GetSpell().Id).GetSpell().IsReady() && Core.Get<IMemApiMCH>().GetBattery() >= 50 && !Core.Get<IMemApiMCH>().Robotactive()) return 0;
        if (Qt.GetQt("攒资源")) return -1;
        if (!Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.RookAutoturret.GetSpell().Id).GetSpell()
            .IsReady()) return -1;
        if (Core.Get<IMemApiMCH>().GetBattery() < 50) return -1;
        if (Core.Get<IMemApiMCH>().Robotactive()) return -1;
        if (Core.Get<IMemApiMCH>().GetBattery() == 100) return 0;
        if (Core.Get<IMemApiMCH>().GetBattery() == 90 && (Core.Me.ClassLevel < 4 || Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HotShot).IsReady() || Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HotShot).CoolDownInGCDs(1)) && (Core.Me.ClassLevel < 90 || SpellsDefine.ChainSaw.GetSpell().IsReady() || SpellsDefine.ChainSaw.RecentlyUsed(1))) return 1;
        if (SpellsDefine.Wildfire.GetSpell().Cooldown.TotalMilliseconds < 10000 && Core.Me.ClassLevel >= 45) return 0;
        if (SpellsDefine.Wildfire.GetSpell().Cooldown.TotalMilliseconds > 60000 && Core.Me.ClassLevel >= 45 && SpellsDefine.Wildfire.GetSpell().Cooldown.TotalMilliseconds < 70000) return 0;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.RookAutoturret.GetSpell().Id).GetSpell());
    }
    
}