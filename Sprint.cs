using System.Numerics;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using ImGuiNET;
using ImGuiScene;

namespace Shiyuvi;

public class Sprint
{
    public static CharacterAgent SprintTarget()
    {
        return Core.Me;
    }

    public class TheSprint : IHotkeyResolver
    {
        private uint spellId;

        public TheSprint(uint spellId)
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
            SpellHelper.DrawSpellInfo(new Spell(SpellsDefine.Sprint, SprintTarget), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (SpellsDefine.Sprint.IsReady())
                AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Sprint.GetSpell().Id, SprintTarget));
        }

    }
}
