using System.ComponentModel.DataAnnotations;

namespace Model;

/// <summary>
/// 视频分区
/// </summary>
public class VideoType
{
    /// <summary>
    /// 视频分区id
    /// </summary>
    [Key]
    public int TypeId { get; set; } = 0;
    
    /// <summary>
    /// 视频分区名
    /// </summary>
    public string Name { get; set; } = "";
}