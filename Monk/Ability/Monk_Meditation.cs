using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Monk.Ability;

public class Monk_Meditation : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    
    public Spell GetSpell()

    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.TheForbiddenChakra.GetSpell().Id).GetSpell();
    }
    
    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return -1;
        //if (AI.Instance.BattleData.CurrBattleTimeInMs < 2 * Core.Get<IMemApiSpell>().GetGCDDuration()) return -1;
        if (Core.Get<IMemApiMonk>().ChakraCount != 5) return -1;
        if (SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds <
            2 * Core.Get<IMemApiSpell>().GetGCDDuration() && Core.Me.ClassLevel >= 68)  return -3;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}