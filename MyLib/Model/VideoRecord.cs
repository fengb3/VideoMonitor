using System.Text.Json;
using SQLite;

namespace Model;

public class VideoRecord
{
    [PrimaryKey, AutoIncrement] public long VrId { get; set; } = 0;

    public string BvId { get; set; } = "";  

    public long TimeStamp { get; set; } = 0;

    public int Likes { get; set; } = 0;

    public int Dislikes { get; set; } = 0; // this is one is always 0

    public int Coins { get; set; } = 0;

    public int Favorites { get; set; } = 0;

    public int Shares { get; set; } = 0;

    public int BulletComments { get; set; } = 0;

    public int Comments { get; set; } = 0;

    public int Views { get; set; } = 0;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}