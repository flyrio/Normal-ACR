using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_CleanShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
          return  SpellsDefine.CleanShot.GetSpell();
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