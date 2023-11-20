using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;


namespace Shiyuvi.Scholar.Ability;

public class Scholar_LucidDreaming : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {   //冷却没好不用
        if (!SpellsDefine.LucidDreaming.IsReady())
            return -1;
        //LogHelper.Info("MANA"+Core.Me.CurrentMana);
        if (AI.Instance.GetGCDCooldown() < 600) return -7;
        //蓝量大于8000不用
        if (Core.Me.CurrentMana > 8000) return -2;
        return 0;
    }

    public void Build(Slot slot)
    {   //醒梦
        slot.Add(SpellsDefine.LucidDreaming.GetSpell());
    }
}