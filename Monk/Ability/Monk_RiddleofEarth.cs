using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk.Ability;

public class Monk_RiddleofEarth : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Qt.GetQt("自动减伤")) return -1;
        if (AI.Instance.GetGCDCooldown() < 600) return -1;
        if (!SpellsDefine.RiddleofEarth.IsReady()) return -1;
        if (TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(),5) || AOEHelper.TargerastingIsAOE(Core.Me.GetCurrTarget(),5)) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(SpellsDefine.RiddleofEarth.GetSpell());
    }
    
}