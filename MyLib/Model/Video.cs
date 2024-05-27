using System.Text.Json;
using SQLite;

namespace Model;

public class Video
{
    [PrimaryKey]
    public string BvId                    { get; set; } = "";

    public long   AuthorId                { get; set; } = 0;
    public int    TId                     { get; set; } = 0;
    public string Title                   { get; set; } = "";
    public string Description             { get; set; } = "";
    public long   UploadTimeStamp         { get; set; } = 0;
    public string CoverUrl                { get; set; } = "";
    public long   MostRecentVideoRecordId { get; set; } = 0;
    
    public override string ToString() => JsonSerializer.Serialize(this);
}