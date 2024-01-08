using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Scholar.GCD;

public class Scholar_BaseGCD : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SchRuin.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.SchRuin) == 566) return -1;
        if (!(SpellsDefine.LucidDreaming.IsReady() || Core.Me.HasMyAura(1204)) && Core.Me.CurrentMana < 1000 && (Core.Me.HasAura(43) || Core.Me.HasAura(44))) return -1;
        if (Core.Get<IMemApiMove>().IsMoving() && !Core.Me.HasAura(AurasDefine.Swiftcast)) return -2;
        return 0;
    }
    
    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}