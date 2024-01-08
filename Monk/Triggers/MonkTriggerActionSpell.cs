using CombatRoutine.TriggerModel;
using Common;
using Common.Define;
using Common.Language;
using ImGuiNET;

namespace Shiyuvi.Monk.Triggers;

public class MonkTriggerActionSpell : ITriggerAction
{
    public string DisplayName => "Monk/插入技能".Loc();
    public string Remark { get; set; }

    public SpellConfig SpellConfig { get; set; } = new();
    
    public bool Clear { get; set; } = new();
    
    public void Check()
    {
    }
    
    bool clear = false;
    
    public bool Draw()
    {
        if (Clear)
        {
            clear = Clear;
        }
        if (ImGui.Checkbox("是否清除队列", ref clear))
        {
            Clear = clear;
        }
        if (!clear)
        {
            SpellConfig.OnGUI();
        }
        return true;
    }
    
    public bool Handle()
    {
        if (Clear)
        {
            MonkBattleData.Instance.SpellQueueAbility.Clear();
            MonkBattleData.Instance.SpellQueueGCD.Clear();
            return true;
        }

        if (Core.Get<IMemApiSpell>().GetSpellType(SpellConfig.Create().Id) == SpellType.Ability)
        {
            MonkBattleData.Instance.SpellQueueAbility.Enqueue(SpellConfig.Create());
        }
        if (Core.Get<IMemApiSpell>().GetSpellType(SpellConfig.Create().Id) != SpellType.Ability)
        {
            MonkBattleData.Instance.SpellQueueGCD.Enqueue(SpellConfig.Create());
        }
        return true;
    }
}