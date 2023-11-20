using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_SacredSoil : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    //罩子
    public int Check()
    {
        List<uint> Dead = new List<uint>
        {
            409,
            811
        };
        if (!Qt.GetQt("减伤")) return -3; 
        if (!TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(),3)) return -2; //目标释放AOE
        if (!Core.Me.HasAura(304)) return -3; //没豆子
        if (!SpellsDefine.SacredSoil.IsReady()) return -3;
        var Tankshield =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.IsTank() &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();//非无敌状态下坦克
        if (Qt.GetQt("减伤") && Core.Me.HasAura(304) && SpellsDefine.SacredSoil.IsReady() && Tankshield.GetCurrTarget().HasAura(1193) &&
            !Tankshield.GetCurrTarget().IsBoss()) return 1; //罩子好了，有豆子，坦克的目标有血仇，坦克的目标不是boss
        return 0;
    }
    
    public void Build(Slot slot)
    {
        {
            List<uint> Dead = new List<uint>
            {
                409,
                811
            };
            var Tankshield =PartyHelper.CastableAlliesWithin30
                .Where(r => r.CurrentHealth > 0 && r.IsTank() &&
                            !r.HasAnyAura(Dead, 3000))
                .OrderBy(r => r.CurrentHealthPercent)
                .FirstOrDefault();//非无敌状态下坦克
            if (Qt.GetQt("罩子放怪脚下")) 
                slot.Add(new Spell(SpellsDefine.SacredSoil, SpellTargetType.Target));
            if (!Qt.GetQt("罩子放怪脚下"))
                slot.Add(new Spell(SpellsDefine.SacredSoil, SpellTargetType.Self));
            if (Core.Me.HasAura(304) && SpellsDefine.SacredSoil.IsReady() && Tankshield.GetCurrTarget().HasAura(1193) &&
                !Tankshield.GetCurrTarget().IsBoss())
                slot.Add(new Spell(SpellsDefine.SacredSoil, Tankshield));
        }
    }
}