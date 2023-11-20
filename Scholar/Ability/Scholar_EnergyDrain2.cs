using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_EnergyDrain2 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    
    public int Check()
    {
        if (!Qt.GetQt("能量吸收")) return -3;
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        if (Qt.GetQt("自动转化") && SpellsDefine.Dissipation.IsReady() && Core.Me.HasAura(304)) return 0;
        if (Qt.GetQt("自动以太") && SpellsDefine.Aetherflow.IsReady() && Core.Me.HasAura(304)) return 0;
        return -1;
    }

    public void Build(Slot slot)
    {
        
        slot.Add(SpellsDefine.EnergyDrain2.GetSpell());
    }
}