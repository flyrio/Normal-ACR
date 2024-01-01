using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Recitation : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    
    public int Check()
    {
        if (!Qt.GetQt("秘策")) return -1;
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
        if (Qt.GetQt("T绿帽") && TankExcogitation.IsValid && SpellsDefine.Excogitation.IsReady() && SpellsDefine.Recitation.IsReady() && SpellsDefine.Recitation.IsReady()) return 1;
        //秘策好了，绿帽好了，目标符合要求,在绿帽前使用秘策
        
        var AllExcogitation =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 &&
                        !r.HasAnyAura(Dead, 3000) && r.CurrentHealthPercent <= ScholarSettings.Instance.Lustrate)
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();//秘策非T绿帽逻辑
        if (!Qt.GetQt("T绿帽") && AllExcogitation.IsValid && SpellsDefine.Excogitation.IsReady() && SpellsDefine.Recitation.IsReady() && SpellsDefine.Recitation.IsReady() && Qt.GetQt("能力治疗")) return 2;
        //秘策好了，绿帽好了，目标符合活性法要求,在绿帽前使用秘策

        var Indomitability = PartyHelper.CastableAlliesWithin15
            .Count(r => r.CurrentHealthPercent < ScholarSettings.Instance.Indomitability && r.CurrentHealth > 0);//秘策不屈逻辑
        if (Indomitability >= ScholarSettings.Instance.AOEHealCount && SpellsDefine.Recitation.IsReady() && SpellsDefine.Indomitability.IsReady() && Qt.GetQt("能力治疗")) return 3;
        //秘策好了，不屈好了，治疗阈值和人数符合要求,在不屈前使用秘策
        return -1;
    }
        

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Recitation.GetSpell());
    }
}