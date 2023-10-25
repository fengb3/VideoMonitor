using System.Text.Json;
using Model.Attribute;
using SQLite;

namespace Model;

[SqliteTable]
public class UserRecord
{
    [PrimaryKey, AutoIncrement] public long UrId { get; set; } = 0;

    public long Uid { get; set; } = 0;

    public long TimeStamp { get; set; } = 0;

    public int FollowerNum { get; set; } = 0;

    public int FollowingNum { get; set; } = 0;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}