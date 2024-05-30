using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model;

[Table("UserRecords")]
public class UserRecord
{
    /// <summary>
    /// 
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long UrId { get; set; }
    
    /// <summary>
    /// 时间戳
    /// </summary>
    public long TimeStamp { get; set; }

    /// <summary>
    /// 粉丝数
    /// </summary>
    public int FollowerNum { get; set; } 

    /// <summary>
    /// 关注数
    /// </summary>
    public int FollowingNum { get; set; } 

    /// <summary>
    /// 发布的视频数
    /// </summary>
    public int ArchiveNum { get; set; } 

    /// <summary>
    /// 点赞数
    /// </summary>
    public int LikeNum { get; set; } 
    
    /// <summary>
    /// 用户id
    /// </summary>
    public long Uid { get; set; }
    
    /// <summary>
    /// navigation property
    /// 用户
    /// </summary>
    [JsonIgnore]
    public virtual User? User { get; set; }
}