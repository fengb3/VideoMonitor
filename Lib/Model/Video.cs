using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model;

[Table("Videos")]
public class Video
{
    /// <summary>
    /// bv号
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string BvId { get; set; } = null!;

    /// <summary>
    /// 视频标题
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// 视频简介
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// 上传时间戳
    /// </summary>
    public long UploadTimeStamp { get; set; } = 0;

    /// <summary>
    /// 封面url
    /// </summary>
    public string CoverUrl { get; set; } = "";

    #region navgation - up主

    /// <summary>
    /// up主id
    /// </summary>
    public long AuthorUid { get; set; } = 0;

    /// <summary>
    /// up主
    /// </summary>
    [JsonIgnore]
    public virtual User? Author { get; set; } = null!;

    #endregion

    #region navgation - 视频记录

    /// <summary>
    /// 最近的视频记录id
    /// </summary>
    public long? MostRecentVideoRecordId { get; set; } = null;

    /// <summary>
    /// navigation property
    /// 最近的视频记录
    /// </summary>
    public virtual VideoRecord? MostRecentVideoRecord { get; set; } = null!;

    /// <summary>
    /// 全部视频记录
    /// </summary>
    public virtual ICollection<VideoRecord> VideoRecords { get; set; } = new List<VideoRecord>();

    #endregion

    #region navgation - 视频分区

    /// <summary>
    /// 视频分区id
    /// </summary>
    public int? TypeId { get; set; } = null;

    /// <summary>
    /// 视频分区
    /// </summary>
    [JsonIgnore]
    public virtual VideoType? Type { get; set; } = null!;

    #endregion
}