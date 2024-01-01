using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Helper;
using ImGuiNET;

namespace Shiyuvi.Scholar;

public class Opener_Scholar : IOpener
{
    public void InitCountDown(CountDownHandler countDownHandler)
    {
        if (!Core.Get<IMemApiScholar>().HasPet)
            countDownHandler.AddAction(18000,SpellsDefine.SummonEos,SpellTargetType.Self);//宝宝
        if (ScholarSettings.Instance.Start == "扩散盾起手") if (Core.Me.ClassLevel >= 74)
        { 
            countDownHandler.AddAction(15000,SpellsDefine.FeyIllumination,SpellTargetType.Self);//幻光
            countDownHandler.AddAction(10000,SpellsDefine.Recitation,SpellTargetType.Self);//秘策
            if (Core.Me.ClassLevel >= 86) 
                countDownHandler.AddAction(9000,SpellsDefine.Protraction,SpellTargetType.Self);//回升
            countDownHandler.AddAction(8000,SpellsDefine.Adloquium,SpellTargetType.Self);//单盾
            countDownHandler.AddAction(5000,SpellsDefine.DeploymentTactics,SpellTargetType.Self);//扩散
        }
        if (ScholarSettings.Instance.Start == "幻光群盾起手" && Core.Me.ClassLevel >= 40) 
        { 
             countDownHandler.AddAction(15000,SpellsDefine.FeyIllumination,SpellTargetType.Self);//幻光
             countDownHandler.AddAction(10000,SpellsDefine.Succor,SpellTargetType.Self);//群盾
        } 
        if (Qt.GetQt("爆发药"))
            countDownHandler.AddPotionAction(3000);//爆发药
        countDownHandler.AddAction(1400,SpellsDefine.SchRuin,SpellTargetType.Target);//预读条
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