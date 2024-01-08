using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk.Ability;

public class Monk_PerfectBalance: ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.PerfectBalance.GetSpell();
    }
    
    public int Check()
    {
        if (Qt.GetQt("攒资源")) return -1;
        if (Core.Me.HasMyAura(110)) return -1;
        if (Core.Get<IMemApiMonk>().MastersGauge[2] != ChakraType.None) return -1;
        if (SpellsDefine.PerfectBalance.GetSpell().Charges < 1) return -1;
        if (Core.Me.HasMyAura(2513)) return -1;
        if (Core.Me.ClassLevel < 50) return -1;
        if (Core.Me.ClassLevel >= 50 &&
            Core.Me.ClassLevel < 68)
            if (Core.Me.HasMyAuraWithTimeleft(3001,8000) || Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246,8000)) return 1;
        if (AI.Instance.BattleData.CurrBattleTimeInMs < 3.1 * Core.Get<IMemApiSpell>().GetGCDDuration()) return -1;
        //if (SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds < 38000 && SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds > 8000 && SpellsDefine.Brotherhood.GetSpell().Cooldown.TotalMilliseconds > 38000) return 2;
        //if (Core.Me.HasMyAuraWithTimeleft(1181,14000) && !Core.Me.HasMyAuraWithTimeleft(1181,18000)) return 1;
        if (Core.Me.ClassLevel < 70 &&
            SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds > 40000) return 1;
        if (Core.Me.HasMyAuraWithTimeleft(1181, 4 * Core.Get<IMemApiSpell>().GetGCDDuration()))//爆发期
            if ((Qt.GetQt("爆发对齐") && ((SpellsDefine.Brotherhood.GetSpell().Cooldown.TotalMilliseconds < 3 * Core.Get<IMemApiSpell>().GetGCDDuration()) || SpellsDefine.Brotherhood.GetSpell().Cooldown.TotalMilliseconds > 10000)) || !Qt.GetQt("爆发对齐"))
            {
                if (Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Solar &&
                    !Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000)) return -1;
                if (Core.Get<IMemApiMonk>().ActiveNadi != NaDi.Lunar)
                    if (Core.Me.HasMyAuraWithTimeleft(3001,8000) || Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246,8000)) return 1;
                if (Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Lunar) return 1;
            }
        if (!Core.Me.HasMyAuraWithTimeleft(1181, 4 * Core.Get<IMemApiSpell>().GetGCDDuration()))//非爆发期
        {
            if (SpellsDefine.PerfectBalance.GetSpell().Charges * 40000 <
                SpellsDefine.Brotherhood.GetSpell().Cooldown.TotalMilliseconds &&
                SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds > 40000) return 1;
        }

        

        
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}