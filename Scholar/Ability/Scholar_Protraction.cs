using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Protraction : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    
    public int Check()
    {
        if (Qt.GetQt("减伤") && TargetHelper.TargercastingIsDeathSentence(Core.Me.GetCurrTarget(), 10) && SpellsDefine.Protraction.IsReady())
            return 2;
        return -1;
    }

    public void Build(Slot slot)
    {
            slot.Add(new Spell(SpellsDefine.Protraction ,SpellTargetType.TargetTarget));
    }
}