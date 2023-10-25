using System.Text.Json;
using Model.Attribute;
using SQLite;

namespace Model;

public class VideoType
{
    [PrimaryKey] public int TId { get; set; } = 0;

    public string Name { get; set; } = "";

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}