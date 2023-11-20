#region
using CombatRoutine;
using CombatRoutine.Setting;
using CombatRoutine.TriggerModel;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;
#endregion

namespace Shiyuvi.Scholar.GCD;

public class Scholar_GetPet : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public Spell GetSpell()
    {
        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.SummonEos.GetSpell().Id).GetSpell();
    }
    public int Check()
    {
        if (Core.Get<IMemApiScholar>().HasPet) //有宠物不叫
            return -1;
        if (Core.Me.HasAura(791)) //有转化不叫
            return -3;
        if (Core.Me.HasAura(418)) //有生还(刚复活的5秒无敌buff)不叫
            return -4;
        if (!Qt.GetQt("自动召唤"))
            return -3;
        if (Core.Get<IMemApiMove>().IsMoving() && !Core.Me.HasAura(AurasDefine.Swiftcast))
            return -2;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}