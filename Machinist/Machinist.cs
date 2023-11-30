using System.Text.RegularExpressions;
using AEAssist.MemoryApi;
using CombatRoutine;
using CombatRoutine.Chat;
using CombatRoutine.TriggerModel;
using CombatRoutine.View.JobView;
using Common;
using Common.GUI;
using Common.Helper;
using Common.Language;
using ImGuiNET;
using Trust;

namespace Shiyuvi.Machinist;

public class MachinistOverlay
{
    private bool isHorizontal;
    private bool shencengmigong;

    public void DrawGeneral(JobViewWindow jobViewWindow)
    {

        if (ImGui.CollapsingHeader("插入技能状态"))
        {
            if (ImGui.Button("清除队列"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
            }

            ImGui.SameLine();
            if (ImGui.Button("清除一个"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
            }

            ImGui.Text("-------能力技-------");
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
                    ImGui.Text(spell.Name);
            ImGui.Text("-------GCD-------");
            if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_GCD)
                    ImGui.Text(spell.Name);
        }

/*        if (ImGui.CollapsingHeader("杂项"))
        {
        }
待补充*/


        if (ImGui.CollapsingHeader("说明书"))
        {
            ImGui.Text($"基本测试完成，有问题反馈Rio布鲁");
            ImGui.Text($"简易QA：");
            ImGui.Text($"ACR适配等级2-90级");
            ImGui.Text($"攒资源会暂停打除了123以外所有技能");
            ImGui.Text($"水中无法放技能时速行会狂点，右键开启按钮临时关闭ACR");
            ImGui.Text($"本ACR无起手设置，所有逻辑触发式运行，即：");
            ImGui.Text($"1.关攒资源，用整备，钻头好了会打掉");
            ImGui.Text($"2.关攒资源，开超荷，自动打热冲击，但不打野火、虹吸弹、弹射");
            ImGui.Text($"喷火器是BOSS上天时的玩具，平时请勿随便点击（会卡起手）");
        }

        

        //ImGui.Checkbox("主动攻击", ref zhudonggongji);
        //if (zhudonggongji == true) 
        //    Share.Pull = zhudonggongji;
        if (ImGui.CollapsingHeader("指定副本杀够怪自动停止攻击（开发中）"))
        {
            ImGui.Checkbox("深层迷宫", ref shencengmigong); //死宫，打开开关后，获取下一层门开了，自动关闭主动攻击，获取进入新一层，主动打开
            if (shencengmigong == true)
            {
                if (Regex.Match(ChatManager.Instance.CurrGameLog,"下一层提示信息|下一层提示信息2|下一层提示信息3").Success)
                    Share.Pull = false;
                if (Regex.Match(ChatManager.Instance.CurrGameLog,"进入新一层的提示文本信息|进入新一层的提示文本信息2|进入新一层的提示文本信息3").Success)
                    Share.Pull = true;
            }

            if (shencengmigong == false)
                Share.Pull = false;
        }

        



    }


    public void DrawTimeLine(JobViewWindow jobViewWindow)//时间轴全文照抄,后续调整改进
    {
        var currTriggerline = AI.Instance.TriggerlineData.CurrTriggerLine;
        var notice = "无";
        if (currTriggerline != null) notice = $"[{currTriggerline.Author}]{currTriggerline.Name}";

        ImGui.Text(notice);
        if (currTriggerline != null)
        {
            ImGui.Text("导出变量:".Loc());
            ImGui.Indent();
            foreach (var v in currTriggerline.ExposedVars)
            {
                var oldValue = AI.Instance.ExposedVars.GetValueOrDefault(v);
                ImGuiHelper.LeftInputInt(v, ref oldValue);
                AI.Instance.ExposedVars[v] = oldValue;
            }

            ImGui.Unindent();
        }
    }

    public void DrawDev(JobViewWindow jobViewWindow)//Dev，会在WarriorRotationEntry中调用，不懂的话建议全文照抄
    {
        if (ImGui.TreeNode("循环"))
        {
            ImGui.Text($"gcd是否可用：{AI.Instance.CanUseGCD()}");
            ImGui.Text($"gcd剩余时间：{AI.Instance.GetGCDCooldown()}");
            ImGui.Text($"gcd总时间：{AI.Instance.GetGCDDuration()}");
            ImGui.TreePop();
        }


        if (ImGui.TreeNode("技能释放"))
        {
            ImGui.Text($"上个技能：{Core.Get<IMemApiSpellCastSucces>().LastSpell}");
            ImGui.Text($"上个GCD：{Core.Get<IMemApiSpellCastSucces>().LastGcd}");
            ImGui.Text($"上个能力技：{Core.Get<IMemApiSpellCastSucces>().LastAbility}");
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("小队"))
        {
            ImGui.Text($"小队人数：{PartyHelper.CastableParty.Count}");
            ImGui.Text($"小队坦克数量：{PartyHelper.CastableTanks.Count}");
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("热量电量"))
        {
            ImGui.Text($"热量：{Core.Get<IMemApiMCH>().GetHeat()}");
            ImGui.Text($"电量：{Core.Get<IMemApiMCH>().GetBattery()}");
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("文本信息"))
        {
            ImGui.Text($"文本内容:{ChatManager.Instance.CurrGameLog}");
            ImGui.Text($"文本内容2:{ChatManager.Instance.CurrMsgType}");
            ImGui.Text($"文本内容3:{Core.Get<IMemApiChatMessage>()}");
            ImGui.TreePop();
        }
    }
    

}
public static class Qt
{
    /// 获取指定名称qt的bool值
    public static bool GetQt(string qtName)
    {
        return ShiyuviMachinistRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return ShiyuviMachinistRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return ShiyuviMachinistRotationEntry.JobViewWindow.SetQt(qtName ,qtValue);
    }

    /// 重置所有qt为默认值
    public static void Reset()
    {
        ShiyuviMachinistRotationEntry.JobViewWindow.Reset();
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        ShiyuviMachinistRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        ShiyuviMachinistRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return ShiyuviMachinistRotationEntry.JobViewWindow.GetQtArray();
    }
}