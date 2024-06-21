using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model;

/// <summary>
/// 用户记录
/// 包含用户在某个时间点的数据信息
/// </summary>
[Table("UserRecords")]
public class UserRecord
{
    /// <summary>
    /// 用户记录id
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long UrId { get; set; }
    
    /// <summary>
    /// 时间戳， Unix时间戳， 精确到毫秒， 此时间点的数据
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