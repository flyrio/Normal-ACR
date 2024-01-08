using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk.GCD;

public class Monk_DragonKick : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.DragonKick.GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return -1;
        if (Core.Me.ClassLevel > 50 && (Core.Me.HasMyAura(107) || Core.Me.HasMyAura(2513)) &&
             !Core.Me.HasMyAura(1861)) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}