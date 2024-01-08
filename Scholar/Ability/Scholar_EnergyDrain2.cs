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
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.EnergyDrain2) == 566) return -1;
        if (AI.Instance.BattleData.CurrBattleTimeInMs <= 5501) return -3;
        if (!Qt.GetQt("能量吸收")) return -3;
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        if (Core.Me.ClassLevel >= 60 && Qt.GetQt("自动转化") && SpellsDefine.Dissipation.GetSpell().Cooldown.TotalMilliseconds < Core.Get<IMemApiSpell>().GetGCDDuration() && Core.Me.HasAura(304) && Core.Get<IMemApiScholar>().Aetherflow() >= 1) return 1;//冷却时间小于GCD，当前有豆子打
        if (Core.Me.ClassLevel >= 60 && Qt.GetQt("自动转化") && SpellsDefine.Dissipation.GetSpell().Cooldown.TotalMilliseconds < 2 * Core.Get<IMemApiSpell>().GetGCDDuration() && Core.Me.HasAura(304) && Core.Get<IMemApiScholar>().Aetherflow() >= 2) return 2;//冷却时间小于2个GCD，当前豆子大于1个打
        if (Core.Me.ClassLevel >= 60 && Qt.GetQt("自动转化") && SpellsDefine.Dissipation.GetSpell().Cooldown.TotalMilliseconds < 3 * Core.Get<IMemApiSpell>().GetGCDDuration() && Core.Me.HasAura(304) && Core.Get<IMemApiScholar>().Aetherflow() == 3) return 3;//冷却时间小于3个GCD，当前豆子3个打
        if (Core.Me.ClassLevel >= 60 && Qt.GetQt("自动转化") && SpellsDefine.Dissipation.IsReady() && Core.Me.HasAura(304)) return 0;//保底
        
        if (Qt.GetQt("自动以太") && SpellsDefine.Aetherflow.GetSpell().Cooldown.TotalMilliseconds < Core.Get<IMemApiSpell>().GetGCDDuration() && Core.Me.HasAura(304) && Core.Get<IMemApiScholar>().Aetherflow() >= 1) return 1;//冷却时间小于GCD，当前有豆子打
        if (Qt.GetQt("自动以太") && SpellsDefine.Aetherflow.GetSpell().Cooldown.TotalMilliseconds < 2 * Core.Get<IMemApiSpell>().GetGCDDuration() && Core.Me.HasAura(304) && Core.Get<IMemApiScholar>().Aetherflow() >= 2) return 2;//冷却时间小于GCD，当前有豆子打
        if (Qt.GetQt("自动以太") && SpellsDefine.Aetherflow.GetSpell().Cooldown.TotalMilliseconds < 3 * Core.Get<IMemApiSpell>().GetGCDDuration() && Core.Me.HasAura(304) && Core.Get<IMemApiScholar>().Aetherflow() >= 3) return 3;//冷却时间小于GCD，当前有豆子打
        if (Qt.GetQt("自动以太") && SpellsDefine.Aetherflow.IsReady() && Core.Me.HasAura(304)) return 0;
        
        return -1;
    }

    public void Build(Slot slot)
    {
        
        slot.Add(SpellsDefine.EnergyDrain2.GetSpell());
    }
}