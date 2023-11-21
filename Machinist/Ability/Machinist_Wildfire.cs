using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Wildfire : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {   //热量大于50，不在过热状态释放
        if (!Qt.GetQt("攒资源") && SpellsDefine.Wildfire.IsReady() && Core.Get<IMemApiMCH>().GetHeat() >= 50 && !Core.Get<IMemApiMCH>().OverHeated() && !SpellsDefine.HeatBlast.RecentlyUsed()) return 0;
        //在刚进入过热状态释放
        if (!Qt.GetQt("攒资源") && SpellsDefine.Wildfire.IsReady() && SpellsDefine.Hypercharge.RecentlyUsed() && !SpellsDefine.HeatBlast.RecentlyUsed()) return 0;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Wildfire.GetSpell().Id).GetSpell());
    }
    
}