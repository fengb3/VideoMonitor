using MyBiliBiliMonitor_2;
using MyLib;
using MyLib.API;
using MyLib.Tool;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
#if DEBUG
        RunTimeVarHelper.PathHome = RunTimeVarHelper.PathHome =
            @".."                       + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." +
            Path.DirectorySeparatorChar + ".."                        + Path.DirectorySeparatorChar;
        Log.Register(ConsoleAppLog.ConsolePrint);
#endif
    }

    [Test]
    public async Task Test1()
    {
        // Assert.Pass();
        //"https://api.bilibili.com/x/web-interface/nav"

        var data = await APIHelper.SendRequest("https://api.bilibili.com/x/web-interface/nav", HttpMethod.Get, new());

        var wbiConsumer = new WbiConsumer();

        wbiConsumer.Consume(data);

        // var (a, b) = await APIHelper.GetWbiKeys();
        //
        // Console.WriteLine($"{a} \n{b}");
    }
}