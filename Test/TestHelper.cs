using System.Text.Json;

namespace Test;

public static class TestHelper
{
    public static string ToJsonString(this object obj)
        => JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
}