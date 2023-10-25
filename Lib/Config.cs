using System.Text.Json;

namespace Lib;

public class Config
{
    public static readonly string PathHome       = @"." + Path.DirectorySeparatorChar;
    public const           string FileNameConfig = @"config.json";

    private static Dictionary<string, JsonElement>? _config;

    private static void Load()
    {
        var path = Path.Combine(PathHome, FileNameConfig);
        var json = File.ReadAllText(path);
        _config = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
    }

    public static JsonElement? Get(string key)
    {
        if (_config == null)
            Load();

        return _config?[key];
    }
}