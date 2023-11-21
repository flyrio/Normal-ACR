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
    {
        if (Core.Me.HasMyAura(851) && Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HotShot.GetSpell().Id).GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds && !Core.Me.HasMyAura(2688)) return 1;
        if (!Qt.GetQt("攒资源") && !SpellsDefine.Reassemble.IsMaxChargeReady(1) && !Core.Me.HasMyAura(2688) && Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HotShot.GetSpell().Id).GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}