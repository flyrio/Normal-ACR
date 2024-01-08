using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common.Define;
using Common.Language;
using Shiyuvi.Monk;
using Shiyuvi.Monk.Ability;
using Shiyuvi.Monk.GCD;
using Shiyuvi.Monk.QT;
using Shiyuvi.Monk.Triggers;

namespace Shiyuvi;

public class ShiyuviMonkRotationEntry : IRotationEntry
{
    public static JobViewWindow JobViewWindow;
    
    private readonly MonkOverlay _lazyOverlay = new MonkOverlay();
    public string OverlayTitle { get; } = "武僧";
    
    private IOpener open = new Opener_Monk();
    
    private IOpener? GetOpener(uint level)//设置起手
    {
        return open;
    }
    
    public void DrawOverlay()
    {

    }
    
    public string AuthorName { get; } = "Shiyuvi";
    public Jobs TargetJob { get; } = Jobs.Monk;

    public AcrType AcrType { get; } = AcrType.Normal;

    public List<ISlotResolver> SlotResolvers = new() //排序

    {
        new Monk_Far(),
        new Monk_MasterfulBlitz(),
        new Monk_Selector(),
        new Monk_Rockbreaker(),
        new Monk_Four_pointFury(),
        new Monk_ArmOfTheDestroyer(),
        new Monk_TwinSnakes(),
        new Monk_Demolish(),
        new Monk_DragonKick(),
        new Monk_SnapPunch(),
        new Monk_TrueStrike(),
        new Monk_Bootshine(),
        
        new Monk_PerfectBalance(),
        new Monk_RiddleofFire(),
        new Monk_Brotherhood(),
        new Monk_RiddleofWind(),
        new Monk_HowlingFist(),
        new Monk_Meditation(),
        
        new Monk_Mantra(),
        new Feint(),
        new Monk_RiddleofEarth(),
        
        
        
    };
    


    public Rotation Build(string settingFolder)
    {
        MonkSettings.Build(settingFolder);
        return new Rotation(this, ()=>SlotResolvers)
            .AddOpener(GetOpener)
            .SetRotationEventHandler(new MonkRotationEventHandler())
            .AddSettingUIs(new MonkSettingView())
            .AddSlotSequences()
            .AddTriggerAction(new MonkTriggerActionSpell());
    }

    public void OnLanguageChanged(LanguageType languageType)
    {
    }

    public bool BuildQt(out JobViewWindow jobViewWindow)
    {
        jobViewWindow = new JobViewWindow(MonkSettings.Instance.JobViewSave, MonkSettings.Instance.save,
            OverlayTitle); // 这里设置一个静态变量.方便其他地方用
        JobViewWindow = jobViewWindow;
        jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
        jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
        jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
        
        jobViewWindow.AddQt("攒资源",false);
        jobViewWindow.AddQt("保持演武",true);
        jobViewWindow.AddQt("自动减伤",true);
        jobViewWindow.AddQt("爆发对齐",true);
        jobViewWindow.AddQt("AOE",true);
        
        jobViewWindow.AddHotkey("LB",new Monk_LB.LB(31399));
        jobViewWindow.AddHotkey("尼给路哒哟", new Sprint.TheSprint(29057));
        jobViewWindow.AddHotkey("无我", new HotKeyResolver_NormalSpell(16475, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7548, SpellTargetType.Self, false));
        
        return true;
    }
}