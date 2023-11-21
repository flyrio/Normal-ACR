using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_RookAutoturret : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {//电量超过50好了就放
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.RookAutoturret.GetSpell().Id).GetSpell().IsReady() && Core.Get<IMemApiMCH>().GetBattery() >= 50 && !Core.Get<IMemApiMCH>().Robotactive()) return 0;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.RookAutoturret.GetSpell().Id).GetSpell());
    }
    
}