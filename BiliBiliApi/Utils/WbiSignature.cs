using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;

namespace BiliBiliApi.Utils;

/// <summary>
/// Wbi 签名 处理
/// </summary>
/// <参考>
/// https://github.com/SocialSisterYi/bilibili-API-collect/blob/master/docs/misc/sign/wbi.md
/// </参考>
public static class WbiSignature
{
	private static readonly int[] MixinKeyEncTab =
	[
		46, 47, 18, 2, 53, 8, 23, 32, 15, 50, 10, 31, 58, 3, 45, 35, 27, 43, 5, 49, 33, 9, 42, 19, 29, 28, 14, 39,
		12, 38, 41, 13, 37, 48, 7, 16, 24, 55, 40, 61, 26, 17, 0, 1, 60, 51, 30, 4, 22, 25, 54, 21, 56, 59, 6, 63,
		57, 62, 11, 36, 20, 34, 44, 52
	];

	/// <summary>
	/// 对 imgKey 和 subKey 进行字符顺序打乱编码
	/// </summary>
	/// <param name="orig"></param>
	/// <returns></returns>
	private static string GetMixinKey(string orig) => MixinKeyEncTab.Aggregate("", (s, i) => s + orig[i])[..32];

	/// <summary>
	/// 参数封装到字典里 并进行进行 wbi 签名处理
	/// </summary>
	/// <param name="parameters">参数字典</param>
	/// <param name="imgKey">imgKey</param>
	/// <param name="subKey">subKey</param>
	/// <returns></returns>
	public static Dictionary<string, string> EncWbi(Dictionary<string, string> parameters,
	                                                string                     imgKey,
	                                                string                     subKey)
	{
		string mixinKey = GetMixinKey(imgKey + subKey);
		string currTime = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
		// 添加 wts 字段
		parameters["wts"] = currTime;
		// 按照 key 重排参数
		parameters = parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
		//过滤 value 中的 "!'()*" 字符
		parameters = parameters.ToDictionary(
			kvp => kvp.Key,
			kvp => new string(kvp.Value.Where(chr => !"!'()*".Contains(chr)).ToArray())
		);
		// 序列化参数
		string query = new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result;
		//计算 w_rid
		using MD5 md5       = MD5.Create();
		byte[]    hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query + mixinKey));
		string    wbiSign   = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
		parameters["w_rid"] = wbiSign;

		return parameters;
	}

	/// <summary>
	/// 将 query 参数进行 wbi 签名处理
	/// </summary>
	/// <param name="query">query</param>
	/// <param name="imgKey">imgKey</param>
	/// <param name="subKey">subKey</param>
	/// <returns></returns>
	public static string EncWbi(string query, string imgKey, string subKey)
	{
		string mixinKey = GetMixinKey(imgKey + subKey);
		string currTime = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
		// 添加 wts 字段
		query += $"&wts={currTime}";
		// 按照 key 重排参数
		var parameters = query.Split("&")
			.Select(x => x.Split("="))
			.ToDictionary(x => x[0], x => x[1]);
		//过滤 value 中的 "!'()*" 字符
		parameters = parameters.ToDictionary(
			kvp => kvp.Key,
			kvp => new string(kvp.Value.Where(chr => !"!'()*".Contains(chr)).ToArray())
		);
		// 序列化参数
		query = new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result;
		//计算 w_rid
		using var md5       = MD5.Create();
		var       hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query + mixinKey));
		var       wbiSign   = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
		query += $"&w_rid={wbiSign}";

		return query;
	}
}