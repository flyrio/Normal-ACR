using System.Numerics;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;
using ImGuiNET;
using ImGuiScene;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_Succor
{
    public static CharacterAgent SuccorTarget()
    {
        return Core.Me;
    }

    public class Succor : IHotkeyResolver
    {
        private uint spellId;

        public Succor(uint spellId)
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
            SpellHelper.DrawSpellInfo(new Spell(3586, SuccorTarget), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (SpellsDefine.EmergencyTactics.IsReady())
                AI.Instance.BattleData.NextSlot.Add(
                        new Spell(SpellsDefine.EmergencyTactics.GetSpell().Id, SuccorTarget));//应急战术
            AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Succor.GetSpell().Id,SuccorTarget));//群盾  
        }

    }
}
