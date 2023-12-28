using CombatRoutine;
using CombatRoutine.TriggerModel;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_ChainStrategem : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    
    public int Check()
    {
        if (AI.Instance.BattleData.CurrBattleTimeInMs < 5000) return -1;
        if (!Qt.GetQt("连环计")) return -3;
        if (!SpellsDefine.ChainStrategem.IsReady()) return -3;
        if (Core.Me.GetCurrTarget().MaxHealth <= Core.Me.MaxHealth * 15UL) return -1;
        return 0;
    }
        
    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.ChainStrategem.GetSpell());
    }
}