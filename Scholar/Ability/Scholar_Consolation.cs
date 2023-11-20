using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Consolation : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    //慰藉
    public int Check()
    {
        if (!Qt.GetQt("能力治疗")) return -3; 
        var skillTarget = PartyHelper.CastableAlliesWithin30.Count(r =>
            r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.SummonSeraph);
        if (AI.Instance.GetGCDCooldown() < 600) return -7;
        if (SpellsDefine.SummonSeraph.RecentlyUsed(4000) && SpellsDefine.Consolation.IsMaxChargeReady(2)) return 1;
        if (skillTarget >= ScholarSettings.Instance.AOEHealCount &&
            !Core.Me.HasAura(1917) && SpellsDefine.Consolation.IsMaxChargeReady(1) && Core.Get<IMemApiScholar>().SeraphTimer() >= 4000 && Core.Get<IMemApiScholar>().SeraphTimer() < 12000) return 1; //22秒内，身上没盾，有一层慰藉，符合治疗要求人数，打慰藉
        if (SpellsDefine.Consolation.IsMaxChargeReady(1) && Core.Get<IMemApiScholar>().SeraphTimer() < 4000 && Core.Get<IMemApiScholar>().SeraphTimer() > 0) return 3; //最后4秒有一层慰藉的话打掉慰藉
        return -1;
    }
    
    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Consolation.GetSpell());
    }
}