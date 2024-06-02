using System.Diagnostics;
using System.Text;
using Lib;
using Lib.Helper;
using Lib.Tool;

namespace Test;

public static class ConsoleAppLog
{
    /// <summary>
    /// 打印到控制台
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="level"></param>
    public static void ConsolePrint(string msg, Log.LogLevel level)
    {
        // Console.Write($"[{TimeHelper.CurrentAscTime()}]");
        Console.ForegroundColor = level switch
                                  {
                                      Log.LogLevel.DEBUG   => ConsoleColor.White,
                                      Log.LogLevel.INFO    => ConsoleColor.Green,
                                      Log.LogLevel.WARNING => ConsoleColor.Yellow,
                                      Log.LogLevel.ERROR   => ConsoleColor.Red,
                                      _                    => Console.ForegroundColor
                                  };

        var prefix = level switch
                     {
                         Log.LogLevel.DEBUG   => "[DEBUG] ",
                         Log.LogLevel.INFO    => "[INFO] ",
                         Log.LogLevel.WARNING => "[WARNING] ",
                         Log.LogLevel.ERROR   => "[ERROR] ",
                         _                    => "UNKNOWN"
                     };

        Console.WriteLine(prefix + msg);
        Console.ResetColor();
    }
    
    /// <summary>
    /// 写入日志文件
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="level"></param>
    public static void WriteLogFile(string msg, Log.LogLevel level)
    {
        var sf         = new StackFrame(3, true);
        var fileName   = sf?.GetFileName()?.Split(Path.DirectorySeparatorChar).Last();
        var methodName = sf?.GetMethod()?.Name;
        var lineNumber = sf?.GetFileLineNumber();
    
        var toLogFile = $"{TimeHelper.CurrentAscTime()} {fileName} {methodName} [line:{lineNumber}] {level} {msg}";
    
        // const string logPath = Config.PathHome;
        //
        // if (!Directory.Exists(logPath))
        // {
        //     Directory.CreateDirectory(logPath);
        // }
    
        var logFile = Path.Combine(RunTimeVarHelper.PathHome, RunTimeVarHelper.Get("path_log_file").ToString() ?? $"{DateTime.Now:yyyyMMdd}.log");
    
        if (!File.Exists(logFile))
        {
            File.Create(logFile).Close();    
        }
        
        File.AppendAllText(logFile, toLogFile + Environment.NewLine, Encoding.GetEncoding("gb2312"));
    
        sf = null;
    }
} 