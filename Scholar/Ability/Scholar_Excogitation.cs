using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Excogitation : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    //绿帽
    public int Check()
    {
        List<uint> Dead = new List<uint>
        {
            409,
            811
        };
        var TankExcogitation =PartyHelper.CastableTanks
            .Where(r => r.CurrentHealth > 0 && r.IsTank() &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();//秘策T绿帽逻辑
        switch (TankExcogitation.IsValid)
        {
            case true when Qt.GetQt("T绿帽") && Core.Me.HasAura(1896) && SpellsDefine.Excogitation.IsReady() && !TankExcogitation.HasMyAura(1220):
                return 1;//自己有秘策buff,绿帽好了,目标没有绿帽
            case true when Qt.GetQt("T绿帽") && !Core.Me.HasAura(1896) && Core.Me.HasAura(304) && SpellsDefine.Excogitation.IsReady() && !TankExcogitation.HasMyAura(1220) && !SpellsDefine.Recitation.IsReady():
                return 2;//自己没有秘策buff，绿帽好了，有豆子，目标没有绿帽buff，且秘策在CD
        }
        var AllExcogitation =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 &&
                        !r.HasAnyAura(Dead, 3000) && r.CurrentHealthPercent <= ScholarSettings.Instance.Lustrate)
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();//秘策全体绿帽逻辑
        switch (AllExcogitation.IsValid)
        {
            case true when Qt.GetQt("T绿帽") && Core.Me.HasAura(1896) && SpellsDefine.Excogitation.IsReady() && !AllExcogitation.HasMyAura(1220) && Qt.GetQt("能力治疗") :
                return 1;//有秘策buff,绿帽好了,目标没有绿帽
            case true when Qt.GetQt("T绿帽") && !Core.Me.HasAura(1896) && Core.Me.HasAura(304) && SpellsDefine.Excogitation.IsReady() && !AllExcogitation.HasMyAura(1220) && !SpellsDefine.Recitation.IsReady() && Qt.GetQt("能力治疗"):
                return 2;//自己没有秘策buff，绿帽好了，有豆子，目标没有绿帽buff，且秘策在CD
        }
            return -1;
    }

    public void Build(Slot slot)
    {
        List<uint> Dead = new List<uint>
        {
            409,
            811
        };
        var TankExcogitation =PartyHelper.CastableTanks
            .Where(r => r.CurrentHealth > 0 && r.IsTank() &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();//秘策T绿帽逻辑
        var AllExcogitation =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();//秘策全体绿帽逻辑
        if (Qt.GetQt("T绿帽"))
            slot.Add(new Spell(SpellsDefine.Excogitation,TankExcogitation));
        if (!Qt.GetQt("T绿帽"))
            slot.Add(new Spell(SpellsDefine.Excogitation,AllExcogitation));
    }
}