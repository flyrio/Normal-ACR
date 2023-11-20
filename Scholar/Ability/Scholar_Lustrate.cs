using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Lustrate : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    
    public int Check()
    {
        List<uint> Dead = new List<uint>
        {
            409,
            811
        };
        if (!Qt.GetQt("能力治疗")) return -3;
        if (SpellsDefine.Lustrate.RecentlyUsed(2500)) return -3;
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        var skillTarget =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.Lustrate &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        if (!skillTarget.IsValid) return -2;
        return 0;
    }

    public void Build(Slot slot)
    {
        List<uint> Dead = new List<uint>
        {
            409,
            811
        };
        var skillTarget =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.Lustrate &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        if ((Core.Me.HasMyAura(1896) || Core.Me.HasAura(304)) && SpellsDefine.Excogitation.IsReady()) //有秘策、豆子时，绿帽好了丢
            slot.Add(new Spell(SpellsDefine.Excogitation, skillTarget));
        if (Core.Me.HasMyAura(304) && !SpellsDefine.Excogitation.IsReady() && !SpellsDefine.Excogitation.RecentlyUsed()) //绿帽没好，刚没打过绿帽,有豆子打活性法
            slot.Add(new Spell(SpellsDefine.Lustrate, skillTarget));
    }
}