using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_SplitShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        switch (Core.Me.ClassLevel)
        {
            case >= 1 and < 54:
                return SpellsDefine.SplitShot.GetSpell();
            case >= 54:
                return SpellsDefine.HeatedSplitShot.GetSpell();
        }

        return null;
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