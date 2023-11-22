using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_BarrelStabilizer : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Qt.GetQt("攒资源") && SpellsDefine.BarrelStabilizer.IsReady() && Core.Get<IMemApiMCH>().GetHeat() <= 50 && Core.Me.ClassLevel >= 66) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(SpellsDefine.BarrelStabilizer.GetSpell());
    }
    
}