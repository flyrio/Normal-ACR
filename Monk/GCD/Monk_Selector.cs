using CombatRoutine;
using CombatRoutine.TriggerModel;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk.GCD;

public class Monk_Selector : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;


    public Spell GetSpell()

    {
        if (TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5) > 3 && Qt.GetQt("AOE"))
        {
            if (Core.Get<IMemApiMonk>().ActiveNadi == NaDi.None ||
                Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Both ||
                Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Solar) //130威力AOE
            {
                if (Core.Me.ClassLevel < 82)
                    return SpellsDefine.Rockbreaker.GetSpell();
                return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.ArmOfTheDestroyer.GetSpell().Id).GetSpell();
            }

            if (Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Lunar)
            {
                if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.None)
                    return SpellsDefine.FourPointFury.GetSpell();
                if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.None && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor)
                    return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.ArmOfTheDestroyer.GetSpell().Id).GetSpell();
                if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.None && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl)
                    return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.ArmOfTheDestroyer.GetSpell().Id).GetSpell();
                if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.None && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo)
                    return SpellsDefine.Rockbreaker.GetSpell();
                if (Core.Get<IMemApiMonk>().MastersGauge[2] == ChakraType.None)
                {
                    if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl)
                        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.ArmOfTheDestroyer.GetSpell().Id).GetSpell();
                    if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo)
                        return SpellsDefine.Rockbreaker.GetSpell();
                }
                if (Core.Get<IMemApiMonk>().MastersGauge[2] == ChakraType.None)
                {
                    if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor)
                        return Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.ArmOfTheDestroyer.GetSpell().Id).GetSpell();
                    if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo)
                        return SpellsDefine.FourPointFury.GetSpell();
                }
                if (Core.Get<IMemApiMonk>().MastersGauge[2] == ChakraType.None)
                {
                    if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor)
                        return SpellsDefine.Rockbreaker.GetSpell();
                    if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo && Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl)
                        return SpellsDefine.FourPointFury.GetSpell();
                }
            }
        }

        if (Core.Me.ClassLevel >= 50 && Core.Me.ClassLevel < 60)
        {
            if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                return SpellsDefine.TwinSnakes.GetSpell();
            if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246,6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                return SpellsDefine.Demolish.GetSpell();
        }

        if (Core.Get<IMemApiMonk>().ActiveNadi == NaDi.None) //选择处理方案
        {
            if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo && //保阴
                Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo)
            {
                if (Core.Me.HasMyAura(1861))
                    return SpellsDefine.Bootshine.GetSpell();
                return SpellsDefine.DragonKick.GetSpell();
            }
            
            if (Core.Get<IMemApiMonk>().MastersGauge[0] != Core.Get<IMemApiMonk>().MastersGauge[1] && Core.Get<IMemApiMonk>().MastersGauge[1] != ChakraType.None) //
            {
                if (Core.Get<IMemApiMonk>().MastersGauge[2] == ChakraType.None)
                {
                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo))
                    {
                        if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                            return SpellsDefine.Demolish.GetSpell();
                        return SpellsDefine.SnapPunch.GetSpell();
                    }

                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo))
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        return SpellsDefine.TrueStrike.GetSpell();
                    }

                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor))
                    {
                        if (Core.Me.HasMyAura(1861))
                            return SpellsDefine.Bootshine.GetSpell();
                        return SpellsDefine.DragonKick.GetSpell();
                    }
                    
                }
            

            }

            if (Core.Me.ClassLevel >= 70 && SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds > 38000 &&
                (SpellsDefine.Brotherhood.GetSpell().Cooldown.TotalMilliseconds < 10000) && Qt.GetQt("爆发对齐"))
            {
                //三阳
                if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.None)
                {
                    if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                        return SpellsDefine.Demolish.GetSpell(); //2
                    if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                        return SpellsDefine.TwinSnakes.GetSpell();
                    if (Core.Me.HasMyAura(1861)) //3
                        return SpellsDefine.Bootshine.GetSpell();
                    return SpellsDefine.DragonKick.GetSpell(); //1
                }

                if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.None)
                {
                    if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo)
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                            return SpellsDefine.Demolish.GetSpell();
                        return SpellsDefine.TrueStrike.GetSpell();
                    }

                    if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor)
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        if (Core.Me.HasMyAura(1861))
                            return SpellsDefine.Bootshine.GetSpell();
                        return SpellsDefine.DragonKick.GetSpell();
                    }

                    if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl)
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        if (Core.Me.HasMyAura(1861))
                            return SpellsDefine.Bootshine.GetSpell();
                        return SpellsDefine.DragonKick.GetSpell();
                    }
                }
                if (Core.Get<IMemApiMonk>().MastersGauge[2] == ChakraType.None)
                {
                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo))
                    {
                        if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                            return SpellsDefine.Demolish.GetSpell();
                        return SpellsDefine.SnapPunch.GetSpell();
                    }

                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo))
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        return SpellsDefine.TrueStrike.GetSpell();
                    }

                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor))
                    {
                        if (Core.Me.HasMyAura(1861))
                            return SpellsDefine.Bootshine.GetSpell();
                        return SpellsDefine.DragonKick.GetSpell();
                    }
                }

            }
            if (Core.Me.ClassLevel < 68 || (SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds > 38000))
            {
                if (Core.Me.HasMyAura(1861))
                    return SpellsDefine.Bootshine.GetSpell();
                return SpellsDefine.DragonKick.GetSpell();
            } //三阴

            if (SpellsDefine.RiddleofFire.GetSpell().Cooldown.TotalMilliseconds <= 38000 && Core.Me.ClassLevel >= 68)
            {
                //三阳
                if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.None)
                {
                    if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                        return SpellsDefine.Demolish.GetSpell(); //2
                    if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                        return SpellsDefine.TwinSnakes.GetSpell();
                    if (Core.Me.HasMyAura(1861)) //3
                        return SpellsDefine.Bootshine.GetSpell();
                    return SpellsDefine.DragonKick.GetSpell(); //1
                }

                if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.None)
                {
                    if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo)
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                            return SpellsDefine.Demolish.GetSpell();
                        return SpellsDefine.TrueStrike.GetSpell();
                    }

                    if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor)
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        if (Core.Me.HasMyAura(1861))
                            return SpellsDefine.Bootshine.GetSpell();
                        return SpellsDefine.DragonKick.GetSpell();
                    }

                    if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl)
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        if (Core.Me.HasMyAura(1861))
                            return SpellsDefine.Bootshine.GetSpell();
                        return SpellsDefine.DragonKick.GetSpell();
                    }
                }
                if (Core.Get<IMemApiMonk>().MastersGauge[2] == ChakraType.None)
                {
                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo))
                    {
                        if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                            return SpellsDefine.Demolish.GetSpell();
                        return SpellsDefine.SnapPunch.GetSpell();
                    }

                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo))
                    {
                        if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                            return SpellsDefine.TwinSnakes.GetSpell();
                        return SpellsDefine.TrueStrike.GetSpell();
                    }

                    if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl) ||
                        (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl &&
                         Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor))
                    {
                        if (Core.Me.HasMyAura(1861))
                            return SpellsDefine.Bootshine.GetSpell();
                        return SpellsDefine.DragonKick.GetSpell();
                    }
                }

            }
        }

        if (Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Solar) //阳
        {
            if (Core.Me.HasMyAura(1861))
                return SpellsDefine.Bootshine.GetSpell();
            return SpellsDefine.DragonKick.GetSpell();
        }




        if (Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Lunar) //阴
        {
            if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.None)
            {
                if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                    return SpellsDefine.Demolish.GetSpell(); //2
                if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                    return SpellsDefine.TwinSnakes.GetSpell();
                if (Core.Me.HasMyAura(1861)) //3
                    return SpellsDefine.Bootshine.GetSpell();
                return SpellsDefine.DragonKick.GetSpell(); //1
            }

            if (Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.None)
            {
                if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo)
                {
                    if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                        return SpellsDefine.TwinSnakes.GetSpell();
                    if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                        return SpellsDefine.Demolish.GetSpell();
                    return SpellsDefine.TrueStrike.GetSpell();
                }

                if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor)
                {
                    if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                        return SpellsDefine.TwinSnakes.GetSpell();
                    if (Core.Me.HasMyAura(1861))
                        return SpellsDefine.Bootshine.GetSpell();
                    return SpellsDefine.DragonKick.GetSpell();
                }

                if (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl)
                {
                    if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                        return SpellsDefine.TwinSnakes.GetSpell();
                    if (Core.Me.HasMyAura(1861))
                        return SpellsDefine.Bootshine.GetSpell();
                    return SpellsDefine.DragonKick.GetSpell();
                }
            }

            if (Core.Get<IMemApiMonk>().MastersGauge[2] == ChakraType.None)
            {
                if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo &&
                     Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor) ||
                    (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor &&
                     Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo))
                {
                    if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                        return SpellsDefine.Demolish.GetSpell();
                    return SpellsDefine.SnapPunch.GetSpell();
                }

                if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.OpoOpo &&
                     Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl) ||
                    (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl &&
                     Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.OpoOpo))
                {
                    if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                        return SpellsDefine.TwinSnakes.GetSpell();
                    return SpellsDefine.TrueStrike.GetSpell();
                }

                if ((Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Raptor &&
                     Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Coeurl) ||
                    (Core.Get<IMemApiMonk>().MastersGauge[0] == ChakraType.Coeurl &&
                     Core.Get<IMemApiMonk>().MastersGauge[1] == ChakraType.Raptor))
                {
                    if (Core.Me.HasMyAura(1861))
                        return SpellsDefine.Bootshine.GetSpell();
                    return SpellsDefine.DragonKick.GetSpell();
                }
            }
        }

        if (Core.Get<IMemApiMonk>().ActiveNadi == NaDi.Both) //任意处理方案
        {
            if (!Core.Me.HasMyAuraWithTimeleft(3001, 6000))
                return SpellsDefine.TwinSnakes.GetSpell();
            if (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(246, 6000) && !SpellsDefine.Demolish.RecentlyUsed(2500))
                return SpellsDefine.Demolish.GetSpell();
            if (Core.Me.HasMyAura(1861))
                return SpellsDefine.Bootshine.GetSpell();
            return SpellsDefine.DragonKick.GetSpell();
        }

        return null;
    }

    public int Check()
    {
        if (Core.Get<IMemApiSpell>().GetActionInRangeOrLoS(SpellsDefine.Bootshine) == 566) return -1;
        if (Core.Me.HasMyAura(110)) return 0;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }
}