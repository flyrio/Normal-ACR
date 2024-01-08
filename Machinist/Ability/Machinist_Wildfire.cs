using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Wildfire : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        //在刚进入过热状态释放
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Wildfire) == 566) return -1;
        if (!Qt.GetQt("攒资源") && (Core.Get<IMemApiBuff>().BuffStackCount(Core.Me,2688) == 5 || SpellsDefine.Hypercharge.RecentlyUsed()) && SpellsDefine.Wildfire.IsReady()) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       
        slot.Add(SpellsDefine.Wildfire.GetSpell());
    }
    
}