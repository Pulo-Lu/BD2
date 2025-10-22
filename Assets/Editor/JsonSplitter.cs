using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;


public class JsonSplitter : Editor
{
    private static string outputFolder = "Assets/Data/Json";

    [MenuItem("Tools/Json切割")]
    public static void SplitJson()
    {
            
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        
        var filePath = $"Assets/character_table.json";
        if (!File.Exists(filePath))
        {
            Debug.Log("文件不存在");
            return;
        }
        
        var jsonTextAsset = File.ReadAllText(filePath);
        JObject obj = JObject.Parse(jsonTextAsset);

        
        foreach (var property in obj.Properties())
        {
            string key = property.Name; // 比如 char_112_siege
            var value = property.Value; // 对应的数据对象
            
            // 从对象中取 name 字段
            string name = value["name"]?.ToString() ?? key;
            
            // 从对象中取 sortIndex 字段
            string sortIndex = value["sortIndex"]?.ToString() ?? key;
            
            // 组合安全文件名
            string safeName = $"{sortIndex}_{name}";
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                safeName = safeName.Replace(c.ToString(), "_");
            }

            var outputPath = Path.Combine(outputFolder, $"{safeName}.json");
            File.WriteAllText(outputPath, value.ToString());
            Debug.Log($"已输出：{outputPath}");
        }
        
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("完成", $"成功分割 {obj.Properties().Count()} 个对象", "确定");
        // JsonConvert.
    }
}