using System.Text.Json.Nodes;
using Model.RunTimeVar;
using MyLib.DataManagement;
using MyLib.Helper;
using MyLib.Tool;

namespace MyLib.API;

public class WbiConsumer:IDataConsumer<JsonNode>
{
    public void Consume(JsonNode? data)
    {
        if (data == null)
        {
            Log.Error($"解析wbi密钥失败，data为null");
            return;
        }
        
        string imgUrl = (string)data["wbi_img"]!["img_url"]!;
        imgUrl = imgUrl.Split("/")[^1].Split(".")[0];

        string subUrl = (string)data["wbi_img"]!["sub_url"]!;
        subUrl = subUrl.Split("/")[^1].Split(".")[0];
        
        var now = DateTime.Now.ToTimeStamp();

        if (DatabaseHandler.Instance.TryGetData(c => c.Key == "wbi_img_url", out Config config))
        {
            config.Value = imgUrl;
            config.LastTimeUpdate = now;
            
            DatabaseHandler.Instance.UpdateData(config);
        }
        else
        {
            config = new Config
                     {
                         Key = "wbi_img_url",
                         Value = imgUrl,
                         LastTimeUpdate = now
                     };
            DatabaseHandler.Instance.AddData(config);
        }
        
        if (DatabaseHandler.Instance.TryGetData(c => c.Key == "wbi_sub_url", out Config config2))
        {
            config2.Value = subUrl;
            
            DatabaseHandler.Instance.UpdateData(config2);
        }
        else
        {
            config2 = new Config
                      {
                         Key = "wbi_sub_url",
                         Value = subUrl,
                         LastTimeUpdate = now
                     };
            DatabaseHandler.Instance.AddData(config2);
        }
        
        Log.Info($"wbi密钥更新成功 - {imgUrl} - {subUrl}");
    }
}