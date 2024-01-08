using System.Xml.Schema;
using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk.GCD;

public class Monk_Bootshine : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.Bootshine.GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return -1;
        if (Core.Me.ClassLevel <= 3) return 0;
        if (Core.Me.ClassLevel > 3 && Core.Me.ClassLevel < 50 &&
                Core.Me.HasMyAura(107)) return 1;
        if (Core.Me.ClassLevel >= 50 && (Core.Me.HasMyAura(107) || Core.Me.HasMyAura(2513)) &&
                Core.Me.HasMyAura(1861)) return 2;
        if (!Core.Me.HasMyAura(107) || !Core.Me.HasMyAura(108) || Core.Me.HasMyAura(109) || Core.Me.HasMyAura(110) ||
            Core.Me.HasMyAura(2513)) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}