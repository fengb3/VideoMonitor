namespace MyLib.Helper;

public static class TimeHelper
{
    private static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 获取当前时间 asc 格式
    /// </summary>
    /// <returns></returns>
    public static string CurrentAscTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff");
    }

    /// <summary>
    /// 获取当前时间戳
    /// </summary>
    /// <returns></returns>
    public static long GetCurrentTimeStamp()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    /// <summary>
    /// datetime 转时间戳
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long ToTimeStamp(this DateTime time)
    {
        var elapsedTime = time - Epoch;
        return (long)elapsedTime.TotalSeconds;
    }

    /// <summary>
    /// 时间戳转 datetime
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this long timeStamp)
    {
        var dateTime = Epoch.AddSeconds(timeStamp);
        return dateTime;
    }

    /// <summary>
    /// 字符串时间戳 转 datetime
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string timestamp)
    {
        return ToDateTime(long.Parse(timestamp));
    }
}