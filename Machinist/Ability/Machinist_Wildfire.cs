using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Wildfire : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {//热量大于50，不在过热状态释放
        if (SpellsDefine.Wildfire.IsReady() && Core.Get<IMemApiMCH>().GetHeat() >= 50 && !Core.Get<IMemApiMCH>().OverHeated()) return 0;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Wildfire.GetSpell().Id).GetSpell());
    }
    
}