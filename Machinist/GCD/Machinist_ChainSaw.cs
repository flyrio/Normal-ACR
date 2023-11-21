using CombatRoutine;
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
        if (Qt.GetQt("攒资源") && !SpellsDefine.ChainSaw.IsReady()) return -3;
        return 0;
    }

    public void Build(Slot slot)
    {
        if (SpellsDefine.Reassemble.IsReady())
            slot.Add(SpellsDefine.Reassemble.GetSpell());
        slot.Add(GetSpell());
    }
}