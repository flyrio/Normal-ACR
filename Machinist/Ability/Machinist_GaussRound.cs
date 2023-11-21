using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_GaussRound : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!SpellsDefine.GaussRound.IsMaxChargeReady(1)) return -3; //不足一层不打
        if (Core.Me.ClassLevel < 15) return -3; //等级不足不打
        if (SpellsDefine.GaussRound.RecentlyUsed(2500)) return -5;//刚打过不打
        if (Qt.GetQt("攒资源")) return -3;
        return 0;
    }
    
    public void Build(Slot slot)
    {       
        slot.Add(SpellsDefine.GaussRound.GetSpell());
    }
    
}