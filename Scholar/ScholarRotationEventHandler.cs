#region

using System.ComponentModel.Design;
using System.Diagnostics;
using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;

#endregion

namespace Shiyuvi.Scholar;

public class ScholarRotationEventHandler : IRotationEventHandler
{
    public void OnResetBattle()
    {
        ScholarBattleData.Instance.Reset();
        Qt.Reset();
    }


    public async Task OnNoTarget()
    {
        var slot = new Slot();
        if (SpellsDefine.DissolveUnion.IsReady())
        {
            slot.Add(SpellsDefine.DissolveUnion.GetSpell());
        }
        await slot.Run(false);
    }
    


    public void AfterSpell(Slot slot, Spell spell)
    {
            switch (spell.Id)
            { 
                //AI.Instance.BattleData.LimitAbility = false;
                case SpellsDefine.SchRuin:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.SchBroil:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.SchBroil2:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.SchBroil3:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.SchBroil4:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.Adloquium:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.Physick:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.Succor:
                    AI.Instance.BattleData.LimitAbility = true;
                    break;
                case SpellsDefine.SchRuin2:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.Bio:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.Bio2:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.ArtOfWar:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.ArtOfWarIi:
                    AI.Instance.BattleData.LimitAbility = false;
                    break;
                case SpellsDefine.Esuna:
                    AI.Instance.BattleData.LimitAbility = true;
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
    
    public static int CastingSpellSuccessRemainTimingSlideTp;
    public static int CastingSpellSuccessRemainTiming;
    
    public void OnExitRotation()
    {

        SettingMgr.GetSetting<GeneralSettings>().CastingSpellSuccessRemainTimingSlideTp = CastingSpellSuccessRemainTimingSlideTp;
        SettingMgr.GetSetting<GeneralSettings>().CastingSpellSuccessRemainTiming = CastingSpellSuccessRemainTiming;

    }
    
    public void OnEnterRotation()
    {
        CastingSpellSuccessRemainTimingSlideTp = SettingMgr.GetSetting<GeneralSettings>().CastingSpellSuccessRemainTimingSlideTp;
        CastingSpellSuccessRemainTiming =
        SettingMgr.GetSetting<GeneralSettings>().CastingSpellSuccessRemainTiming;
        SettingMgr.GetSetting<GeneralSettings>().CastingSpellSuccessRemainTimingSlideTp = 470;
        SettingMgr.GetSetting<GeneralSettings>().CastingSpellSuccessRemainTiming = 470;
    }
}