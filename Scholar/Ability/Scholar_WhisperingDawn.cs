using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_WhisperingDawn : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    //低语
    public int Check()
    {
        if (!Qt.GetQt("能力治疗")) return -3;
        if (!SpellsDefine.WhisperingDawn.IsReady()) return -3;
        var skillTarget = PartyHelper.CastableAlliesWithin30.Count(r =>
            r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.WhisperingDawn);
        if (Core.Me.HasAura(791)) return -3; //转化状态打不了
        if (Core.Get<IMemApiScholar>().SeraphTimer() < 4000 && Core.Get<IMemApiScholar>().SeraphTimer() > 0) return -5; //大天使最后4秒不打，防止吞技能
        if (!Core.Get<IMemApiScholar>().HasPet) return -3; //没宠物打不了
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        if (skillTarget < ScholarSettings.Instance.AOEHealCount) return -5; //人数小于要求不治疗
        return 0;
    }
    
    public void Build(Slot slot)
    {

           slot.Add(SpellsDefine.WhisperingDawn.GetSpell());
    }
}