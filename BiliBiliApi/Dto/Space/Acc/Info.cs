using BiliBiliApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BiliBiliApi.Dto.Space.Acc;

/// <summary>
/// 用户空间详细信息
/// 请求方式：GET
/// 认证方式：Cookie（SESSDATA）
/// 鉴权方式：Wbi 签名, Cookie (对于某些 IP 地址，需要在 Cookie 中提供任意非空的 buvid3 字段)
/// </summary>
/// <api>https://api.bilibili.com/x/space/wbi/acc/info</api>
/// <doc>https://socialsisteryi.github.io/bilibili-API-collect/docs/user/info.html#%E7%94%A8%E6%88%B7%E7%A9%BA%E9%97%B4%E8%AF%A6%E7%BB%86%E4%BF%A1%E6%81%AF</doc>
public abstract partial class Info
{
    public record Response : BaseResponse
    {
        public Data? Data { get; set; }
    }

    public record Data
    {
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sex")]
        public string Sex { get; set; }

        [JsonPropertyName("face")]
        public string Face { get; set; }

        [JsonPropertyName("face_nft")]
        public int FaceNft { get; set; }

        [JsonPropertyName("face_nft_type")]
        public int FaceNftType { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("jointime")]
        public int Jointime { get; set; }

        [JsonPropertyName("moral")]
        public int Moral { get; set; }

        [JsonPropertyName("silence")]
        public int Silence { get; set; }

        [JsonPropertyName("coins")]
        public int Coins { get; set; }

        [JsonPropertyName("fans_badge")]
        public bool FansBadge { get; set; }

        [JsonPropertyName("fans_medal")]
        public FansMedal FansMedal { get; set; }

        [JsonPropertyName("official")]
        public Official Official { get; set; }

        [JsonPropertyName("vip")]
        public Vip Vip { get; set; }

        [JsonPropertyName("pendant")]
        public Pendant Pendant { get; set; }

        [JsonPropertyName("nameplate")]
        public Nameplate Nameplate { get; set; }

        [JsonPropertyName("user_honour_info")]
        public UserHonourInfo UserHonourInfo { get; set; }

        [JsonPropertyName("is_followed")]
        public bool IsFollowed { get; set; }

        [JsonPropertyName("top_photo")]
        public string TopPhoto { get; set; }

        [JsonPropertyName("theme")]
        public object Theme { get; set; }

        [JsonPropertyName("sys_notice")]
        public object SysNotice { get; set; }

        [JsonPropertyName("live_room")]
        public LiveRoom LiveRoom { get; set; }

        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }

        [JsonPropertyName("school")]
        public School School { get; set; }

        [JsonPropertyName("profession")]
        public Profession Profession { get; set; }

        [JsonPropertyName("tags")]
        public object Tags { get; set; }

        [JsonPropertyName("series")]
        public Series Series { get; set; }

        [JsonPropertyName("is_senior_member")]
        public int IsSeniorMember { get; set; }

        [JsonPropertyName("mcn_info")]
        public object McnInfo { get; set; }

        [JsonPropertyName("gaia_res_type")]
        public int GaiaResType { get; set; }

        [JsonPropertyName("gaia_data")]
        public object GaiaData { get; set; }

        [JsonPropertyName("is_risk")]
        public bool IsRisk { get; set; }

        [JsonPropertyName("elec")]
        public Elec Elec { get; set; }

        [JsonPropertyName("contract")]
        public Contract Contract { get; set; }
    }

    public record FansMedal
    {
        [JsonPropertyName("uid")]
        public long Uid { get; set; }

        [JsonPropertyName("target_id")]
        public long TargetId { get; set; }

        [JsonPropertyName("medal_id")]
        public long MedalId { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("medal_name")]
        public string MedalName { get; set; }

        [JsonPropertyName("medal_color")]
        public int MedalColor { get; set; }

        [JsonPropertyName("intimacy")]
        public int Intimacy { get; set; }

        [JsonPropertyName("next_intimacy")]
        public int NextIntimacy { get; set; }

        [JsonPropertyName("day_limit")]
        public int DayLimit { get; set; }

        [JsonPropertyName("medal_color_start")]
        public int MedalColorStart { get; set; }

        [JsonPropertyName("medal_color_end")]
        public int MedalColorEnd { get; set; }

        [JsonPropertyName("medal_color_border")]
        public int MedalColorBorder { get; set; }

        [JsonPropertyName("is_lighted")]
        public int IsLighted { get; set; }

        [JsonPropertyName("light_status")]
        public int LightStatus { get; set; }

        [JsonPropertyName("wearing_status")]
        public int WearingStatus { get; set; }

        [JsonPropertyName("score")]
        public long Score { get; set; }
    }

    public record Official
    {
        [JsonPropertyName("role")]
        public int Role { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }
    }

    public record Vip
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("due_date")]
        public long DueDate { get; set; }

        [JsonPropertyName("vip_pay_type")]
        public int VipPayType { get; set; }

        [JsonPropertyName("theme_type")]
        public int ThemeType { get; set; }

        [JsonPropertyName("label")]
        public Label Label { get; set; }

        [JsonPropertyName("avatar_subscript")]
        public int AvatarSubscript { get; set; }

        [JsonPropertyName("nickname_color")]
        public string NicknameColor { get; set; }

        [JsonPropertyName("role")]
        public int Role { get; set; }

        [JsonPropertyName("avatar_subscript_url")]
        public string AvatarSubscriptUrl { get; set; }

        [JsonPropertyName("tv_vip_status")]
        public int TvVipStatus { get; set; }

        [JsonPropertyName("tv_vip_pay_type")]
        public int TvVipPayType { get; set; }

        [JsonPropertyName("tv_due_date")]
        public long TvDueDate { get; set; }
    }

    public record Label
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("label_theme")]
        public string LabelTheme { get; set; }

        [JsonPropertyName("text_color")]
        public string TextColor { get; set; }

        [JsonPropertyName("bg_style")]
        public int BgStyle { get; set; }

        [JsonPropertyName("bg_color")]
        public string BgColor { get; set; }

        [JsonPropertyName("border_color")]
        public string BorderColor { get; set; }

        [JsonPropertyName("use_img_label")]
        public bool UseImgLabel { get; set; }

        [JsonPropertyName("img_label_uri_hans")]
        public string ImgLabelUriHans { get; set; }

        [JsonPropertyName("img_label_uri_hant")]
        public string ImgLabelUriHant { get; set; }

        [JsonPropertyName("img_label_uri_hans_static")]
        public string ImgLabelUriHansStatic { get; set; }

        [JsonPropertyName("img_label_uri_hant_static")]
        public string ImgLabelUriHantStatic { get; set; }
    }

    public record Pendant
    {
        [JsonPropertyName("pid")]
        public long Pid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("expire")]
        public int Expire { get; set; }

        [JsonPropertyName("image_enhance")]
        public string ImageEnhance { get; set; }

        [JsonPropertyName("image_enhance_frame")]
        public string ImageEnhanceFrame { get; set; }
    }

    public record Nameplate
    {
        [JsonPropertyName("nid")]
        public int Nid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("image_small")]
        public string ImageSmall { get; set; }

        [JsonPropertyName("level")]
        public string Level { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; }
    }

    public record UserHonourInfo
    {
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        [JsonPropertyName("colour")]
        public object Colour { get; set; }

        [JsonPropertyName("tags")]
        public List<object> Tags { get; set; }
    }

    public record LiveRoom
    {
        [JsonPropertyName("roomStatus")]
        public int RoomStatus { get; set; }

        [JsonPropertyName("liveStatus")]
        public int LiveStatus { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("cover")]
        public string Cover { get; set; }

        [JsonPropertyName("roomid")]
        public long Roomid { get; set; }

        [JsonPropertyName("roundStatus")]
        public int RoundStatus { get; set; }

        [JsonPropertyName("broadcast_type")]
        public int BroadcastType { get; set; }

        [JsonPropertyName("watched_show")]
        public WatchedShow WatchedShow { get; set; }
    }

    public record WatchedShow
    {
        [JsonPropertyName("switch")]
        public bool Switch { get; set; }

        [JsonPropertyName("num")]
        public int Num { get; set; }

        [JsonPropertyName("text_small")]
        public string TextSmall { get; set; }

        [JsonPropertyName("text_large")]
        public string TextLarge { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("icon_location")]
        public string IconLocation { get; set; }

        [JsonPropertyName("icon_web")]
        public string IconWeb { get; set; }
    }

    public record School
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public record Profession
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("is_show")]
        public int IsShow { get; set; }
    }

    public record Series
    {
        [JsonPropertyName("user_upgrade_status")]
        public int UserUpgradeStatus { get; set; }

        [JsonPropertyName("show_upgrade_window")]
        public bool ShowUpgradeWindow { get; set; }
    }

    public record Elec
    {
        [JsonPropertyName("show_info")]
        public ShowInfo ShowInfo { get; set; }
    }

    public record ShowInfo
    {
        [JsonPropertyName("show")]
        public bool Show { get; set; }

        [JsonPropertyName("state")]
        public int State { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("jump_url")]
        public string JumpUrl { get; set; }
    }

    public record Contract
    {
        [JsonPropertyName("is_display")]
        public bool IsDisplay { get; set; }

        [JsonPropertyName("is_follow_display")]
        public bool IsFollowDisplay { get; set; }
    }


}
