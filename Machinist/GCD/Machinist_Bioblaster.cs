using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_Bioblaster : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return SpellsDefine.Bioblaster.GetSpell();
    }
    
    public int Check()
    {
        var aoecount = TargetHelper.GetEnemyCountInsideSector(Core.Me, Core.Me.GetCurrTarget(), 12, 90);
        if (!Qt.GetQt("攒资源") && SpellsDefine.Bioblaster.IsReady() && aoecount > 2 && !Core.Get<IMemApiMCH>().OverHeated() && Qt.GetQt("AOE")) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}