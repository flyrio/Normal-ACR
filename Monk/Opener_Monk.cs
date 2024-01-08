using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Helper;

namespace Shiyuvi.Monk;

public class Opener_Monk : IOpener
{
    public void InitCountDown(CountDownHandler countDownHandler)
    {
        if (Core.Me.ClassLevel >= 15)
            countDownHandler.AddAction(10000, SpellsDefine.Meditation, SpellTargetType.Self);
        if (Core.Me.ClassLevel >= 52)
            countDownHandler.AddAction(5000, SpellsDefine.FormShift, SpellTargetType.Self);
        if (Core.Me.ClassLevel >= 35)
            countDownHandler.AddAction(350, SpellsDefine.Thunderclap, SpellTargetType.Target);
    }
    public Action CompeltedAction { get; set; }

    public List<Action<Slot>> Sequence { get; } = new()//起手具体的队列，这个例子中有7步，序号从0开始
    {
    };
    
    public int StopCheck(int index)//什么时候停止起手
    {
        return -1;
    }
    
    public int StartCheck()//什么时候会使用起手的检测
    {
        return 0;
    }

}