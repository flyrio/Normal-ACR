using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Expedient : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    //跑快快
    public int Check()
    {
        if (!Qt.GetQt("跑快快")) return -3;
        if (!Qt.GetQt("减伤")) return -3;
        if (!SpellsDefine.Expedient.IsReady()) return -3;
        if (!(AOEHelper.TargerastingIsAOE(Core.Me.GetCurrTarget(),10) || TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(),10))) return -5; //目标释放AOE
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        return 0;
    }
    
    public void Build(Slot slot)
    {       

            slot.Add(SpellsDefine.Expedient.GetSpell());
    }
    
}