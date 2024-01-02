using System.ComponentModel;
using AEAssist.MemoryApi;
using CombatRoutine;
using Common;
using Common.Define;
using ECommons;
using ECommons.DalamudServices;
using Shiyuvi.Machinist;

namespace Shiyuvi;

public class Resurrect
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   
        //拉人QT没开不拉
        if (!Qt.GetQt("拉人")) return -3;
        //死人身上已经有复活buff了不拉
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(AurasDefine.Raise));
        if (!skillTarget.IsValid) return -2;
        //其他情况 常开，随时准备拉
        return 1;
    }

    public void Build(Slot slot)
    {   //把死了的人加进目标
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(AurasDefine.Raise));
        //复活目标加入slot
        slot.Add(new Spell(SpellsDefine.Resurrection, skillTarget));
    }
}