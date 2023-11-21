using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Dismantle : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (SpellsDefine.Dismantle.IsReady() && !Core.Me.GetCurrTarget().HasAura(860) && Qt.GetQt("自动减伤") && TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(),3) && !SpellsDefine.Tactician.IsReady()) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(SpellsDefine.Dismantle.GetSpell());
    }
    
}