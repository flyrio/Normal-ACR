using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Monk.Ability;

public class Monk_Far : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.Meditation.GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiMonk>().ChakraCount == 5) return -1;
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Meditation.GetSpell().Id,Core.Me));
    }
}