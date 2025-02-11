﻿using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Language;
using Shiyuvi.Machinist;
using Shiyuvi.Machinist.Ability;
using Shiyuvi.Machinist.GCD;
using Shiyuvi.Machinist.Triggers;

namespace Shiyuvi;

public class ShiyuviMachinistRotationEntry : IRotationEntry
{
    public static JobViewWindow JobViewWindow;
    
    private readonly MachinistOverlay _lazyOverlay = new MachinistOverlay();
    public string OverlayTitle { get; } = "机工";
    
    private IOpener open = new Opener_MCH();
    
    private IOpener? GetOpener(uint level)//设置起手
    {
        return open;
    }
    
    public void DrawOverlay()
    {

    }
    
    public string AuthorName { get; } = "Shiyuvi";
    public Jobs TargetJob { get; } = Jobs.Machinist;

    public AcrType AcrType { get; } = AcrType.Normal;

    public List<ISlotResolver> SlotResolvers = new() //排序

    {
        

        new Machinist_Bioblaster(), //毒菌
        new Machinist_Drill(), //钻头
        new Machinist_HotShot(), //空气矛
        new Machinist_ChainSaw(), // 回转飞橘
        new Machinist_AutoCrossbow(), //自动弩
        new Machinist_HeatBlast(), //热冲击
        new Machinist_SpreadShot(), //散射
        new Machinist_CleanShot(), //基础连
        new Machinist_SlugShot(),
        new Machinist_SplitShot(),
        //new Machinist_GCD(),
        
        
        new Machinist_Wildfire(), //野火
        new Machinist_Tactician(), //策动
        new Machinist_Dismantle(), //武装解除
        new Machinist_Hypercharge(), //超荷
        new Machinist_Reassemble(), //整备
        new Machinist_BarrelStabilizer(), //加热
        new Machinist_RookAutoturret(), // robot
        new Machinist_Ricochet(), //弹射
        new Machinist_GaussRound(), // 虹吸弹
        new Machinist_HeadGraze(), //伤头
    };
    


    public Rotation Build(string settingFolder)
    {
        MachinistSettings.Build(settingFolder);
        return new Rotation(this, ()=>SlotResolvers)
            .AddOpener(GetOpener)
            .SetRotationEventHandler(new MachinistRotationEventHandler())
            .AddSettingUIs(new MachinistSettingView())
            .AddSlotSequences()
            .AddTriggerAction(new MCHTriggerActionSpell());
    }

    public void OnLanguageChanged(LanguageType languageType)
    {
    }

    public bool BuildQt(out JobViewWindow jobViewWindow)
    {
        jobViewWindow = new JobViewWindow(MachinistSettings.Instance.JobViewSave, MachinistSettings.Instance.save,
            OverlayTitle); // 这里设置一个静态变量.方便其他地方用
        JobViewWindow = jobViewWindow;
        jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
        jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
        jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
        jobViewWindow.AddQt("主动攻击",false);
        jobViewWindow.AddQt("自动减伤",true);
        jobViewWindow.AddQt("攒资源",false);
        jobViewWindow.AddQt("自动打断",true);
        jobViewWindow.AddQt("AOE",true);
        jobViewWindow.AddQt("自动速行",true);
        jobViewWindow.AddQt("回转飞锯",true);
        

        
        jobViewWindow.AddHotkey("LB", new HotKeyResolver_NormalSpell(24859, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7548, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("内丹", new HotKeyResolver_NormalSpell(7541, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("策动", new HotKeyResolver_NormalSpell(16889, SpellTargetType.Self, false));
        jobViewWindow.AddHotkey("扳手", new HotKeyResolver_NormalSpell(2887, SpellTargetType.Target, false));
        jobViewWindow.AddHotkey("超荷", new HotKeyResolver_NormalSpell(SpellsDefine.Hypercharge.GetSpell().Id, SpellTargetType.Target, false));
        jobViewWindow.AddHotkey("尼给路哒哟", new Sprint.TheSprint(29057));
        return true;
    }
}