#define CONSOLE_APP

using MyBiliBiliMonitor_2;
using MyLib;
using MyLib.DataManagement;
using MyLib.Tool;


class Program
{
    public static async Task Main()
    {
        // var builder = WebApplication.CreateBuilder();

#if DEBUG
        // Log.Register(Log.LogLevel.FULL);
        Log.Register(ConsoleAppLog.ConsolePrint, Log.LogLevel.FULL);
        // Log.Register(ConsoleAppLog.WriteLogFile, Log.LogLevel.FULL);
        RunTimeVarHelper.PathHome = @".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." +
                                    Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar;
#elif RELEASE
        Log.Register(ConsoleAppLog.ConsolePrint, Log.LogLevel.WARNING_ONLY);
        Log.Register(ConsoleAppLog.WriteLogFile, Log.LogLevel.WARNING_ONLY);
#endif

        var x = DatabaseHandler.Instance;
    }
}