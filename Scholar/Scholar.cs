#region

using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.GUI;
using Common.Language;
using ImGuiNET;

#endregion

namespace Shiyuvi.Scholar;

public class ScholarOverlay
{
    private bool isHorizontal;
    
    public void DrawGeneral(JobViewWindow jobViewWindow)
    {
        
/*        if (ImGui.CollapsingHeader("杂项"))
        {
        }
待补充*/
        
        ImGui.Text($"感谢残光、Fra等大佬们在编写时提供的帮助");
        ImGui.Text($"本ACR有具体血量阈值等设置可在ACR设置界面中调整");
        ImGui.Text($"日随相关功能基本测试完毕");
        ImGui.Text($"如果发现任何问题欢迎及时与Rio布鲁联系");
        ImGui.Text($"----------------------------------------");
        ImGui.Text($"简单QA：");
        ImGui.Text($"罩子放怪脚下不勾选时会放自己脚下");
        ImGui.Text($"T绿帽选项为绿帽好了就给当前MT上一个");
        ImGui.Text($"自动召唤仅在战斗开始后/复活后会自动进行召唤");
        ImGui.Text($"跳崖的队友可能会拉不到");
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

    public void DrawControl(JobViewWindow jobViewWindow)
    {
        if (ImGui.BeginCombo("起手选择", ScholarSettings.Instance.Start))
        {
            if (ImGui.Selectable("无起手"))
            {
                ScholarSettings.Instance.Start = "无起手";
                ScholarSettings.Instance.save();
            }

            if (ImGui.Selectable("扩散盾起手"))
            {
                ScholarSettings.Instance.Start = "扩散盾起手";
                ScholarSettings.Instance.save();
            }

            if (ImGui.Selectable("幻光群盾起手"))
            {
                ScholarSettings.Instance.Start = "幻光群盾起手";
                ScholarSettings.Instance.save();
            }
            ImGui.EndCombo();
        }

        ImGui.Text("推荐倒计时15秒以上,扩散盾起手74级以上才可用");
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
            ImGui.Text($"多变复活层数:{Core.Get<IMemApiSpell>().GetCharges(29734)}");
            ImGui.Text($"QT状态:{AI.Instance.BattleData.NextSlot}");
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("小队"))
        {
            ImGui.Text($"小队人数：{PartyHelper.CastableParty.Count}");
            ImGui.Text($"小队坦克数量：{PartyHelper.CastableTanks.Count}");
            ImGui.TreePop();
        }
        ImGui.Text($"起手状态:{ScholarSettings.Instance.Start}");
    }
    

}
public static class Qt
{
    /// 获取指定名称qt的bool值
    public static bool GetQt(string qtName)
    {
        return ShiyuviScholarRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return ShiyuviScholarRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return ShiyuviScholarRotationEntry.JobViewWindow.SetQt(qtName ,qtValue);
    }

    /// 重置所有qt为默认值
    public static void Reset()
    {
        ShiyuviScholarRotationEntry.JobViewWindow.Reset();
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        ShiyuviScholarRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        ShiyuviScholarRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return ShiyuviScholarRotationEntry.JobViewWindow.GetQtArray();
    }
}