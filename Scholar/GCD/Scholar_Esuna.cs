using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Esuna : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    
    public int Check()
    {
        if (Core.Get<IMemApiMove>().IsMoving())
        {
            return -1;
        }

        if (PartyHelper.CastableAlliesWithin30.Any(agent=>agent.HasCanDispel()) && Qt.GetQt("康复"))
        {
            return 1;
        }
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Esuna,
            PartyHelper.CastableAlliesWithin30.LastOrDefault(agent => agent.HasCanDispel())));
    }
}