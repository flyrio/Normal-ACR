using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Reassemble : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Qt.GetQt("攒资源") && SpellsDefine.Drill.CoolDownInGCDs(1) && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsMaxChargeReady(1)
            && SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds ) return 3;
        if (!Qt.GetQt("攒资源") && SpellsDefine.HotShot.CoolDownInGCDs(1) && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsMaxChargeReady(1) && Core.Me.ClassLevel < 76
            && SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds ) return 3;
        if (!Qt.GetQt("攒资源") && SpellsDefine.Bioblaster.CoolDownInGCDs(1) && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsMaxChargeReady(1)
            && SpellsDefine.Bioblaster.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds ) return 3;
        if (!Qt.GetQt("攒资源") && SpellsDefine.AirAnchor.CoolDownInGCDs(1) && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsMaxChargeReady(1) && Core.Me.ClassLevel >= 76
            && SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds ) return 3;
        if (!Qt.GetQt("攒资源") && SpellsDefine.ChainSaw.CoolDownInGCDs(1) && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsMaxChargeReady(1)
            && SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds ) return 3;
        return -1;
    }
    
    public void Build(Slot slot)
    {       
        slot.Add(SpellsDefine.Reassemble.GetSpell());
    }
    
}