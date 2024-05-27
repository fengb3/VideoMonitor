using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using MyLib;
using MyLib.Helper;
using MyLib.Tool;

public static class WebAppLog
{
	private static ILogger? _logger;
	
	public static void SetLogger(this WebApplication app)
	{
	    _logger = app.Logger;
		
		Log.Register(ServerPrint);
		// Log.Register(WriteLogFile);
	}

	/// <summary>
	/// 打印到logger
	/// </summary>
	/// <param name="msg"> saf</param>
	/// <param name="level">asdf</param>
	private static void ServerPrint(string msg, Log.LogLevel level)
	{
		msg = $"[{TimeHelper.CurrentAscTime()}] {msg}";
		
	    if((level & Log.LogLevel.ERROR) > 0)
	        _logger?.LogError(msg);
	    else if((level & Log.LogLevel.WARNING) > 0)
	        _logger?.LogWarning(msg);
	    else if((level & Log.LogLevel.INFO) > 0)
	        _logger?.LogInformation(msg);
	    else if((level & Log.LogLevel.DEBUG) > 0)
	        _logger?.LogDebug(msg);
	}
	
	/// <summary>
	/// 写入日志文件
	/// </summary>
	/// <param name="msg"></param>
	/// <param name="level"></param>
	// [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.String; size: 166MB")]
	public static void WriteLogFile(string msg, Log.LogLevel level)
	{
		var sf         = new StackFrame(3, true);
		var fileName   = sf?.GetFileName()?.Split(Path.DirectorySeparatorChar).Last();
		var methodName = sf?.GetMethod()?.Name;
		var lineNumber = sf?.GetFileLineNumber();
    
		var toLogFile = $"{TimeHelper.CurrentAscTime()} {level} {msg} {fileName} {methodName} [line:{lineNumber}]";
    
		var logFile = Path.Combine(RunTimeVarHelper.PathHome, RunTimeVarHelper.Get("path_log_file").ToString() ?? $"{DateTime.Now:yyyyMMdd}.log");
    
		if (!File.Exists(logFile))
		{
			File.Create(logFile).Close();    
		}
        
		File.AppendAllText(logFile, toLogFile + Environment.NewLine, Encoding.GetEncoding("gb2312"));
	}
}