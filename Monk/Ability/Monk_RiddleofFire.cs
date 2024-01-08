using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Monk.Ability;

public class Monk_RiddleofFire : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.RiddleofFire.GetSpell();
    }
    
    public int Check()
    {
        if (Qt.GetQt("攒资源")) return -1;
        if (!SpellsDefine.RiddleofFire.IsReady()) return -1;
        if (AI.Instance.BattleData.CurrBattleTimeInMs < 0.4 * Core.Get<IMemApiSpell>().GetGCDDuration()) return -1;
        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}