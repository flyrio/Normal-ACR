using System.Numerics;
using CombatRoutine.TriggerModel;
using Common.Language;
using ImGuiNET;

namespace Shiyuvi.Monk.Triggers;

public class Monk_QT : ITriggerAction
{

    public string DisplayName => "Monk/QT设置".Loc();

    private int 当前combo = 0;

    public string ValueName { get; set; } = new("");
    public bool Value { get; set; } = new();

    private int radioType;
    private int radioCheck;

    public string Remark { get; set; }
    public void Check()
    {
    }

    public bool Draw()
    {
        var qtArray = Qt.GetQtArray();
        当前combo = Array.IndexOf(qtArray, ValueName);
        if (当前combo == -1)
        {
            当前combo = 0;
        }
        radioCheck = Value ? 0 : 1;
        //return false;
        if (ImGui.BeginTabBar("###TriggerTab"))
        {
            if (ImGui.BeginTabItem("Machinist"))
            {
                ImGui.BeginChild("###TriggerSage", new Vector2(0, 0));

                //选择类型
                //ImGui.SetCursorPos(new Vector2(40,10));
                ImGui.RadioButton("Qt", ref radioType, 0);
                ImGui.NewLine();

                ImGui.SetCursorPos(new Vector2(0, 40));
                if (radioType == 0)
                {
                    ImGui.Combo("Qt开关", ref 当前combo, qtArray, qtArray.Length);
                    ValueName = qtArray[当前combo];
                    ImGui.RadioButton("开", ref radioCheck, 0);
                    ImGui.SameLine();
                    ImGui.RadioButton("关", ref radioCheck, 1);
                    Value = radioCheck == 0;
                }
                ImGui.EndChild();
                ImGui.EndTabItem();
            }
            ImGui.EndTabBar();
        }
        return true;
    }
    public bool Handle()
    {
        Qt.SetQt(ValueName, Value);
        return true;
    }
}