using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.GCD;

public class Scholar_SingleGCDHeal : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    
    public int Check()
    {
        List<uint> Dead = new List<uint>
        {
            409,
            811
        };
        if (!Qt.GetQt("GCD治疗")) return -3; 
        if (!SpellsDefine.SchRuin.IsReady()) return -3; //GCD转好
        var HighLevel = PartyHelper.CastableAlliesWithin30.FirstOrDefault(r =>
            r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.Adloquium && !r.HasAnyAura(Dead,3000));
        if (HighLevel.HasAura(297)) return -3;
        if (!HighLevel.IsValid && Core.Me.ClassLevel > 50) return -2;
        var LowLevel = PartyHelper.CastableAlliesWithin30.FirstOrDefault(r =>
            r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.LowLevelSingleHeal);
        if (LowLevel.HasAura(297)) return -3;
        if (!LowLevel.IsValid && Core.Me.ClassLevel <= 50) return -2;
        if (Core.Get<IMemApiMove>().IsMoving() && !Core.Me.HasAura(AurasDefine.Swiftcast)) return -2;
        return 0;
    }
    
    public void Build(Slot slot)
    {
        List<uint> Dead = new List<uint>
        {
            409,
            811
        };
        var LowLevel =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.LowLevelSingleHeal)
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        var HighLevel =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent <= ScholarSettings.Instance.Adloquium &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        if (Core.Me.ClassLevel < 30 )
            slot.Add(new Spell(SpellsDefine.Physick, LowLevel));
        if (Core.Me.ClassLevel >= 30 && Core.Me.ClassLevel < 50)
            slot.Add(new Spell(SpellsDefine.Adloquium, LowLevel));
        if (Core.Me.ClassLevel >= 50)
            slot.Add(new Spell(SpellsDefine.Adloquium, HighLevel));
    }
}