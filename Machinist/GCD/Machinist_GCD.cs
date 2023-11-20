using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.GCD;

public class Machinist_GCD : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()//逻辑需要在过热前打完3才能正常跑?
    {
        if (Core.Me.ClassLevel < 26)
        {//1、2连招
            if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SplitShot.GetSpell().Id))
                return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SlugShot.GetSpell().Id).GetSpell();
            return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SplitShot.GetSpell().Id).GetSpell();
        }
        //2触发3,1触发2，不符合返回1
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SlugShot.GetSpell().Id))
            return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.CleanShot.GetSpell().Id).GetSpell();
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SplitShot.GetSpell().Id))
            return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SlugShot.GetSpell().Id).GetSpell();
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SplitShot.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}