using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Shiyuvi.Scholar;

namespace Shiyuvi.Machinist;

public class Opener_MCH : IOpener
{
    public void InitCountDown(CountDownHandler countDownHandler)
    {
        if (Core.Me.ClassLevel >= 10)
            countDownHandler.AddAction(5000, SpellsDefine.Reassemble, SpellTargetType.Self);
        if (Qt.GetQt("爆发药"))
            countDownHandler.AddPotionAction(3000);//爆发药
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