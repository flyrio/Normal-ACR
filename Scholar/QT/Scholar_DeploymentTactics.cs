using System.Numerics;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;
using ImGuiNET;
using ImGuiScene;

namespace Shiyuvi.Scholar.Ability;

public class Scholar_DeploymentTactics
{
    public static CharacterAgent DeploymentTacticsTarget()
    {
        return Core.Me;
    }

    public class DeploymentTactics : IHotkeyResolver
    {
        private uint spellId;

        public DeploymentTactics(uint spellId)
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
            SpellHelper.DrawSpellInfo(new Spell(3585, DeploymentTacticsTarget), size, isActive);

        public int Check() => 0;

        public void Run()
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            if (SpellsDefine.DeploymentTactics.IsReady() &&
                (SpellsDefine.Recitation.IsReady() || Core.Me.HasAura(1896)))
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Recitation.GetSpell().Id,DeploymentTacticsTarget) ); //秘策
                if ((!SpellsDefine.Swiftcast.IsReady()) && (!SpellsDefine.Protraction.IsReady()))
                    AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Adloquium.GetSpell().Id,DeploymentTacticsTarget));//单盾
                else
                {
                    if (Core.Me.GetCurrTarget().CanAttack)
                        AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.SchRuin2.GetSpell().Id,SpellTargetType.Target)); //毁坏
                    if (SpellsDefine.Swiftcast.IsReady())
                        AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Swiftcast.GetSpell().Id,DeploymentTacticsTarget())); //即刻
                    if (SpellsDefine.Protraction.IsReady())
                        AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Protraction.GetSpell().Id,DeploymentTacticsTarget())); //回升
                    AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Adloquium.GetSpell().Id,DeploymentTacticsTarget));//单盾
                }
                AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.DeploymentTactics.GetSpell().Id,DeploymentTacticsTarget)); //扩散
            }

            if (!(SpellsDefine.DeploymentTactics.IsReady() && (SpellsDefine.Recitation.IsReady() || Core.Me.HasAura(1896)) && SpellsDefine.Swiftcast.IsReady()))
            {
                AI.Instance.BattleData.NextSlot.Add(new Spell(SpellsDefine.Succor.GetSpell().Id,DeploymentTacticsTarget));//群盾
            }
    
        }
    }
}