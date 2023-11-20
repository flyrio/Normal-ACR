using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.GCD;

public class Scholar_AOEGCDHeal : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    
    public int Check()
    {
        if (!Qt.GetQt("GCD治疗")) return -3; //开关
        if (!SpellsDefine.Succor.IsReady()) return -3; //GCD转好
        var skillTarget = PartyHelper.CastableAlliesWithin15.Count(r =>
            r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.Succor); //技能范围内，血量符合要求的人数
        if (skillTarget < ScholarSettings.Instance.AOEHealCount) return -1; //人数大于要求就治疗
        if (SpellsDefine.Succor.RecentlyUsed(3000)) return -2; //防止连打
        if (Core.Me.HasAura(297)) return -3; //检查鼓舞buff
        if (Core.Get<IMemApiMove>().IsMoving() && !Core.Me.HasAura(AurasDefine.Swiftcast)) return -2;
        return 0; //移动中无即刻不打
    }
    
    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Succor.GetSpell());
    }
}