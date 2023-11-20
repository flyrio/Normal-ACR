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
        if (Core.Get<IMemApiMove>().IsMoving() && !Core.Me.HasAura(AurasDefine.Swiftcast)) return -2;
        return 0;
    }
    
    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}