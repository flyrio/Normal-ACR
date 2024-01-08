using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk;

public class MonkRotationEventHandler : IRotationEventHandler
{
    public void OnResetBattle()
    {
        MonkBattleData.Instance.Reset();
        Qt.Reset();
    }


    public async Task OnNoTarget()
    {
        var slot = new Slot();
        if (Core.Get<IMemApiMonk>().ChakraCount != 5 && Core.Me.ClassLevel >= 15 && Core.Me.HitboxRadius() == 0.5)
            slot.Add(SpellsDefine.Meditation.GetSpell());
        else if (!Core.Me.HasMyAuraWithTimeleft(2513, 5000) && Qt.GetQt("保持演武") && Core.Me.ClassLevel >= 52 && Core.Me.HitboxRadius() == 0.5 && !Core.Me.HasMyAura(110))
            slot.Add(SpellsDefine.FormShift.GetSpell());
        //{
        //    slot.Add(SpellsDefine.DissolveUnion.GetSpell());
        //}
        await slot.Run(false);
    }
    


    public void AfterSpell(Slot slot, Spell spell)
    {
            switch (spell.Id)
            { 
                //case SpellsDefine.Esuna:
                //    AI.Instance.BattleData.LimitAbility = true;
                //    break;
            }
    }
    public void OnBattleUpdate(int currTime)//逐帧
    {
    }

    public Task OnPreCombat()//战前准备
    {
        return Task.CompletedTask;
    }
    
    public static int CastingSpellSuccessRemainTimingSlideTp;
    public static int CastingSpellSuccessRemainTiming;
    
}