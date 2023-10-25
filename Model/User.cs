using System.Text.Json;
using SQLite;

namespace Model;

public class User
{
    [PrimaryKey] public long Uid { get; set; } = 0;

    public string Name { get; set; } = "";

    public string FaceUrl { get; set; } = "";

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}