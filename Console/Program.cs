using Lib.Tool;
using Model;
using Model.DataManage;

namespace Console;

public static class Program
{
    public static void Main(string[] args)
    {
        // 注册日志
        Log.Register();

        AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
        {
            Log.Error(eventArgs.Exception.ToString());
        };

        // 读取json文件
        JsonReader.Read();
    }
}