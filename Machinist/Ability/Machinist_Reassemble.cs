using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Reassemble : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        //需判断什么时候打钻头等技能
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(SpellsDefine.Reassemble.GetSpell());
    }
    
}