using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_HeatBlast : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return SpellsDefine.HeatBlast.GetSpell();
    }
    
    public int Check()
    {
        if (SpellsDefine.HeatBlast.IsReady() && Core.Get<IMemApiMCH>().OverHeated()) return 3;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}