using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_SlugShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        switch (Core.Me.ClassLevel)
        {
            case >= 2 and < 60:
                return SpellsDefine.SlugShot.GetSpell();
            case >= 60:
                return SpellsDefine.HeatedSlugShot.GetSpell();
        }

        return null;
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() ==
            SpellsDefine.SplitShot.GetSpell().Id)
            return 1; //1-2
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() ==
            SpellsDefine.HeatedSplitShot.GetSpell().Id)
            return 2; //1-2
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}