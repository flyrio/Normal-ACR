using System.Reflection.Metadata.Ecma335;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;


namespace Shiyuvi.Scholar.GCD;

public class Scholar_Dot : ISlotResolver
{
    public Spell GetSpell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Bio.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.SchRuin) == 566) return -1;
        if (DotBlacklistHelper.IsBlackList(Core.Me.GetCurrTarget()))
            return -10;
        if (!Qt.GetQt("DOT")) return -3;
        if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Bio, 4000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Bio2, 4000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.Biolysis, 4000)) return -1;
        if (Core.Me.GetCurrTarget().MaxHealth <= Core.Me.MaxHealth * 15UL && //目标是小怪，且目标血量小于阈值
            Core.Me.GetCurrTarget().CurrentHealthPercent <= ScholarSettings.Instance.NotBossDot) return -1;
        if (Core.Me.GetCurrTarget().MaxHealth > Core.Me.MaxHealth * 15UL && //目标是大怪，且目标血量小于阈值
            Core.Me.GetCurrTarget().CurrentHealthPercent <= ScholarSettings.Instance.BossDot) return -1;
            return 0;
    }
    
    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}
