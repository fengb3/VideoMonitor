
using System.Text.Json.Serialization;

namespace BiliBiliApi.Web_Interface;

/// <summary>
/// <para>获取视频详细信息(web端)</para>
/// <para>认证方式：Cookie(SESSDATA)</para>
/// <para>限制游客访问的视频需要登录</para>
/// </summary>
/// <api>
/// https://api.bilibili.com/x/web-interface/view
/// </api>
/// <doc>
/// https://socialsisteryi.github.io/bilibili-API-collect/docs/video/info.html#%E8%8E%B7%E5%8F%96%E8%A7%86%E9%A2%91%E8%AF%A6%E7%BB%86%E4%BF%A1%E6%81%AF-web%E7%AB%AF
/// </doc>

public abstract partial class View
{
    [Serializable]
    public record Response : BaseResponse
    {
        [JsonPropertyName("data")]
        public Data? Data { get; set; }
    }

    [Serializable]
    public class Data
    {
        [JsonPropertyName("bvid")]
        public string? Bvid { get; set; }

        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        [JsonPropertyName("videos")]
        public int Videos { get; set; }

        [JsonPropertyName("tid")]
        public int Tid { get; set; }

        [JsonPropertyName("tname")]
        public string? Tname { get; set; }

        [JsonPropertyName("copyright")]
        public int Copyright { get; set; }

        [JsonPropertyName("pic")]
        public string Pic { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("pubdate")]
        public long PubDate { get; set; }

        [JsonPropertyName("ctime")]
        public long Ctime { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("desc_v2")]
        public List<DescV2> DescV2 { get; set; }

        [JsonPropertyName("state")]
        public int State { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("mission_id")]
        public int MissionId { get; set; }

        [JsonPropertyName("rights")]
        public Rights Rights { get; set; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }

        [JsonPropertyName("stat")]
        public Stat Stat { get; set; }

        [JsonPropertyName("argue_info")]
        public ArgueInfo ArgueInfo { get; set; }

        [JsonPropertyName("dynamic")]
        public string Dynamic { get; set; }

        [JsonPropertyName("cid")]
        public long Cid { get; set; }

        [JsonPropertyName("dimension")]
        public Dimension Dimension { get; set; }

        [JsonPropertyName("premiere")]
        public object Premiere { get; set; }

        [JsonPropertyName("teenage_mode")]
        public int TeenageMode { get; set; }

        [JsonPropertyName("is_chargeable_season")]
        public bool IsChargeableSeason { get; set; }

        [JsonPropertyName("is_story")]
        public bool IsStory { get; set; }

        [JsonPropertyName("is_upower_exclusive")]
        public bool IsUpowerExclusive { get; set; }

        [JsonPropertyName("is_upower_play")]
        public bool IsUpowerPlay { get; set; }

        [JsonPropertyName("enable_vt")]
        public int EnableVt { get; set; }

        [JsonPropertyName("vt_display")]
        public string VtDisplay { get; set; }

        [JsonPropertyName("no_cache")]
        public bool NoCache { get; set; }

        [JsonPropertyName("pages")]
        public List<Page> Pages { get; set; }

        [JsonPropertyName("subtitle")]
        public Subtitle Subtitle { get; set; }

        [JsonPropertyName("staff")]
        public List<Staff> Staff { get; set; }

        [JsonPropertyName("is_season_display")]
        public bool IsSeasonDisplay { get; set; }

        [JsonPropertyName("user_garb")]
        public UserGarb UserGarb { get; set; }

        [JsonPropertyName("honor_reply")]
        public HonorReply HonorReply { get; set; }

        [JsonPropertyName("like_icon")]
        public string LikeIcon { get; set; }

        [JsonPropertyName("need_jump_bv")]
        public bool NeedJumpBv { get; set; }

        [JsonPropertyName("disable_show_up_info")]
        public bool DisableShowUpInfo { get; set; }
    }

    [Serializable]
    public class DescV2
    {
        [JsonPropertyName("raw_text")]
        public string RawText { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("biz_id")]
        public int BizId { get; set; }
    }

    [Serializable]
    public class Rights
    {
        [JsonPropertyName("bp")]
        public int Bp { get; set; }

        [JsonPropertyName("elec")]
        public int Elec { get; set; }

        [JsonPropertyName("download")]
        public int Download { get; set; }

        [JsonPropertyName("movie")]
        public int Movie { get; set; }

        [JsonPropertyName("pay")]
        public int Pay { get; set; }

        [JsonPropertyName("hd5")]
        public int Hd5 { get; set; }

        [JsonPropertyName("no_reprint")]
        public int NoReprint { get; set; }

        [JsonPropertyName("autoplay")]
        public int Autoplay { get; set; }

        [JsonPropertyName("ugc_pay")]
        public int UgcPay { get; set; }

        [JsonPropertyName("is_cooperation")]
        public int IsCooperation { get; set; }

        [JsonPropertyName("ugc_pay_preview")]
        public int UgcPayPreview { get; set; }

        [JsonPropertyName("no_background")]
        public int NoBackground { get; set; }

        [JsonPropertyName("clean_mode")]
        public int CleanMode { get; set; }

        [JsonPropertyName("is_stein_gate")]
        public int IsSteinGate { get; set; }

        [JsonPropertyName("is_360")]
        public int Is360 { get; set; }

        [JsonPropertyName("no_share")]
        public int NoShare { get; set; }

        [JsonPropertyName("arc_pay")]
        public int ArcPay { get; set; }

        [JsonPropertyName("free_watch")]
        public int FreeWatch { get; set; }
    }

    [Serializable]
    public class Owner
    {
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("face")]
        public string Face { get; set; }
    }

    [Serializable]
    public class Stat
    {
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        [JsonPropertyName("view")]
        public int View { get; set; }

        [JsonPropertyName("danmaku")]
        public int Danmaku { get; set; }

        [JsonPropertyName("reply")]
        public int Reply { get; set; }

        [JsonPropertyName("favorite")]
        public int Favorite { get; set; }

        [JsonPropertyName("coin")]
        public int Coin { get; set; }

        [JsonPropertyName("share")]
        public int Share { get; set; }

        [JsonPropertyName("now_rank")]
        public int NowRank { get; set; }

        [JsonPropertyName("his_rank")]
        public int HisRank { get; set; }

        [JsonPropertyName("like")]
        public int Like { get; set; }

        [JsonPropertyName("dislike")]
        public int Dislike { get; set; }

        [JsonPropertyName("evaluation")]
        public string Evaluation { get; set; }

        [JsonPropertyName("vt")]
        public int Vt { get; set; }
    }

    [Serializable]
    public class ArgueInfo
    {
        [JsonPropertyName("argue_msg")]
        public string ArgueMsg { get; set; }

        [JsonPropertyName("argue_type")]
        public int ArgueType { get; set; }

        [JsonPropertyName("argue_link")]
        public string ArgueLink { get; set; }
    }

    [Serializable]
    public class Page
    {
        [JsonPropertyName("cid")]
        public long Cid { get; set; }

        [JsonPropertyName("page")]
        public int PageNumber { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("part")]
        public string Part { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("vid")]
        public string Vid { get; set; }

        [JsonPropertyName("weblink")]
        public string Weblink { get; set; }

        [JsonPropertyName("dimension")]
        public Dimension Dimension { get; set; }
    }

    [Serializable]
    public class Dimension
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("rotate")]
        public int Rotate { get; set; }
    }

    [Serializable]
    public class Subtitle
    {
        [JsonPropertyName("allow_submit")]
        public bool AllowSubmit { get; set; }

        [JsonPropertyName("list")]
        public List<SubtitleItem> List { get; set; }
    }

    [Serializable]
    public class SubtitleItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("lan")]
        public string Lan { get; set; }

        [JsonPropertyName("lan_doc")]
        public string LanDoc { get; set; }

        [JsonPropertyName("subtitle_url")]
        public string SubtitleUrl { get; set; }
    }

    [Serializable]
    public class Staff
    {
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("face")]
        public string Face { get; set; }

        [JsonPropertyName("official_verify")]
        public OfficialVerify OfficialVerify { get; set; }

        [JsonPropertyName("follower")]
        public int Follower { get; set; }
    }

    [Serializable]
    public class OfficialVerify
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }
    }

    [Serializable]
    public class UserGarb
    {
        [JsonPropertyName("is_vip")]
        public bool IsVip { get; set; }

        [JsonPropertyName("vip_type")]
        public int VipType { get; set; }

        [JsonPropertyName("vip_status")]
        public int VipStatus { get; set; }

        [JsonPropertyName("due_date")]
        public long DueDate { get; set; }

        [JsonPropertyName("vip_pay_type")]
        public int VipPayType { get; set; }

        [JsonPropertyName("theme_type")]
        public int ThemeType { get; set; }

        [JsonPropertyName("label")]
        public Label Label { get; set; }
    }

    [Serializable]
    public class Label
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }

    [Serializable]
    public class HonorReply
    {
        [JsonPropertyName("is_open")]
        public int IsOpen { get; set; }

        [JsonPropertyName("level_info")]
        public LevelInfo LevelInfo { get; set; }
    }

    [Serializable]
    public class LevelInfo
    {
        [JsonPropertyName("current_level")]
        public int CurrentLevel { get; set; }

        [JsonPropertyName("current_min")]
        public int CurrentMin { get; set; }

        [JsonPropertyName("current_exp")]
        public int CurrentExp { get; set; }

        [JsonPropertyName("next_exp")]
        public int NextExp { get; set; }
    }
}