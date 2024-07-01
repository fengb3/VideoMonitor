using System.Text.Json.Serialization;

namespace BiliBiliApi.Space.Arc;

public abstract class Search
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public record Request
    {
        [JsonPropertyName("mid")]
        public required long Mid { get; set; }

        [JsonPropertyName("order")]
        public string? Order { get; set; }

        [JsonPropertyName("tid")]
        public int? Tid { get; set; }

        [JsonPropertyName("keyword")]
        public string? Keyword { get; set; }

        [JsonPropertyName("pn")]
        public int? Pn { get; set; }

        [JsonPropertyName("ps")]
        public int? Ps { get; set; }
    }

    [Serializable]
    public record Response : BaseResponse
    {
        [JsonPropertyName("data")]
        public Data? Data { get; set; }
    }

    [Serializable]
    public class Data
    {
        [JsonPropertyName("list")]
        public List? List { get; set; }

        [JsonPropertyName("page")]
        public Page? Page { get; set; }

        [JsonPropertyName("episodic_button")]
        public EpisodicButton? EpisodicButton { get; set; }
    }

    [Serializable]
    public class List
    {
        [JsonPropertyName("tlist")]
        public Dictionary<string, Tlist> Tlist { get; set; }

        [JsonPropertyName("vlist")]
        public List<Vlist> Vlist { get; set; }
    }

    [Serializable]
    public class Tlist
    {
        [JsonPropertyName("tid")]
        public int Tid { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    [Serializable]
    public class Vlist
    {
        [JsonPropertyName("comment")]
        public int Comment { get; set; }

        [JsonPropertyName("typeid")]
        public int Typeid { get; set; }

        [JsonPropertyName("play")]
        public int Play { get; set; }

        [JsonPropertyName("pic")]
        public string Pic { get; set; }

        [JsonPropertyName("subtitle")]
        public string Subtitle { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("review")]
        public int Review { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("mid")]
        public int Mid { get; set; }

        [JsonPropertyName("created")]
        public int Created { get; set; }

        [JsonPropertyName("length")]
        public string Length { get; set; }

        [JsonPropertyName("video_review")]
        public int VideoReview { get; set; }

        [JsonPropertyName("aid")]
        public int Aid { get; set; }

        [JsonPropertyName("bvid")]
        public string Bvid { get; set; }

        [JsonPropertyName("hide_click")]
        public bool HideClick { get; set; }

        [JsonPropertyName("is_pay")]
        public int IsPay { get; set; }

        [JsonPropertyName("is_union_video")]
        public int IsUnionVideo { get; set; }

        [JsonPropertyName("is_steins_gate")]
        public int IsSteinsGate { get; set; }
    }

    [Serializable]
    public class Page
    {
        [JsonPropertyName("pn")]
        public int Pn { get; set; }

        [JsonPropertyName("ps")]
        public int Ps { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    [Serializable]
    public class EpisodicButton
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }
}