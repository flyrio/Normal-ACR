using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_SplitShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        
        if (Core.Me.ClassLevel == 1) return SpellsDefine.SplitShot.GetSpell();
        if (Core.Me.ClassLevel < 26 && Core.Me.HasMyAura(851))
            return SpellsDefine.SlugShot.GetSpell();
        if (Core.Me.ClassLevel is >= 1 and < 54)
            return SpellsDefine.SplitShot.GetSpell();
        if (Core.Me.ClassLevel >= 54) return SpellsDefine.HeatedSplitShot.GetSpell();

        return null;
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.SplitShot) == 566) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}