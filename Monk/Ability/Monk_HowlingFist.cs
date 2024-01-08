using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Monk.Ability;

public class Monk_HowlingFist : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    
    public Spell GetSpell()

    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.HowlingFist.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.HowlingFist) == 566) return -1;
        if (Core.Get<IMemApiMonk>().ChakraCount != 5) return -1;
        if (TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5) > 3) return 0;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}