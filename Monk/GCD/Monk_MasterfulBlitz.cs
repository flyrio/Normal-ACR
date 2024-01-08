using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Monk.GCD;

public class Monk_MasterfulBlitz : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    
    public Spell GetSpell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.MasterfulBlitz.GetSpell().Id).GetSpell();
        //if (Core.Get<IMemApiMonk>().MastersGauge[2] != ChakraType.None && Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Both)
        //    return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.PhantomRush.GetSpell().Id).GetSpell();
        //if (Core.Get<IMemApiMonk>().MastersGauge[0] == Core.Get<IMemApiMonk>().MastersGauge[1] &&
        //    Core.Get<IMemApiMonk>().MastersGauge[1] == Core.Get<IMemApiMonk>().MastersGauge[2])
        //    return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.ElixirField.GetSpell().Id).GetSpell();
        //if (Core.Get<IMemApiMonk>().MastersGauge[0] != Core.Get<IMemApiMonk>().MastersGauge[1] &&
        //    Core.Get<IMemApiMonk>().MastersGauge[1] != Core.Get<IMemApiMonk>().MastersGauge[2])
        //    return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.RisingPhoenix.GetSpell().Id).GetSpell();
        //return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.CelestialRevolution.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return -1;
        if (Core.Me.ClassLevel >= 60 && Core.Get<IMemApiMonk>().BlitzTimer.TotalMilliseconds > 0) return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}