using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;
using Microsoft.VisualBasic.CompilerServices;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_SpreadShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SpreadShot.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        var aoecount = TargetHelper.GetEnemyCountInsideSector(Core.Me, Core.Me.GetCurrTarget(), 12, 90);
        if (aoecount < 3) return -5;
        if (!Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SpreadShot.GetSpell().Id).GetSpell().IsReady()) return -3;
        if (!Qt.GetQt("AOE")) return -3;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}