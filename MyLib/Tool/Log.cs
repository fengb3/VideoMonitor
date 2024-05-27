using MyLib.Helper;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;

namespace MyLib.Tool;

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
        NECESSARY = INFO | WARNING | ERROR,
        WARNING_ONLY = WARNING | ERROR,
        ERROR_ONLY = ERROR,
        NONE = 0,
    }

    public static LogLevel Level { get; set; } = LogLevel.FULL;
    
    public static event LoggerTriggerEventHandler? LoggerTriggerEvent;

    // /// <summary> 
    // /// 日志触发事件
    // /// 输出到日志文件
    // /// </summary>
    // public static LoggerTriggerEventHandler? TriggerWriteLogFile;
    //
    // /// <summary>
    // /// 日志出发事件
    // /// 打印到控制台
    // /// </summary>
    // public static LoggerTriggerEventHandler? TriggerConsolePrint;

    /// <summary>
    /// 警告日志
    /// </summary>
    /// <param name="msg"></param>
    public static void Warning(object? msg)
    {
        if ((Level & LogLevel.WARNING) > 0)
            InvokeTrigger(msg?.ToString(), LogLevel.WARNING);
    }

    /// <summary>
    /// 调试日志
    /// </summary>
    /// <param name="msg"></param>
    public static void Debug(object? msg)
    {
        if ((Level & LogLevel.DEBUG) > 0)
            InvokeTrigger(msg?.ToString(), LogLevel.DEBUG);
    }

    /// <summary>
    /// info日志
    /// </summary>
    /// <param name="msg"></param>
    public static void Info(object? msg)
    {
        if ((Level & LogLevel.INFO) > 0)
            InvokeTrigger(msg?.ToString(), LogLevel.INFO);
    }

    /// <summary>
    /// 错误日志
    /// </summary>
    /// <param name="msg"></param>
    public static void Error(object? msg)
    {
        if ((Level & LogLevel.ERROR) > 0)
            InvokeTrigger(msg?.ToString(), LogLevel.ERROR);
    }

    /// <summary>
    /// 触发日志事件
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="level"></param>
    private static void InvokeTrigger(string? msg, LogLevel level)
    {
        LoggerTriggerEvent?.Invoke(msg, level);
        // TriggerConsolePrint?.Invoke(msg, level);
    }
    
    public static void Register(LoggerTriggerEventHandler handler, LogLevel mask = LogLevel.FULL)
    {
        Level = mask;

        LoggerTriggerEvent += handler;

        // TriggerWriteLogFile += WriteLogFile;
        // TriggerConsolePrint += ConsolePrint;
        // TriggerConsolePrint += handler;

        // 注册 encoding, 用于解决中文乱码问题
        Encoding.RegisterProvider((CodePagesEncodingProvider.Instance));
    }

   



    // private static void ServerPrint(string msg, LogLevel level)
    // {
    //     if((level & LogLevel.ERROR) > 0)
    //         _logger?.LogError(msg);
    //     else if((level & LogLevel.WARNING) > 0)
    //         _logger?.LogWarning(msg);
    //     else if((level & LogLevel.INFO) > 0)
    //         _logger?.LogInformation(msg);
    //     else if((level & LogLevel.DEBUG) > 0)
    //         _logger?.LogDebug(msg);
    // }

    /// <summary>
    /// 注册日志触发器
    /// </summary>
    // public static void Register(LogLevel mask = LogLevel.FULL)
    // {
    //     Level = mask;
    //
    //     TriggerWriteLogFile += WriteLogFile;
    //     TriggerConsolePrint += ConsolePrint;
    //
    //     // 注册 encoding, 用于解决中文乱码问题
    //     Encoding.RegisterProvider((CodePagesEncodingProvider.Instance));
    // }
    
    
}

/// <summary>
/// 日志触发委托
/// </summary>
public delegate void LoggerTriggerEventHandler(string msg, Log.LogLevel level);