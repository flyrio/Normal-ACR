using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_AutoDissipation : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Qt.GetQt("自动转化")) return -3;
        if (!SpellsDefine.Dissipation.IsReady()) return -3;
        if (SpellsDefine.SummonSeraph.RecentlyUsed(22000)) return -3; //大天使期间用不了
        if (Core.Me.HasAura(304)) return -30;
        if (Qt.GetQt("自动以太") && !Qt.GetQt("优先转化") && SpellsDefine.Aetherflow.IsReady()) return -4;
        if (!Core.Get<IMemApiScholar>().HasPet) return -3; //没宠物打不了
        if (SpellsDefine.WhisperingDawn.RecentlyUsed(3000) || SpellsDefine.FeyBlessing.RecentlyUsed(3000) ||
            SpellsDefine.FeyIllumination.RecentlyUsed(3000)) return -9; //防吞仙女技能
        if (!Core.Get<IMemApiScholar>().HasPet) return -3;
        if (!Qt.GetQt("自动以太")) return 2;
        if (Qt.GetQt("自动以太") && Qt.GetQt("优先转化") && SpellsDefine.Aetherflow.IsReady()) return 3;
        return 0;
    }
    
    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Dissipation.GetSpell());
    }
}