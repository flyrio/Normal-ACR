using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk.Ability;

public class Monk_Brotherhood : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.Brotherhood.GetSpell();
    }
    
    public int Check()
    {
        if (Qt.GetQt("攒资源")) return -1;
        if (!SpellsDefine.Brotherhood.IsReady()) return -1;
        if (AI.Instance.BattleData.CurrBattleTimeInMs < 3 * Core.Get<IMemApiSpell>().GetGCDDuration()) return -1;
        if (Qt.GetQt("爆发对齐") && (Core.Me.HasMyAuraWithTimeleft(1181, 16000) || !Core.Me.HasMyAura(1181))) return -1; //大于16秒或无BUFF时不打
        return 1;

    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}