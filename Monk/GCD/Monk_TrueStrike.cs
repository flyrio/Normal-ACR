using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk.GCD;

public class Monk_TrueStrike : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.TrueStrike.GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return -1;
        if (Core.Me.ClassLevel > 3 && Core.Me.ClassLevel < 18 &&
            Core.Me.HasMyAura(108)) return 1;
        if (Core.Me.ClassLevel >= 18 && (Core.Me.HasMyAura(108) || Core.Me.HasMyAura(2513)) &&
            Core.Me.HasMyAuraWithTimeleft(3001, 6000)) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}