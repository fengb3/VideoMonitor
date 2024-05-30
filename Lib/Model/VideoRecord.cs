using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model;

[Table("VideoRecords")]
public class VideoRecord
{
    /// <summary>
    /// 视频记录id
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long VrId { get; set; } = 0;


    /// <summary>
    /// 视频记录时间戳
    /// </summary>
    public long TimeStamp { get; set; } = 0;

    /// <summary>
    /// 点赞
    /// </summary>
    public int Likes { get; set; } = 0;

    /// <summary>
    /// 点踩
    /// </summary>
    /// <注>这个值一直是0</注>
    public int Dislikes { get; set; } = 0; 

    /// <summary>
    /// 投币
    /// </summary>
    public int Coins { get; set; } = 0;

    /// <summary>
    /// 收藏
    /// </summary>
    public int Favorites { get; set; } = 0;

    /// <summary>
    /// 分享
    /// </summary>
    public int Shares { get; set; } = 0;


    /// <summary>
    /// 弹幕
    /// </summary>
    public int Danmaku { get; set; } = 0;

    /// <summary>
    /// 评论
    /// </summary>
    public int Comments { get; set; } = 0;

    /// <summary>
    /// 正在观看的人数
    /// </summary>
    public int ViewingNum { get; set; } = 0;

    /// <summary>
    /// 播放数
    /// </summary>
    public int Views { get; set; } = 0;


    #region navgation - 视频

    /// <summary>
    /// 视频bv号
    /// </summary>
    public string BvId { get; set; } = "";

    /// <summary>
    /// 视频
    /// </summary>
    [JsonIgnore]
    public virtual Video? Video { get; set; }

    #endregion
}