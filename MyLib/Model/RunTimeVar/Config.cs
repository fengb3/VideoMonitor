using SQLite;

namespace Model.RunTimeVar;

public class Config
{
    [PrimaryKey] 
    public string Key { get; set; } = "";
    
    public string Value { get; set; } = "";
    
    public long LastTimeUpdate { get; set; } = 0;
}