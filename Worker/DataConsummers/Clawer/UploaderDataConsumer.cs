using Lib.Attributes;
using Microsoft.Playwright;

namespace Lib.API.DataConsumer.Clawer;

public class UploaderDataConsumer
{
    private string _urlFormatter = "https://space.bilibili.com/{uid}/video";

    private readonly IBrowserContext _context;

    public UploaderDataConsumer()
    {
        var playWright = Playwright.CreateAsync().Result;
        var browser = playWright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
                                                      {
                                                          Headless = false,
                                                          SlowMo   = 50,
                                                      }).Result;
        _context = browser.NewContextAsync(new BrowserNewContextOptions
                                           {
                                               Permissions = new[] { "clipboard-read", "clipboard-write" }
                                           }).Result;
    }

    [Log]
    public async Task Consume(long uid)
    {
        var url = _urlFormatter.Replace("{uid}", uid.ToString());

        var page = await _context.NewPageAsync();

        await page.GotoAsync(url, new PageGotoOptions()
                            {
                                WaitUntil = WaitUntilState.NetworkIdle
                            });

        Console.WriteLine(page.ToString());

        await _context.CloseAsync();
    }
}