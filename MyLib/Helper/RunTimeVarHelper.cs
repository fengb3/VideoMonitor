using System.Reflection;
using System.Text.Json;
using Model.RunTimeVar;
using MyLib.DataManagement;
using MyLib.Helper;
using MyLib.Tool;

namespace MyLib;

public static class RunTimeVarHelper
{
    public static string PathHome = @".." + Path.DirectorySeparatorChar;
// #if DEBUG
//     public static readonly string PathHome = @"../../../.." + Path.DirectorySeparatorChar;
// #elif RELEASE
//     public static readonly string PathHome = @".." + Path.DirectorySeparatorChar;+Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar;
// #endif

    // private const string FILE_NAME_CONFIG = @"config.json";
    //
    // private static Dictionary<string, JsonElement>? _config;

    private static bool _isInitialized = false;

    private static void Load()
    {
        
        #region Initialize config with default value

        if (!DatabaseHandler.Instance.TryGetData(c => c.Key == "cookie_url", out Config config))
        {
            config = new Config()
                     {
                         Key            = "https://api.bilibili.com",
                         Value          = "",
                         LastTimeUpdate = DateTime.Now.ToTimeStamp()
                     };

            DatabaseHandler.Instance.AddData(config);
        }
        
        if (!DatabaseHandler.Instance.TryGetData(c => c.Key == "cookie_SESSDATA", out Config config1))
        {
            config1 = new Config()
                     {
                         Key            = "cookie_SESSDATA",
                         Value          = "",
                         LastTimeUpdate = DateTime.Now.ToTimeStamp()
                     };

            DatabaseHandler.Instance.AddData(config1);
        }

        if (!DatabaseHandler.Instance.TryGetData(c => c.Key == "cookie_buvid3", out Config cookie2))
        {
            cookie2 = new Config()
                      {
                          Key            = "cookie_buvid3",
                          Value          = "",
                          LastTimeUpdate = DateTime.Now.ToTimeStamp()
                      };

            DatabaseHandler.Instance.AddData(cookie2);
        }

        if (!DatabaseHandler.Instance.TryGetData(c => c.Key == "cookie_buvid4", out Config cookie3))
        {
            cookie3 = new Config()
                      {
                          Key            = "cookie_buvid4",
                          Value          = "",
                          LastTimeUpdate = DateTime.Now.ToTimeStamp()
                      };

            DatabaseHandler.Instance.AddData(cookie3);
        }

        #endregion

        _isInitialized = true;
    }

    public static string Get(string? key)
    {
        if (!_isInitialized)
            Load();

        if (key == null)
            Log.Error($"获取运行变量失败 - key 为 null");

        if (!DatabaseHandler.Instance.TryGetData(c => c.Key == key, out Config config))
        {
            Log.Error($"获取运行变量失败 - key: {key} 未找到");
        }

        return config.Value;
        
    }
}