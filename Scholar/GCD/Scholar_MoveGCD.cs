using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;


namespace Shiyuvi.Scholar.GCD;

public class Scholar_MoveGCD : ISlotResolver
{

    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    
    public int Check()
    {
        if (Core.Get<IMemApiMove>().IsMoving() && AI.Instance.CanUseGCD()) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {
        if (Core.Me.ClassLevel < 39 && Qt.GetQt("DOT") && Qt.GetQt("移动输出"))
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Bio.GetSpell().Id).GetSpell());
        if (Core.Me.ClassLevel >= 39 && Qt.GetQt("移动输出"))
            slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SchRuin2.GetSpell().Id).GetSpell());
    }
}