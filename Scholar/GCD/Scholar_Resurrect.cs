using CombatRoutine;
using Common;
using Common.Define;

namespace Shiyuvi.Scholar.GCD;

public class Scholar_Resurrect : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   
        //特定副本不拉人
        if (Core.Get<IMemApiMap>().GetCurrTerrId() == 1069) return -1;
        if (Core.Get<IMemApiMap>().GetCurrTerrId() == 1075) return -1;
        if (Core.Get<IMemApiMap>().GetCurrTerrId() == 1076) return -1;
        if (Core.Get<IMemApiMap>().GetCurrTerrId() == 1055) return -1;
        if (Core.Get<IMemApiMap>().GetCurrTerrId() == 1056) return -1;
        if (Core.Get<IMemApiMap>().GetCurrTerrId() == 1037) return -1;
        //即刻没转好不拉
        if (!SpellsDefine.Swiftcast.IsReady()) return -3;
        //拉人QT没开不拉
        if (!Qt.GetQt("拉人")) return -3;
        //蓝量小于2400不拉
        if (Core.Me.CurrentMana < 2400) return -2;
        //死人身上已经有复活buff了不拉
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(AurasDefine.Raise));
        if (!skillTarget.IsValid) return -2;
        //其他情况 常开，随时准备拉
        return 1;
    }

    public void Build(Slot slot)
    {   //把死了的人加进目标
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(AurasDefine.Raise));
        //即刻加入slot
        slot.Add(SpellsDefine.Swiftcast.GetSpell());
        //复活目标加入slot
        slot.Add(new Spell(SpellsDefine.Resurrection, skillTarget));
    }
}