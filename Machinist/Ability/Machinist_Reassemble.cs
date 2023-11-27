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
        if (!Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsReady() && Core.Me.ClassLevel >= 58
            && SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetGCDDuration() 
            && SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds - (Core.Get<IMemApiSpell>().GetGCDDuration() - Core.Get<IMemApiSpell>().GetElapsedGCD()) < 0) return 3;
        if (!Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsReady() && Core.Me.ClassLevel < 26
            && SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetGCDDuration() 
            && SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds - (Core.Get<IMemApiSpell>().GetGCDDuration() - Core.Get<IMemApiSpell>().GetElapsedGCD()) < 0) return 3;
        if (!Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsReady() && Core.Me.ClassLevel >= 76
            && SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetGCDDuration() 
            && SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds - (Core.Get<IMemApiSpell>().GetGCDDuration() - Core.Get<IMemApiSpell>().GetElapsedGCD()) < 0) return 3;
        if (!Qt.GetQt("攒资源") && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.Reassemble.RecentlyUsed(2000) && SpellsDefine.Reassemble.IsReady() && Core.Me.ClassLevel == 90
            && SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetGCDDuration() 
            && SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds - (Core.Get<IMemApiSpell>().GetGCDDuration() - Core.Get<IMemApiSpell>().GetElapsedGCD()) < 0) return 3;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiSpell>().GetLastComboSpellId() == 2868 && SpellsDefine.Reassemble.IsReady() && Core.Me.ClassLevel >= 26 && Core.Me.ClassLevel < 58) return 3;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiSpell>().GetLastComboSpellId() == 2866 && SpellsDefine.Reassemble.IsReady() && Core.Me.ClassLevel < 26 && Core.Me.ClassLevel >= 10) return 3;
        return -1;
    }
    
    public void Build(Slot slot)
    {       
        slot.Add(SpellsDefine.Reassemble.GetSpell());
    }
    
}