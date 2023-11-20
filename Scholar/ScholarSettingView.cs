using CombatRoutine.View;
using Common.GUI;
using Common.Language;
using ImGuiNET;

namespace Shiyuvi.Scholar;

public class ScholarSettingView : ISettingUI
{
    public string Name => "学者";
    private bool setting;

    public void Draw()
    {
        ImGui.Text("如有设置上的建议欢迎及时与我反馈");
        ImGuiHelper.LeftInputInt("群奶人数", ref ScholarSettings.Instance.AOEHealCount);
        {
            ScholarSettings.Instance.save();
        }

        if (ImGui.SliderFloat("单盾阈值", ref ScholarSettings.Instance.Adloquium, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }

        if (ImGui.SliderFloat("群盾阈值", ref ScholarSettings.Instance.Succor, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }
        
        if (ImGui.SliderFloat("低语阈值", ref ScholarSettings.Instance.WhisperingDawn, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }

        if (ImGui.SliderFloat("不屈阈值", ref ScholarSettings.Instance.Indomitability, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }
        
        if (ImGui.SliderFloat("祥光阈值", ref ScholarSettings.Instance.FeyBlessing, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }
        
        if (ImGui.SliderFloat("大天使阈值", ref ScholarSettings.Instance.SummonSeraph, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }
        
        if (ImGui.SliderFloat("链子阈值", ref ScholarSettings.Instance.Aetherpact, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }

        if (ImGui.SliderFloat("活性法阈值", ref ScholarSettings.Instance.Lustrate, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }
        
        if (ImGui.SliderFloat("低等级单奶阈值", ref ScholarSettings.Instance.LowLevelSingleHeal, 0.0f, 0.99f))
        {
            ScholarSettings.Instance.save();
        }
        
        ImGui.Text("DOT阈值为0时无论怪多少血都会上DOT，为1时不会上DOT（小于39级时如果开启DOT和移动输出，会插入DOT走位");
        if (ImGui.SliderFloat("Boss低于多少血不上DOT", ref ScholarSettings.Instance.BossDot, 0.0f, 1.0f))
        {
            ScholarSettings.Instance.save();
        }
        
        if (ImGui.SliderFloat("小怪低于多少血不上DOT", ref ScholarSettings.Instance.NotBossDot, 0.0f, 1.0f))
        {
            ScholarSettings.Instance.save();
        }

        ImGui.Text("点击此按钮设置为默认阈值设置");
        if (ImGui.Button("默认设置"))
        {
            ScholarSettings.Instance.AOEHealCount = 2;
            ScholarSettings.Instance.Adloquium = 0.40f;
            ScholarSettings.Instance.Succor = 0.50f;
            ScholarSettings.Instance.WhisperingDawn = 0.75f;
            ScholarSettings.Instance.Indomitability = 0.65f;
            ScholarSettings.Instance.FeyBlessing = 0.70f;
            ScholarSettings.Instance.SummonSeraph = 0.55f;
            ScholarSettings.Instance.Aetherpact = 0.60f;
            ScholarSettings.Instance.Lustrate = 0.45f;
            ScholarSettings.Instance.LowLevelSingleHeal = 0.7f;
            ScholarSettings.Instance.BossDot = 0.03f;
            ScholarSettings.Instance.NotBossDot = 1.0f;
            ScholarSettings.Instance.save();
        }
    }
}