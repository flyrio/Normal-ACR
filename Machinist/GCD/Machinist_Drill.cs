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
    {//钻头优先级最高
        if (Core.Me.HasMyAura(851) && SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds && !Core.Me.HasMyAura(2688)) return 1;
        if (!Qt.GetQt("攒资源") && !SpellsDefine.Reassemble.IsMaxChargeReady(1) && !Core.Me.HasMyAura(2688) && SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}