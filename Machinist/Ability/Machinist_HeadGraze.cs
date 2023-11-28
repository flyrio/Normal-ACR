using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_HeadGraze : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (Core.Me.ClassLevel < 24) return -3;
        if (!Core.Me.GetCurrTarget().CastingSpellInterruptible) return -3;
        if (!Qt.GetQt("自动打断")) return -3;
        if (!SpellsDefine.HeadGraze.IsReady()) return -3;
        //if (SpellsDefine.Wildfire.IsReady() && SpellsDefine.Hypercharge.RecentlyUsed()) return -5;
        return 0;
    }
    
    public void Build(Slot slot)
    {       
        slot.Add(SpellsDefine.HeadGraze.GetSpell());
    }
    
}