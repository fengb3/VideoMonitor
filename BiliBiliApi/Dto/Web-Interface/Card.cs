using System.Text.Json.Serialization;

namespace BiliBiliApi.Web_Interface;

/// <summary>
/// <para>用户名片信息</para>
/// <para>认证方式：Cookie(SESSDATA)</para>
/// </summary>
/// <api>
/// https://api.bilibili.com/x/web-interface/card
/// </api>
/// <doc>
/// https://socialsisteryi.github.io/bilibili-API-collect/docs/user/info.html#%E7%94%A8%E6%88%B7%E5%90%8D%E7%89%87%E4%BF%A1%E6%81%AF
/// </doc>
public abstract class Card
{
	[Serializable]
	public record Response : BaseResponse
	{
		[JsonPropertyName("data")]
		public Data Data { get; init; } = new ();
	}

	[Serializable]
	public record Data
	{
		/// <summary>
		/// 卡片信息
		/// </summary>
		[JsonPropertyName("card")]
		public CardInfo? Card { get; init; }

        /// <summary>
        /// 空间信息
        /// </summary>
        [JsonPropertyName("space")]
		public SpaceInfo? Space { get; init; }

		/// <summary>
		/// 是否关注此用户
		/// - true：已关
		/// - false：未关注
		/// </summary>
		/// <remarks>需要登录(Cookie)未登录为 false</remarks>
		[JsonPropertyName("following")]
		public bool Following { get; init; }

		/// <summary>
		/// 用户稿件数
		/// </summary>
		[JsonPropertyName("archive_count")]
		public int ArchiveCount { get; init; }

		/// <summary>
		/// 0
		/// </summary>
		[JsonPropertyName("article_count")]
		public int ArticleCount { get; init; }

		/// <summary>
		/// 粉丝数
		/// </summary>
		[JsonPropertyName("follower")]
		public int Follower { get; init; }

		/// <summary>
		/// 点赞数
		/// </summary>
		[JsonPropertyName("like_num")]
		public int LikeNum { get; init; }
	}

	[Serializable]
	public record SpaceInfo
	{
		[JsonPropertyName("s_img")]
		public Uri SImg { get; init; } // Assuming this is a small image URL

		[JsonPropertyName("l_img")]
		public Uri LImg { get; init; } // Assuming this is a large image URL
	}

    /*
		| 字段            | 类型  | 内容           | 备注                                                         |
		| --------------- | ----- | -------------- | ------------------------------------------------------------ |
		| mid             | str   | 用户mid        |                                                              |
		| approve         | bool  | false          | **作用尚不明确**                                             |
		| name            | str   | 用户昵称       |                                                              |
		| sex             | str   | 用户性别       | 男 女 保密                                                   |
		| face            | str   | 用户头像链接   |                                                              |
		| DisplayRank     | str   | 0              | **作用尚不明确**                                             |
		| regtime         | num   | 0              | **作用尚不明确**                                             |
		| spacesta        | num   | 用户状态       | 0：正常<br />-2：被封禁                                      |
		| birthday        | str   | 空             | **作用尚不明确**                                             |
		| place           | str   | 空             | **作用尚不明确**                                             |
		| description     | str   | 空             | **作用尚不明确**                                             |
		| article         | num   | 0              | **作用尚不明确**                                             |
		| attentions      | array | 空             | **作用尚不明确**                                             |
		| fans            | num   | 粉丝数         |                                                              |
		| friend          | num   | 关注数         |                                                              |
		| attention       | num   | 关注数         |                                                              |
		| sign            | str   | 签名           |                                                              |
		| level_info      | obj   | 等级           |                                                              |
		| pendant         | obj   | 挂件           |                                                              |
		| nameplate       | obj   | 勋章           |                                                              |
		| Official        | obj   | 认证信息       |                                                              |
		| official_verify | obj   | 认证信息2      |                                                              |
		| vip             | obj   | 大会员状态     |                                                              |
		| space           | obj   | 主页头图       |                                                              |
	*/
    [Serializable]
	public record CardInfo
	{

        /// <summary>
        /// 用户mid
        /// </summary>
        [JsonPropertyName("mid")]
		public string Mid { get; init; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [JsonPropertyName("name")]
		public string Name { get; init; }

		/// <summary>
		/// false
		/// </summary>
		[JsonPropertyName("approve")]
		public bool Approve { get; init; }

        /// <summary>
        /// 用户性别
        /// </summary>
        [JsonPropertyName("sex")]
		public string Sex { get; init; }

		[JsonPropertyName("rank")]
		public string Rank { get; init; }

		/// <summary>
		/// 头像链接
		/// </summary>
		[JsonPropertyName("face")]
		public Uri Face { get; init; }

		/// <summary>
		/// 0
		/// </summary>
		[JsonPropertyName("DisplayRank")]
		public string DisplayRank { get; init; }

		/// <summary>
		/// 0
		/// </summary>
		[JsonPropertyName("regtime")]
		public long Regtime { get; init; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [JsonPropertyName("spacesta")]
		public int Spacesta { get; init; }

		[JsonPropertyName("birthday")]
		public string Birthday { get; init; }

		[JsonPropertyName("place")]
		public string Place { get; init; }

		[JsonPropertyName("description")]
		public string Description { get; init; }

		[JsonPropertyName("article")]
		public int Article { get; init; }

		[JsonPropertyName("attentions")]
		public int[] Attentions { get; init; }

		/// <summary>
		/// 粉丝数
		/// </summary>
		[JsonPropertyName("fans")]
		public int Fans { get; init; }

        /// <summary>
        /// 关注数
        /// </summary>
        [JsonPropertyName("friend")]
		public int Friend { get; init; }

        /// <summary>
        /// 关注数
        /// </summary>
        [JsonPropertyName("attention")]
		public int Attention { get; init; }

        /// <summary>
        /// 签名
        /// </summary>
        [JsonPropertyName("sign")]
		public string Sign { get; init; }

        /// <summary>
        /// 等级
        /// </summary>
        [JsonPropertyName("level_info")]
		public LevelInfo LevelInfo { get; init; }

        /// <summary>
        /// 挂件
        /// </summary>
        [JsonPropertyName("pendant")]
		public PendantInfo Pendant { get; init; }

        /// <summary>
        /// 勋章
        /// </summary>
        [JsonPropertyName("nameplate")]
		public NameplateInfo Nameplate { get; init; }

        /// <summary>
        /// 认证信息  
        /// </summary>
        [JsonPropertyName("Official")]
		public OfficialInfo Official { get; init; }

        /// <summary>
        /// 认证信息2
        /// </summary>
        [JsonPropertyName("official_verify")]
		public OfficialVerifyInfo OfficialVerify { get; init; }

        /// <summary>
        /// 大会员状态 
        /// </summary>
        [JsonPropertyName("vip")]
		public VipInfo Vip { get; init; }
	}

	[Serializable]
	public record LevelInfo
	{
		[JsonPropertyName("current_level")]
		public int CurrentLevel { get; init; }

		[JsonPropertyName("current_min")]
		public int CurrentMin { get; init; }

		[JsonPropertyName("current_exp")]
		public int CurrentExp { get; init; }

		[JsonPropertyName("next_exp")]
		public int NextExp { get; init; }
	}

	[Serializable]
	public record PendantInfo
	{
		[JsonPropertyName("pid")]
		public int Pid { get; init; }

		[JsonPropertyName("name")]
		public string Name { get; init; }

		[JsonPropertyName("image")]
		public Uri Image { get; init; }

		[JsonPropertyName("expire")]
		public long Expire { get; init; } // Assuming this is a timestamp

		[JsonPropertyName("image_enhance")]
		public Uri ImageEnhance { get; init; } // Additional image details if available

		[JsonPropertyName("image_enhance_frame")]
		public Uri ImageEnhanceFrame { get; init; } // Frame for enhanced image if available
	}

	[Serializable]
	public record NameplateInfo
	{
		[JsonPropertyName("nid")]
		public int Nid { get; init; } // Numeric identifier for the nameplate

		[JsonPropertyName("name")]
		public string Name { get; init; } // Name displayed on the nameplate

		[JsonPropertyName("image")]
		public Uri Image { get; init; } // URL to the image of the nameplate

		[JsonPropertyName("image_small")]
		public Uri ImageSmall { get; init; } // URL to a smaller version of the image

		[JsonPropertyName("level")]
		public string Level { get; init; } // Level or type of the nameplate (e.g., "普通勋章")

		[JsonPropertyName("condition")]
		public string Condition { get; init; } // The condition to obtain this nameplate
	}

	[Serializable]
	public record OfficialInfo
	{
		[JsonPropertyName("role")]
		public int Role { get; init; }

		[JsonPropertyName("title")]
		public string Title { get; init; }

		[JsonPropertyName("desc")]
		public string Description { get; init; }

		[JsonPropertyName("type")]
		public int Type { get; init; }
	}

	[Serializable]
	public record OfficialVerifyInfo
	{
		[JsonPropertyName("type")]
		public int Type { get; init; }

		[JsonPropertyName("desc")]
		public string Description { get; init; }
	}

	[Serializable]
	public record VipInfo
	{
		[JsonPropertyName("type")]
		public int Type { get; init; }

		[JsonPropertyName("status")]
		public int Status { get; init; }

		[JsonPropertyName("due_date")]
		public long DueDate { get; init; } // Assuming this is a Unix timestamp for the VIP due date

		[JsonPropertyName("vip_pay_type")]
		public int VipPayType { get; init; }

		[JsonPropertyName("theme_type")]
		public int ThemeType { get; init; }

		[JsonPropertyName("label")]
		public VipLabel Label { get; init; }

		[JsonPropertyName("avatar_subscript")]
		public int AvatarSubscript { get; init; }

		[JsonPropertyName("nickname_color")]
		public string NicknameColor { get; init; }

		[JsonPropertyName("role")]
		public int Role { get; init; }

		[JsonPropertyName("avatar_subscript_url")]
		public Uri AvatarSubscriptUrl { get; init; } // URL to the avatar subscript image

		[JsonPropertyName("vipType")]
		public int VipType { get; init; } // This might be redundant if it's the same as 'Type'

		[JsonPropertyName("vipStatus")]
		public int VipStatus { get; init; } // This might be redundant if it's the same as 'Status'
	}

	[Serializable]
	public record VipLabel
	{
		[JsonPropertyName("path")]
		public string Path { get; init; }

		[JsonPropertyName("text")]
		public string Text { get; init; }

		[JsonPropertyName("label_theme")]
		public string LabelTheme { get; init; }

		[JsonPropertyName("text_color")]
		public string TextColor { get; init; }

		[JsonPropertyName("bg_style")]
		public int BackgroundStyle { get; init; }

		[JsonPropertyName("bg_color")]
		public string BackgroundColor { get; init; }

		[JsonPropertyName("border_color")]
		public string BorderColor { get; init; }
	}
}