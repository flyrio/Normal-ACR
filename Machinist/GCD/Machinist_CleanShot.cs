using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_CleanShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        if (Core.Me.ClassLevel < 26)
            return SpellsDefine.SplitShot.GetSpell();
        if (Core.Me.ClassLevel is >= 26 and < 64)
            return SpellsDefine.CleanShot.GetSpell();
        if (Core.Me.ClassLevel >= 64) return SpellsDefine.HeatedCleanShot.GetSpell();
        return null;
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() ==
            SpellsDefine.SlugShot.GetSpell().Id)
            return 2; //2-3
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() ==
            SpellsDefine.HeatedSlugShot.GetSpell().Id)
            return 2; //2-3
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}