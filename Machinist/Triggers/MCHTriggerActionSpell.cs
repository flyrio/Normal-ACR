using CombatRoutine.TriggerModel;
using Common;
using Common.Define;
using Common.Language;
using ImGuiNET;
using Shiyuvi.Machinist;

namespace Shiyuvi.Machinist.Triggers;

public class MCHTriggerActionSpell : ITriggerAction
{
    public string DisplayName => "MCH/插入技能".Loc();
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
            MachinistBattleData.Instance.SpellQueueAbility.Clear();
            MachinistBattleData.Instance.SpellQueueGCD.Clear();
            return true;
        }

        if (Core.Get<IMemApiSpell>().GetSpellType(SpellConfig.Create().Id) == SpellType.Ability)
        {
            MachinistBattleData.Instance.SpellQueueAbility.Enqueue(SpellConfig.Create());
        }
        if (Core.Get<IMemApiSpell>().GetSpellType(SpellConfig.Create().Id) != SpellType.Ability)
        {
            MachinistBattleData.Instance.SpellQueueGCD.Enqueue(SpellConfig.Create());
        }
        return true;
    }
}