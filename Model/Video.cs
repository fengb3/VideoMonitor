using System.Text.Json;
using Model.Attribute;
using SQLite;

namespace Model;

public class Video
{
    [PrimaryKey] public string BvId { get; set; } = "";

    public long AuthorId { get; set; } = 0;

    public int TId { get; set; } = 0;

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public long UploadTimeStamp { get; set; } = 0;

    public string CoverUrl { get; set; } = "";

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}