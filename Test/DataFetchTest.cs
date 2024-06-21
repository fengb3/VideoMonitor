using BiliBiliApi.Apis;
using Lib.DataManagement;
using Lib.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataBase_Releation;

namespace Test;

[TestFixture]
internal class DataFetchTest
{
    private MonitorDbContext _context;
    private IBilibiliApi _api;

    [SetUp]
    public void Setup()
    {
        _context = DataBaseInitailizer.GetContext();
        var services = new ServiceCollection();
        services.AddBilibiliApiSolid();
        var serviceProvider = services.BuildServiceProvider();
        _api = serviceProvider.GetRequiredService<IBilibiliApi>();

    
    }

    [Test]
    public async Task Test()
    {
        GG gg = new GG(_api, _context);

        await gg.Run(1);
    }
}
