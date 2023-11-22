using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_GCD: ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        var lastComboSpellId = Core.Get<IMemApiSpell>().GetLastComboSpellId();
        // 刚打完2
        if (lastComboSpellId == SpellsDefine.SlugShot.GetSpell().Id ||
            lastComboSpellId == SpellsDefine.HeatedSlugShot.GetSpell().Id)
        {
            return Core.Me.ClassLevel >= 26 ? SpellsDefine.CleanShot.GetSpell() : SpellsDefine.SplitShot.GetSpell();
        }
        // 刚打完1
        if (lastComboSpellId == SpellsDefine.SplitShot.GetSpell().Id ||
            lastComboSpellId == SpellsDefine.HeatedSplitShot.GetSpell().Id)
        {
            return SpellsDefine.SlugShot.GetSpell();
        }
        return SpellsDefine.SplitShot.GetSpell();
    }
    
    public int Check()
    {
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}