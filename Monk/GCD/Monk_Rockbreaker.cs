using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Monk.GCD;

public class Monk_Rockbreaker : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()

    {
        return SpellsDefine.Rockbreaker.GetSpell();
    }
    
    public int Check()
    {
        if (!Qt.GetQt("AOE")) return -1;
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return -1;
        if (TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5) < 3) return -1;
        if (Core.Me.ClassLevel >= 30 && (Core.Me.HasMyAura(109) || Core.Me.HasMyAura(2513))) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}