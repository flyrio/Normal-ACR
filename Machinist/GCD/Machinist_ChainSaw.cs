using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_ChainSaw : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return SpellsDefine.ChainSaw.GetSpell();
    }
    
    public int Check()
    {
        if (Core.Me.HasMyAura(851) && SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= Core.Get<IMemApiSpell>().GetComboTimeLeft().Milliseconds && !Core.Me.HasMyAura(2688) && Core.Me.ClassLevel == 90) return 1;
        if (!Qt.GetQt("攒资源") && SpellsDefine.ChainSaw.IsReady() && !Core.Me.HasMyAura(2688)) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}