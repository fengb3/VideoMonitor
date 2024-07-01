using System.Text.Json.Serialization;

namespace BiliBiliApi;

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