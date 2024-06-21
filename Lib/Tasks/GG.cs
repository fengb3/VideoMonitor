using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BiliBiliApi.Apis;
using Lib.DataManagement;
using Model;


namespace Lib.Tasks;

public class GG
{
    private IBilibiliApi _api { get; }
    private MonitorDbContext _dbContext { get; }

    public GG(IBilibiliApi bilibiliApi, MonitorDbContext context)
    {
        _api = bilibiliApi;
        _dbContext = context;
    }

    public async Task Run(params object[] @param)
    {
        if (@param.Length == 0)
            throw new ArgumentException("GG task needs at least one parameter.");

        if (!int.TryParse(@param[0].ToString(), out var uid))
            throw new ArgumentException("GG task needs an integer as the first parameter.");


        var gg = await _api.WebInterfaceCard(uid);

        var userRecord = new UserRecord
        {
            TimeStamp    = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
            FollowerNum  = gg.Data.Follower,
            FollowingNum = -1,
            ArchiveNum   = gg.Data.ArchiveCount,
            LikeNum      = gg.Data.LikeNum,
            Uid          = uid
        };

        Console.WriteLine(JsonSerializer.Serialize(userRecord, new JsonSerializerOptions()
        {
            WriteIndented = true
        }));

    }
}
