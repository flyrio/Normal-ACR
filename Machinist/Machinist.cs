using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.GUI;
using Common.Language;
using ImGuiNET;

namespace Shiyuvi.Machinist;

public class MachinistOverlay
{
    private bool isHorizontal;
    
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
        
        ImGui.Text($"基本测试完成，有问题反馈Rio布鲁");
        ImGui.Text($"简易QA：");
        ImGui.Text($"ACR适配等级2-90级");
        ImGui.Text($"攒资源会暂停打除了123以外所有技能");
        ImGui.Text($"水中无法放技能时速行会狂点，右键开启按钮临时关闭ACR");
        ImGui.Text($"本ACR无起手设置，所有逻辑触发式运行，即：");
        ImGui.Text($"1.关攒资源，用整备，钻头好了会打掉");
        ImGui.Text($"2.关攒资源，开超荷，自动打热冲击，但不打野火、虹吸弹、弹射");
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