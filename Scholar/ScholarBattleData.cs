using AEAssist.MemoryApi;
using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Scholar;

public class ScholarBattleData
{
    public static ScholarBattleData Instance = new();
    
    public void Reset()
    {
        Instance = new ScholarBattleData();
        SpellQueueGCD.Clear();
        SpellQueueAbility.Clear();
    }
    public Queue<Spell> SpellQueueGCD = new();
    public Queue<Spell> SpellQueueAbility = new();
}