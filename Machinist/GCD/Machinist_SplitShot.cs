using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_SplitShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.SplitShot.GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() ==
            SpellsDefine.SplitShot.GetSpell().Id)
            return -2; //1-2
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() ==
            SpellsDefine.HeatedSplitShot.GetSpell().Id)
            return -2; //1-2
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() ==
            SpellsDefine.SlugShot.GetSpell().Id)
            return -1; //2-3
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() ==
            SpellsDefine.HeatedSlugShot.GetSpell().Id)
            return -1; //2-3
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}