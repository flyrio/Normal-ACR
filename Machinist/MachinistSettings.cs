using System.Numerics;
using CombatRoutine.View.JobView;
using Common;
using Common.Helper;
using Shiyuvi.Machinist;

namespace Shiyuvi.Machinist;

public class MachinistSettings
{
    public static MachinistSettings Instance;
    private static string path;
    public bool AutoReset = true;
    
    public JobViewSave JobViewSave = new() { MainColor = new Vector4(168 / 255f, 20 / 255f, 20 / 255f, 0.8f) };//设置了QT界面的颜色

    public Dictionary<string, object> StyleSetting = new();//照抄

    public int Time = 100;
    public bool TP = false;
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "MachinistSettings.json");
        if (!File.Exists(path))
        {
            Instance = new MachinistSettings();
            Instance.save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<MachinistSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new MachinistSettings();
            LogHelper.Error(e.ToString());
        }
    }

    
    public void save()
    {

        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path,JsonHelper.ToJson(this));
    }


}