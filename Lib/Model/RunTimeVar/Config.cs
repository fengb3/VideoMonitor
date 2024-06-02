using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.RunTimeVar;

public class Config
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] 
    public string Key { get; set; } = "";
    
    public string Value { get; set; } = "";
    
    public long LastTimeUpdate { get; set; } = 0;
}