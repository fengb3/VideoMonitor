using System.Text.Json.Serialization;

namespace BiliBiliApi.Models;

[Serializable]
public record BaseResponse
{
    /// <summary>
    /// 错误码，非 0 表示失败
    /// </summary>
    
    [JsonPropertyName("code")]
    public int    Code    { get; init; } = 0;
    
    /// <summary>
    /// 返回信息
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = "";
    
    /// <summary>
    /// 意义不明
    /// </summary>
    [JsonPropertyName("ttl")]
    public int    Ttl     { get; init; } = 0;
}

[Serializable]
public record WbiResponse : BaseResponse
{
    [JsonPropertyName("data")]
    public WbiData? Data { get; set; }

    [Serializable]
    public class WbiData
    {
        [JsonPropertyName("isLogin")]
        public bool IsLogin { get; set; }
        
        [JsonPropertyName("wbi_img")]
        public WbiImg? WbiImg { get; set; }
    }

    [Serializable]
    public class WbiImg
    {
        [JsonPropertyName("img_url")]
        public string? ImgUrl { get; set; }
        
        [JsonPropertyName("sub_url")]
        public string? SubUrl { get; set; } 
    }
}