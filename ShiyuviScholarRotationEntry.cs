using System.Reflection;
using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common.Define;
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
    
    public void DrawOverlay()
    {
        
    }
    
    public string AuthorName { get; } = "Shiyuvi";
    public Jobs TargetJob { get; } = Jobs.Scholar;

    public AcrType AcrType { get; } = AcrType.Normal;

    public List<ISlotResolver> SlotResolvers = new() //未排序
    {
        new Scholar_ChainStrategem(),//连环计
        new Scholar_LucidDreaming(),//醒梦
        new Scholar_Resurrect(),//复活
        new Scholar_DissolveUnion(),//截断链子
        new Scholar_GetPet(),//召唤宝宝
        new Scholar_AutoAetherflow(),//以太
        new Scholar_AutoDissipation(),//转化
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
        new Scholar_EnergyDrain2(),//豆子
        new Scholar_Aetherpact(),//链子
        new Scholar_Esuna(),//驱散
        new Scholar_AOEGCDHeal(),
        new Scholar_SingleGCDHeal(),
        new Scholar_Dot(),
        new Scholar_AOE(),
        new Scholar_MoveGCD(),
        new Scholar_BaseGCD(),
    };
    


    public Rotation Build(string settingFolder)
    {
        ScholarSettings.Build(settingFolder);
        return new Rotation(this, ()=>SlotResolvers)
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
        jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
        jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
        jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
        jobViewWindow.AddQt("AOE",true);
        jobViewWindow.AddQt("DOT", true);
        jobViewWindow.AddQt("连环计", true);
        jobViewWindow.AddQt("移动输出", true);
        
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
        
        jobViewWindow.AddHotkey("LB", new HotKeyResolver_NormalSpell(24859, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
        return true;
    }
}