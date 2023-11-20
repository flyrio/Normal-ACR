using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_SummonSeraph : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    //大天使
    public int Check()
    {
        if (!Qt.GetQt("能力治疗")) return -3; 
        var skillTarget = PartyHelper.CastableAlliesWithin30.Count(r =>
            r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.SummonSeraph);
        if (Core.Me.HasAura(791)) return -3; //转化状态打不了
        if (!Core.Get<IMemApiScholar>().HasPet) return -3; //没宠物打不了
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        if (skillTarget < ScholarSettings.Instance.AOEHealCount) return -5; //人数大于要求
        if (!SpellsDefine.SummonSeraph.IsReady()) return -3;
        return 0;
    }
    
    public void Build(Slot slot)
    {

            slot.Add(SpellsDefine.SummonSeraph.GetSpell());
    }
}