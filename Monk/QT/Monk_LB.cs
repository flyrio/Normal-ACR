using System.Numerics;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;
using ImGuiNET;
using ImGuiScene;

namespace Shiyuvi.Monk.QT;

public class Monk_LB 
{
    public static CharacterAgent LBTarget()
    {
        return Core.Me.GetCurrTarget();
    }

    public class LB : IHotkeyResolver
    {
        private uint spellId;

        public LB(uint spellId)
        {
            this.spellId = spellId;
        }

        public void Draw(Vector2 size)
        {
            Vector2 size1 = size * 0.8f;
            ImGui.SetCursorPos(size * 0.1f);
            TextureWrap textureWrap;
            if (!Core.Get<IMemApiIcon>().GetActionTexture(spellId, out textureWrap))
                return;
            ImGui.Image(textureWrap.ImGuiHandle,size1);
        }

        public void DrawExternal(Vector2 size, bool isActive) =>
            SpellHelper.DrawSpellInfo(new Spell(31399, LBTarget), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (Core.Me.GetCurrTarget().CanAttack)
            {
                if (Core.Me.ClassLevel >= 80)
                    AI.Instance.BattleData.NextSlot.Add(
                    new Spell(SpellsDefine.SixSidedStar.GetSpell().Id, LBTarget));//星导脚
                if (Core.Get<IMemApiLimitBreak>().GetLimitBreakCurrentValue() >= (ushort) 30000 || Core.Get<IMemApiLimitBreak>().GetLimitBreakCurrentValue() == (ushort) 0)
                    AI.Instance.BattleData.NextSlot.Add(new Spell(202,LBTarget));//LB 
                if (Core.Get<IMemApiLimitBreak>().GetLimitBreakCurrentValue() >= (ushort) 20000)
                    AI.Instance.BattleData.NextSlot.Add(new Spell(201,LBTarget));//LB 
                if (Core.Get<IMemApiLimitBreak>().GetLimitBreakCurrentValue() >= (ushort) 10000)
                    AI.Instance.BattleData.NextSlot.Add(new Spell(200,LBTarget));//LB 
            }
        }

    }
}
