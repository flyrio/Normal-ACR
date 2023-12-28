using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_FeyIllumination : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    //幻光
    public int Check()
    {
        if (!Qt.GetQt("减伤")) return -3; 
        if (!(AOEHelper.TargerastingIsAOE(Core.Me.GetCurrTarget(),10) || TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(),10))) return -5; //目标释放AOE
        if (Core.Me.HasAura(791)) return -3; //转化状态打不了
        if (!Core.Get<IMemApiScholar>().HasPet) return -3; //没宠物打不了
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        if (!SpellsDefine.FeyIllumination.IsReady()) return -3;
        if (Core.Get<IMemApiScholar>().SeraphTimer() < 4000 & Core.Get<IMemApiScholar>().SeraphTimer() > 0) return -5;
        if (SpellsDefine.Expedient.IsReady() && Core.Me.ClassLevel == 90) return -4;
        if (SpellsDefine.Expedient.RecentlyUsed(20000)) return -4;
        return 0;
    }
    
    public void Build(Slot slot)
    {

            slot.Add(SpellsDefine.FeyIllumination.GetSpell());
    }
}