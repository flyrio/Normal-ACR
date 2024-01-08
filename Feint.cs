using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;
using Shiyuvi.Monk;

namespace Shiyuvi;

public class Feint : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!Qt.GetQt("自动减伤")) return -1;
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Feint) == 566) return -1;
        if (AI.Instance.GetGCDCooldown() < 600) return -1;
        if (!SpellsDefine.Feint.IsReady()) return -1;
        if ((TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(),5) || AOEHelper.TargerastingIsAOE(Core.Me.GetCurrTarget(),5)) && !Core.Me.GetCurrTarget().HasAura(1195)) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(SpellsDefine.Feint.GetSpell());
    }
    
}