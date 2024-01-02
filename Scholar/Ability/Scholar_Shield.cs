using System.Numerics;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;
using ImGuiNET;
using ImGuiScene;


namespace Shiyuvi.Scholar.Ability;

public class Scholar_Shield
{
    public static CharacterAgent Getshield()
    {
        if (Qt.GetQt("罩子放怪脚下"))
            return Core.Me.GetCurrTarget();
        return Core.Me;
    }

    public class Shield : IHotkeyResolver
    {
        private uint spellId;

        public Shield(uint spellId)
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
            SpellHelper.DrawSpellInfo(new Spell(188, Getshield()), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            AI.Instance.BattleData.NextSlot.Add(new Spell(188,Getshield));
        }
    }
}