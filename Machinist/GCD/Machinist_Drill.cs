using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_Drill : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return SpellsDefine.Drill.GetSpell();
    }
    
    public int Check()
    {//有整备，钻头等级足够
        if (Core.Me.HasMyAura(851) && SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetGCDDuration() && !Core.Me.HasMyAura(2688) && Core.Me.ClassLevel >= 58) return 1;
        if (!Qt.GetQt("攒资源") && (SpellsDefine.Drill.IsReady() || SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds) && !Core.Me.HasMyAura(2688) && Core.Me.ClassLevel >= 58) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        if (AI.Instance.BattleData.CurrBattleTimeInMs == 0 && !Qt.GetQt("攒资源") && (SpellsDefine.Reassemble.IsReady() || SpellsDefine.Reassemble.IsMaxChargeReady(1)) && !Core.Me.HasMyAura(851))
            slot.Add(SpellsDefine.Reassemble.GetSpell());
        slot.Add(GetSpell());
    }
}