using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Scholar.GCD;

public class Scholar_AOE : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.ArtOfWar.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        if (Core.Me.ClassLevel < 46) return -3;
        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
        if (aoeCount < 2) return -2;
        if (!Qt.GetQt("AOE")) return -3;
        return 0;
    }
    
    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}