using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_AutoAetherflow : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Qt.GetQt("自动以太")) return -3;
        if (!SpellsDefine.Aetherflow.IsReady()) //以太超流没好
            return -3;
        if (Core.Me.HasAura(304)) //有豆子
            return -30;
        if (Qt.GetQt("自动转化") && Qt.GetQt("优先转化") && SpellsDefine.Dissipation.IsReady())
            return -4;
        if (Qt.GetQt("自动转化") && !Qt.GetQt("优先转化") && SpellsDefine.Dissipation.IsReady())
            return 3;
        return 0;
    }
    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Aetherflow.GetSpell());
    }
}