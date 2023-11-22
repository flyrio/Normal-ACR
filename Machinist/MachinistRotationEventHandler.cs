﻿using CombatRoutine;
using Common;
using Common.Define;
using Shiyuvi.Machinist;

namespace Shiyuvi.Machinist;

public class MachinistRotationEventHandler : IRotationEventHandler
{
    public void OnResetBattle()
    {
        MachinistBattleData.Instance.Reset();
        Qt.Reset();
    }


    public async Task OnNoTarget()
    {
        var slot = new Slot();
        if (Core.Me.HasAura(1199) && Core.Me.ClassLevel >= 20)
        {
            slot.Add(SpellsDefine.Peloton.GetSpell());
        }
        await slot.Run(false);
    }
    


    public void AfterSpell(Slot slot, Spell spell)
    {
            switch (spell.Id)
            { 
                //AI.Instance.BattleData.LimitAbility = false;
                case SpellsDefine.SplitShot:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.SlugShot:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.HotShot:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.SpreadShot:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.Hypercharge:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.HeatBlast:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.AutoCrossbow:
                    AI.Instance.BattleData.AbilityCount = 0;
                    break;
                case SpellsDefine.Drill:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.HeatedSplitShot:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.HeatedSlugShot:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.HeatedCleanShot:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.Bioblaster:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.AirAnchor:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.ChainSaw:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
            }
    }
    public void OnBattleUpdate(int currTime)//逐帧
    {
    }

    public Task OnPreCombat()//战前准备
    {
        return Task.CompletedTask;
    }
}