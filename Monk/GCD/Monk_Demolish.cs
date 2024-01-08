using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk.GCD;

public class Monk_Demolish : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.Demolish.GetSpell();
    }
    
    public int Check()
    {
        if (SpellsDefine.Demolish.RecentlyUsed(2500)) return -1;
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return -1;
        if (Core.Me.ClassLevel >= 30 && (Core.Me.HasMyAura(109) || Core.Me.HasMyAura(2513)) &&
            !Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246,6000)) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}