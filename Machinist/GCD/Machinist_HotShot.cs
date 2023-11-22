using CombatRoutine;
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
    {//小于26级，大于等于10级，热弹好了就打
        if (Core.Me.HasMyAura(851) && SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds && !Core.Me.HasMyAura(2688) && Core.Me.ClassLevel < 26 && Core.Me.ClassLevel >= 10) return 1;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HotShot.GetSpell().Id).GetSpell().IsReady() && !Core.Me.HasMyAura(2688)) return 2;
        if (Core.Me.HasMyAura(851) && SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds && !Core.Me.HasMyAura(2688) && Core.Me.ClassLevel >= 76) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}