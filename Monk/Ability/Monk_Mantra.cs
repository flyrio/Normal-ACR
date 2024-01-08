using CombatRoutine;
using Common;
using Common.Define;
using ECommons;

namespace Shiyuvi.Monk.Ability;

public class Monk_Mantra : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.Mantra.GetSpell();
    }
    
    public int Check()
    {
        if (!SpellsDefine.Mantra.IsReady()) return -1;
        var skillTarget = PartyHelper.CastableAlliesWithin30.Count(r =>
            r.CurrentHealth > 0 && (r.HasAura(1219) || r.HasAura(1896) || r.HasAura(1892) || r.HasAura(2611)));
        if (skillTarget >= 1) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}