using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model;

[Table("Users")]
public class User
{
    /// <summary>
    /// 用户id
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Uid { get; set; } = 0;

    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 头像url
    /// </summary>
    public string FaceUrl { get; set; } = "";

    /// <summary>
    /// 最近的用户记录id
    /// </summary>
    public long? MostRecentUserRecordId { get; set; } = null;

    /// <summary>
    /// navigation property
    /// 最近的用户记录
    /// </summary>
    public UserRecord? MostRecentUserRecord { get; set; } = null!;

    /// <summary>
    /// 所有的用户记录
    /// </summary>
    public virtual ICollection<UserRecord> UserRecords { get; set; } = new List<UserRecord>();
    
    /// <summary>
    /// 所有的视频
    /// </summary>
    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}