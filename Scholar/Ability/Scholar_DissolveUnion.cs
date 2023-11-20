using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_DissolveUnion : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    
    public int Check()
    {
        //if (AI.Instance.GetGCDCooldown() < 600) return -7;
        var skillTarget =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && r.CurrentHealthPercent > 0.99f && r.HasAura(1223))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        if (skillTarget.IsValid && SpellsDefine.DissolveUnion.IsReady()) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.DissolveUnion.GetSpell());
    }
}