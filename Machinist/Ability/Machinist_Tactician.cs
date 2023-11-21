using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Tactician : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (SpellsDefine.Tactician.IsReady() && !Core.Me.HasAura(1826) && !Core.Me.HasAura(1951) && !Core.Me.HasAura(2177) && !Core.Me.HasAura(1934) && Qt.GetQt("自动减伤") && TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(),3) && Core.Get<IMemApiMCH>().OverHeated()) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(SpellsDefine.Tactician.GetSpell());
    }
    
}