using Lib.Helper;
using System.Diagnostics;
using System.Text;

namespace Lib.Tool;

public static class Log
{
    /// <summary>
    /// 日志等级枚举
    /// </summary>
    [Flags]
    public enum LogLevel
    {
        DEBUG   = 1 << 1,
        INFO    = 1 << 2,
        WARNING = 1 << 3,
        ERROR   = 1 << 4,

        FULL = DEBUG | INFO | WARNING | ERROR,
        NONE = 0,
    }

    public static LogLevel Level { get; set; } = LogLevel.FULL;

    /// <summary> 
    /// 日志触发事件
    /// 输出到日志文件
    /// </summary>
    public static LoggerTriggerEventHandler? TriggerWriteLogFile;

    /// <summary>
    /// 日志出发事件
    /// 打印到控制台
    /// </summary>
    public static LoggerTriggerEventHandler? TriggerConsolePrint;

    /// <summary>
    /// 警告日志
    /// </summary>
    /// <param name="msg"></param>
    public static void Warning(string msg)
    {
        if ((Level & LogLevel.WARNING) > 0)
            InvokeTrigger(msg, LogLevel.WARNING);
    }

    /// <summary>
    /// 调试日志
    /// </summary>
    /// <param name="msg"></param>
    public static void Debug(string msg)
    {
        if ((Level & LogLevel.DEBUG) > 0)
            InvokeTrigger(msg, LogLevel.DEBUG);
    }

    /// <summary>
    /// info日志
    /// </summary>
    /// <param name="msg"></param>
    public static void Info(string msg)
    {
        if ((Level & LogLevel.INFO) > 0)
            InvokeTrigger(msg, LogLevel.INFO);
    }

    /// <summary>
    /// 错误日志
    /// </summary>
    /// <param name="msg"></param>
    public static void Error(string msg)
    {
        if ((Level & LogLevel.ERROR) > 0)
            InvokeTrigger(msg, LogLevel.ERROR);
    }

    /// <summary>
    /// 触发日志事件
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="level"></param>
    private static void InvokeTrigger(string msg, LogLevel level)
    {
        TriggerWriteLogFile?.Invoke(msg, level);
        TriggerConsolePrint?.Invoke(msg, level);
    }

    /// <summary>
    /// 打印到控制台
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="level"></param>
    private static void ConsolePrint(string msg, LogLevel level)
    {
        Console.Write($"[{TimeHelper.CurrentAscTime()}]");
        Console.ForegroundColor = level switch
                                  {
                                      LogLevel.DEBUG   => ConsoleColor.DarkGray,
                                      LogLevel.INFO    => ConsoleColor.White,
                                      LogLevel.WARNING => ConsoleColor.Yellow,
                                      LogLevel.ERROR   => ConsoleColor.Red,
                                      _                => Console.ForegroundColor
                                  };

        var prefix = level switch
                     {
                         LogLevel.DEBUG   => "[DEBUG] ",
                         LogLevel.INFO    => "[INFO] ",
                         LogLevel.WARNING => "[WARNING] ",
                         LogLevel.ERROR   => "[ERROR] ",
                         _                => "UNKNOWN"
                     };

        Console.WriteLine(prefix + msg);
        Console.ResetColor();
    }

    /// <summary>
    /// 写入日志文件
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="level"></param>
    private static void WriteLogFile(string msg, LogLevel level)
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

        var logFile = Path.Combine(".", Config.Get("path_log_file").ToString() ?? $"{DateTime.Now:yyyyMMdd}.log");

        File.AppendAllText(logFile, toLogFile + Environment.NewLine, Encoding.GetEncoding("gb2312"));

        sf = null;
    }

    /// <summary>
    /// 注册日志触发器
    /// </summary>
    public static void Register(LogLevel mask = LogLevel.FULL)
    {
        Level = mask;

        TriggerWriteLogFile += WriteLogFile;
        TriggerConsolePrint += ConsolePrint;

        // 注册 encoding, 用于解决中文乱码问题
        Encoding.RegisterProvider((CodePagesEncodingProvider.Instance));
    }
}

/// <summary>
/// 日志触发委托
/// </summary>
public delegate void LoggerTriggerEventHandler(string msg, Log.LogLevel level);