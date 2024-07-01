using System.Text.Json.Serialization;

namespace BiliBiliApi.Dto;

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