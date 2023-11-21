using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_AutoCrossbow : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        var aoecount = TargetHelper.GetEnemyCountInsideSector(Core.Me, Core.Me.GetCurrTarget(), 12, 90);
        if (aoecount < 3) return -5;
        if (!SpellsDefine.AutoCrossbow.IsReady()) return -3;
        return 0;
    }
    
    public void Build(Slot slot)
    {       
        slot.Add(SpellsDefine.AutoCrossbow.GetSpell());
    }
    
}