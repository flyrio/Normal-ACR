using Common.Define;
using Shiyuvi.Machinist;

namespace Shiyuvi.Machinist;

public class MachinistBattleData
{
    public static MachinistBattleData Instance = new();
    
    public void Reset()
    {
        Instance = new MachinistBattleData();
        SpellQueueGCD.Clear();
        SpellQueueAbility.Clear();
    }
    public Queue<Spell> SpellQueueGCD = new();
    public Queue<Spell> SpellQueueAbility = new();
}