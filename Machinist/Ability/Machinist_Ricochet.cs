using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Ricochet : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!SpellsDefine.Ricochet.IsMaxChargeReady(1)) return -3; //不足一层不打
        if (Core.Me.ClassLevel < 50) return -3; //等级不足不打
        if (SpellsDefine.Ricochet.RecentlyUsed(2500)) return -5;//刚打过不打
        if (Qt.GetQt("攒资源")) return -3;
        return 0;
    }
    
    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Ricochet.GetSpell());
    }
    
}