using Common.Define;

namespace Shiyuvi.Monk;

public class MonkBattleData
{
    public static MonkBattleData Instance = new();
    
    public void Reset()
    {
        Instance = new MonkBattleData();
        SpellQueueGCD.Clear();
        SpellQueueAbility.Clear();
    }
    public Queue<Spell> SpellQueueGCD = new();
    public Queue<Spell> SpellQueueAbility = new();
}