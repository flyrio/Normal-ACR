using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Machinist.Ability;

public class Machinist_Dismantle : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Dismantle) == 566) return -1;
        if (AI.Instance.GetGCDCooldown() < 600) return -1;
        if (SpellsDefine.Dismantle.IsReady() && !Core.Me.GetCurrTarget().HasAura(860) && Qt.GetQt("自动减伤") && (TargetHelper.TargercastingIsbossaoe(Core.Me.GetCurrTarget(),5) || AOEHelper.TargerastingIsAOE(Core.Me.GetCurrTarget(),5)) && !SpellsDefine.Tactician.IsReady() && !SpellsDefine.Tactician.RecentlyUsed(10000)) return 1;
        return -1;
    }
    
    public void Build(Slot slot)
    {       

        slot.Add(SpellsDefine.Dismantle.GetSpell());
    }
    
}