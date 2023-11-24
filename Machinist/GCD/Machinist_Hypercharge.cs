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
        return SpellsDefine.Hypercharge.GetSpell();
    }
    
    public int Check()
    {
        //不在过热状态，热量大于50时，还没学会野火，无脑丢
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 35 && Core.Me.ClassLevel < 45 && SpellsDefine.Hypercharge.IsReady()
            &&
            SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds <= 5000) return 0;
        //不在过热，热量大于50，学会野火了，野火马上好了丢
        //不在过热，热量大于50，学会野火了，野火冷却30秒以上丢
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 45 && Core.Me.ClassLevel < 58 && SpellsDefine.Hypercharge.IsReady()
            &&
            SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds <= 5000) return 2;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 58 && Core.Me.ClassLevel < 76 && SpellsDefine.Hypercharge.IsReady()
            &&
            (SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds <= 5000 || SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= 5000 )) return 2;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel >= 76 && Core.Me.ClassLevel < 90 && SpellsDefine.Hypercharge.IsReady()
            &&
            (SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= 5000 || SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= 5000 )) return 2;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiMCH>().GetHeat() >=50 && !Core.Get<IMemApiMCH>().OverHeated() && Core.Me.ClassLevel == 90 && SpellsDefine.Hypercharge.IsReady()
            &&
            (SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= 5000 || SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= 5000  || SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= 5000)) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}