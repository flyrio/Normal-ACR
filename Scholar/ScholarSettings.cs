using System.Numerics;
using CombatRoutine.View.JobView;
using Common.Helper;

namespace Shiyuvi.Scholar;

public class ScholarSettings
{
    public static ScholarSettings Instance;
    private static string path;
    public bool AutoReset = true;
    
    public JobViewSave JobViewSave = new() { MainColor = new Vector4(168 / 255f, 20 / 255f, 20 / 255f, 0.8f) };//设置了QT界面的颜色

    public Dictionary<string, object> StyleSetting = new();//照抄

    public int Time = 100;
    public bool TP = false;
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "ScholarSettings.json");
        if (!File.Exists(path))
        {
            Instance = new ScholarSettings();
            Instance.save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<ScholarSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new ScholarSettings();
            LogHelper.Error(e.ToString());
        }
    }
    //默认阈值
    public float Aetherpact = 0.6f; //链子
    public float FeyBlessing = 0.7f; //祥光
    public float Indomitability = 0.65f; //不屈
    public float Lustrate = 0.45f; //活性法
    public float SummonSeraph = 0.55f; //大天使（带慰藉）
    public float WhisperingDawn = 0.75f; //低语
    public float Succor = 0.5f; //群盾
    public float Adloquium = 0.4f; //GCD单体治疗
    public float LowLevelSingleHeal = 0.7f; //低等级治疗
    public float BossDot = 0.03f; //BOSSDOT
    public float NotBossDot = 1.0f; //
    
    public int AOEHealCount = 2;
    
    public int time = 1500;
    public int stack = 3;

    public string Start = "无起手";
    public void save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path,JsonHelper.ToJson(this));
    }


}