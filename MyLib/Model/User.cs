using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using SQLite;

namespace Model;

public class User
{
    [PrimaryKey] public long Uid { get; set; } = 0;
    
    public string Name { get; set; } = "";

    public string FaceUrl { get; set; } = "";
    
    public long MostRecentUserRecordId { get; set; } = 0;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}