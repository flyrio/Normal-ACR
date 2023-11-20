using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Aetherpact : ISlotResolver
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
        if (SpellsDefine.SummonSeraph.RecentlyUsed(22000)) return -3;
        if (!Core.Get<IMemApiScholar>().HasPet) return -3;
        if (PartyHelper.CastableAlliesWithin30.Any(agent=>agent.HasAura(1223))) return -3;
        if (!SpellsDefine.Aetherpact.IsReady()) return -3;
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        var skillTarget =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.Aetherpact &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        if (!skillTarget.IsValid) return -1;
        if (Core.Get<IMemApiScholar>().FairyGauge() == 0) return -1;
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
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.Aetherpact &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        slot.Add(new Spell(SpellsDefine.Aetherpact, skillTarget));
    }
}