﻿using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Ricochet : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    { //弹射层数大于虹吸弹层数.弹射好了
        if (!Qt.GetQt("攒资源") && Core.Me.ClassLevel >= 50&& Core.Get<IMemApiSpell>().GetCharges(2890) > Core.Get<IMemApiSpell>().GetCharges(2874)) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Ricochet.GetSpell());
    }
    
}