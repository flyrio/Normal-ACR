using System.Reflection;
using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;
using Common.Language;
using Shiyuvi.Scholar;
using Shiyuvi.Scholar.Ability;
using Shiyuvi.Scholar.GCD;
using Shiyuvi.Scholar.Triggers;

namespace Shiyuvi;

public class ShiyuviScholarRotationEntry : IRotationEntry
{
    public static JobViewWindow JobViewWindow;
    
    private readonly ScholarOverlay _lazyOverlay = new ScholarOverlay();
    public string OverlayTitle { get; } = "学者";

    private IOpener open = new Opener_Scholar();
    
    private IOpener? GetOpener(uint level)//设置起手
    {
        return open;
    }


    public void DrawOverlay()
    {
        
    }
    
    public string AuthorName { get; } = "Shiyuvi";
    public Jobs TargetJob { get; } = Jobs.Scholar;

    public AcrType AcrType { get; } = AcrType.Normal;

    public List<ISlotResolver> SlotResolvers = new() //未排序
    {
        new Scholar_Resurrect(),//复活
        new Scholar_GetPet(),//召唤宝宝
        new Scholar_Esuna(),//驱散
        new Scholar_AOEGCDHeal(),
        new Scholar_SingleGCDHeal(),
        new Scholar_Dot(),
        new Scholar_AOE(),
        new Scholar_MoveGCD(),
        new Scholar_BaseGCD(),
        
        new Scholar_ChainStrategem(),//连环计
        new Scholar_Consolation(),//慰藉
        new Scholar_FeyIllumination(),//幻光
        new Scholar_Expedient(),//跑快快
        new Scholar_SacredSoil(),//罩子
        new Scholar_FeyBlessing(),//祥光
        new Scholar_SummonSeraph(),//大天使
        new Scholar_WhisperingDawn(),//低语
        new Scholar_Recitation(),//秘策
        new Scholar_Indomitability(),//不屈
        new Scholar_Excogitation(),//绿帽
        new Scholar_Lustrate(),//活性法
        new Scholar_Protraction(),//回升法
        new Scholar_DissolveUnion(),//截断链子
        new Scholar_AutoAetherflow(),//以太
        new Scholar_AutoDissipation(),//转化
        new Scholar_EnergyDrain2(),//豆子
        new Scholar_Aetherpact(),//链子
        new Scholar_LucidDreaming(),//醒梦
    };
    


    public Rotation Build(string settingFolder)
    {
        ScholarSettings.Build(settingFolder);
        return new Rotation(this, ()=>SlotResolvers)
            .AddOpener(GetOpener)
            .SetRotationEventHandler(new ScholarRotationEventHandler())
            .AddSettingUIs(new ScholarSettingView())
            .AddSlotSequences()
            .AddTriggerAction(new SCHTriggerActionSpell());
    }

    public void OnLanguageChanged(LanguageType languageType)
    {
    }

    public bool BuildQt(out JobViewWindow jobViewWindow)
    {
        jobViewWindow = new JobViewWindow(ScholarSettings.Instance.JobViewSave, ScholarSettings.Instance.save,
            OverlayTitle); // 这里设置一个静态变量.方便其他地方用
        JobViewWindow = jobViewWindow;
        jobViewWindow.AddTab("控制台", _lazyOverlay.DrawControl);
        jobViewWindow.AddTab("说明书", _lazyOverlay.DrawGeneral);
        jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
        jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);

        jobViewWindow.AddQt("爆发药",false);
        jobViewWindow.AddQt("AOE",true);
        jobViewWindow.AddQt("DOT", true);
        jobViewWindow.AddQt("连环计", true);
        
        jobViewWindow.AddQt("GCD治疗", true);
        jobViewWindow.AddQt("能力治疗", true);
        jobViewWindow.AddQt("减伤", true);
        jobViewWindow.AddQt("T绿帽", false);
        
        jobViewWindow.AddQt("自动以太", true);
        jobViewWindow.AddQt("自动转化", true);
        jobViewWindow.AddQt("优先转化", true);
        jobViewWindow.AddQt("能量吸收", true);
        
        jobViewWindow.AddQt("自动召唤", true);
        jobViewWindow.AddQt("康复", true);
        jobViewWindow.AddQt("拉人", true);
        jobViewWindow.AddQt("罩子放怪脚下", true);
        jobViewWindow.AddQt("跑快快", true);
        
        jobViewWindow.AddQt("秘策", true);
        jobViewWindow.AddQt("豆子单奶", true);
        jobViewWindow.AddQt("豆子群奶", true);
        
        
        jobViewWindow.AddHotkey("LB", new HotKeyResolver_NormalSpell(24859, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("尼给路哒哟", new Sprint.TheSprint(29057));
        jobViewWindow.AddHotkey("做盾",new Scholar_DeploymentTactics.DeploymentTactics(3585));
        //jobViewWindow.AddHotkey("做盾", new HotkeyResolver_General("../../RotationPlugin/Shiyuvi/Resources/牛逼的护盾.png",
        //    () =>
        //    {
        //        if (SpellsDefine.DeploymentTactics.IsReady() &&
        //            (SpellsDefine.Recitation.IsReady() || Core.Me.HasAura(1896)))
        //        {
        //            AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(SpellsDefine.Recitation.GetSpell()); //秘策
        //            AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(SpellsDefine.SchRuin2.GetSpell()); //毁坏
        //            if (SpellsDefine.Swiftcast.IsReady())
        //                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(SpellsDefine.Swiftcast.GetSpell()); //即刻
        //            if (SpellsDefine.Protraction.IsReady())
        //               AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(SpellsDefine.Protraction
        //                    .GetSpell()); //回升
        //            AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(SpellsDefine.Adloquium.GetSpell()); //单盾
         //           AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(SpellsDefine.DeploymentTactics
         //               .GetSpell()); //扩散
         //       }
//
         //       if (!(SpellsDefine.DeploymentTactics.IsReady() && (SpellsDefine.Recitation.IsReady() || Core.Me.HasAura(1896)) && SpellsDefine.Swiftcast.IsReady()))
         //       {
         //           AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(SpellsDefine.Succor.GetSpell());//群盾
         //       }
         //   }));
        //jobViewWindow.AddHotkey("应急群盾", new HotkeyResolver_General("../../RotationPlugin/Shiyuvi/Resources/牛逼的抬血.png",
        //    () =>
        //    {
         //       if (SpellsDefine.EmergencyTactics.IsReady())
         //           AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(SpellsDefine.EmergencyTactics.GetSpell());
         //       AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(SpellsDefine.Succor.GetSpell());//群盾
        //    }));
        jobViewWindow.AddHotkey("应急盾", new Scholar_Succor.Succor(3586));
        jobViewWindow.AddHotkey("罩子", new Scholar_Shield.Shield(188));
        //jobViewWindow.AddHotkey("罩子", new HotkeyResolver_General("../../RotationPlugin/Shiyuvi/Resources/罩子.png",
        //    () =>
         //   {
         //       if (!Qt.GetQt("罩子放怪脚下") && SpellsDefine.SacredSoil.IsReady() && !AI.Instance.BattleData.HighPrioritySlots_OffGCD.Contains(SpellsDefine.SacredSoil.GetSpell())) 
          //          AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(new Spell(SpellsDefine.SacredSoil.GetSpell().Id,SpellTargetType.Self));
          //      if (Qt.GetQt("罩子放怪脚下") && SpellsDefine.SacredSoil.IsReady() && !AI.Instance.BattleData.HighPrioritySlots_OffGCD.Contains(SpellsDefine.SacredSoil.GetSpell())) 
          //          AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(new Spell(SpellsDefine.SacredSoil.GetSpell().Id,SpellTargetType.Target));
          //  }));
        jobViewWindow.AddHotkey("跑快快", new HotKeyResolver_NormalSpell(SpellsDefine.Expedient.GetSpell().Id, SpellTargetType.Self, false));
        List<uint> Dead = new List<uint>
        {
            409,
            811
        };
        var HealTarget =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 &&
                        !r.HasAnyAura(Dead, 3000))
            .OrderBy(r => r.CurrentHealthPercent)
            .FirstOrDefault();
        //jobViewWindow.AddHotkey("单盾", new HotKeyResolver_NormalSpell(185, SpellTargetType.DynamicTarget, false));
        jobViewWindow.AddHotkey("群盾", new HotKeyResolver_NormalSpell(186, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("营救天边的你",new Rescue.RescueTarget(7571));
        
        return true;
    }
}