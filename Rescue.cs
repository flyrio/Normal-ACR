using System.Numerics;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;
using ECommons;
using ImGuiNET;
using ImGuiScene;
using Shiyuvi.Scholar;

namespace Shiyuvi;

public class Rescue
{
    public static CharacterAgent GetRescueTarget()
    {
        var RescueTarget =PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHealth > 0 && !r.HasAura(2663) && !r.HasAura(7559) && !r.HasAura(7548))
            .OrderBy(r => r.Distance(PartyHelper.CastableAlliesWithin30.FirstOrDefault()))
            .LastOrDefault();
        return RescueTarget;
    }

    public class RescueTarget : IHotkeyResolver
    {
        private uint spellId;

        public RescueTarget(uint spellId)
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
            SpellHelper.DrawSpellInfo(new Spell(7571, GetRescueTarget), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.BattleStartTime > 0)
            {
                if (AI.Instance.BattleData.NextSlot == null)
                    AI.Instance.BattleData.NextSlot = new Slot();
                AI.Instance.BattleData.NextSlot.Add(new Spell(7571,GetRescueTarget));    
            }
            
        }
    }
}