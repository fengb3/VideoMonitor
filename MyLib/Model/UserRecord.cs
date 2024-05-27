using System.Text.Json;
using SQLite;

namespace Model;

public class UserRecord
{
    [PrimaryKey, AutoIncrement] 
    public long UrId { get; set; } = 0;

    public long Uid { get; set; } = 0;

    public long TimeStamp { get; set; } = 0;

    public int FollowerNum { get; set; } = 0;

    public int FollowingNum { get; set; } = 0;
    
    public int ArchiveNum { get; set; } = 0;
    
    public int LikeNum { get; set; } = 0;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}