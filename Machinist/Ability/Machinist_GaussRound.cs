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
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiSpell>().GetCharges(2874) >= 0.667 && Core.Me.ClassLevel >= 15 && Core.Me.ClassLevel < 74 && AI.Instance.GetGCDCooldown() >= 600) return 1;
        if (!Qt.GetQt("攒资源") && Core.Get<IMemApiSpell>().GetCharges(2874) >= 1 && Core.Me.ClassLevel >= 74 && AI.Instance.GetGCDCooldown() >= 600) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       
        slot.Add(SpellsDefine.GaussRound.GetSpell());
    }
    
}