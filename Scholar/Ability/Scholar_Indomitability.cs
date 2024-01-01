using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;


namespace Shiyuvi.Scholar.Ability;

public class Scholar_Indomitability : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    //不屈
    public int Check()
    {
        if (!Qt.GetQt("豆子群奶")) return -3;
        var Indomitability = PartyHelper.CastableAlliesWithin15
            .Count(r => r.CurrentHealthPercent < ScholarSettings.Instance.Indomitability && r.CurrentHealth > 0);
        if (Qt.GetQt("秘策"))
        {
            if (Qt.GetQt("能力治疗") && Indomitability >= ScholarSettings.Instance.AOEHealCount && !SpellsDefine.Recitation.IsReady() && Core.Me.HasAura(304) && SpellsDefine.Indomitability.IsReady()) return 4; //秘策没好，有豆子
            if (Qt.GetQt("能力治疗") && Indomitability >= ScholarSettings.Instance.AOEHealCount && Core.Me.HasMyAura(1896) && SpellsDefine.Indomitability.IsReady()) return 4; //有秘策buff
        }
        if (!Qt.GetQt("秘策") && Qt.GetQt("能力治疗") && Indomitability >= ScholarSettings.Instance.AOEHealCount && Core.Me.HasAura(304) && SpellsDefine.Indomitability.IsReady()) return 4; //有豆子
        
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Indomitability.GetSpell());
    }
}