using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using LitJson;
using UnityEngine;

public class Tools
{
    public readonly static System.Random Rnd = new System.Random();
    #region 字母表
    private enum Chart
    {
        A, E, I, O, U
    }
    private enum Chart_0
    {
        B, C, D, F, G, H, J, K, L, M, N, P, Q, R, S, T, V, W, X, Y, Z
    }
    private enum Chart_1
    {
        a, e, i, o, u
    }
    private enum Chart_2
    {
        b, c, d, f, g, h, j, k, l, m, n, p, q, r, s, t, v, w, x, y, z
    }
    #endregion
    public static string GetRandomName(int min_length, int max_length)
    {
        int Length = Rnd.Next(min_length, max_length);
        string Name = "";
        bool Last = false;
        int Pop = 0;
        switch (Rnd.Next(0, 2))
        {
            case 0:
                {
                    Chart[] chart = Enum.GetValues(typeof(Chart)) as Chart[];
                    Name += chart[Rnd.Next(0, chart.Length)].ToString();
                    Last = true;
                    Pop = +15;
                    break;
                }
            case 1:
                {
                    Chart_0[] chart = Enum.GetValues(typeof(Chart_0)) as Chart_0[];
                    Name += chart[Rnd.Next(0, chart.Length)].ToString();
                    Last = false;
                    Pop = 0;
                    break;
                }
        }
        while (Name.Length < Length)
        {
            switch (Last)
            {
                case true:
                    {
                        if (Rnd.Next(0, 100) < 30 - Pop)
                        {
                            Chart_1[] chart = Enum.GetValues(typeof(Chart_1)) as Chart_1[];
                            Name += chart[Rnd.Next(0, chart.Length)].ToString();
                            Last = true;
                            Pop += 15;
                        }
                        else
                        {
                            Chart_2[] chart = Enum.GetValues(typeof(Chart_2)) as Chart_2[];
                            Name += chart[Rnd.Next(0, chart.Length)].ToString();
                            Last = false;
                            Pop = 0;
                        }
                        break;
                    }
                case false:
                    {
                        Chart_1[] chart = Enum.GetValues(typeof(Chart_1)) as Chart_1[];
                        Name += chart[Rnd.Next(0, chart.Length)].ToString();
                        Last = true;
                        Pop += 15;
                        break;
                    }
            }
        }
        return Name;
    }
    public static string intToRoman(int num)
    {
        string[] M = new string[] { "", "M", "MM", "MMM" };
        string[] C = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        string[] X = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        string[] I = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
        return M[num / 1000] + C[(num % 1000) / 100] + X[(num % 100) / 10] + I[(num % 10)];
    }
    public static string getRule(string Name)
    {
        string filePath = Application.streamingAssetsPath + "/Rule/" + Name + ".json";
        if(!File.Exists(filePath))
        {
            Debug.Log("Rule file not found!");
            return null;
        }
        string str = File.ReadAllText(filePath, System.Text.Encoding.Default);
        GenerateRule rule = JsonConvert.DeserializeObject<GenerateRule>(str);
        if(rule == null)
        {
            Debug.Log("Rule file not correct!");
            return null;
        }
        int maxCount = 0;
        List<string> temp = new List<string>();
        foreach (var item in rule.generateRule)
        {
            maxCount += item.Value;
            for(int i = 0;i<item.Value;i++)
            {
                temp.Add(item.Key);
            }
        }
        int rand = UnityEngine.Random.Range(0,maxCount);
        return temp[rand]; 
    }
    public static void Serialize<T>(T obj, string filepath, string filename)
    {
        string json = JsonConvert.SerializeObject(obj);
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }
        File.WriteAllText(filepath + filename, json);
    }
    public static T DeSerialize<T>(string filename)
    {
        T obj = default(T);
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            obj = (T)JsonConvert.DeserializeObject(json, typeof(T));
        }

        return obj;
    }
    public static void SaveDataToJson<T>(T obj,string filePath,string fileName)
    {
        {
            string json = JsonMapper.ToJson(obj);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            File.WriteAllText(filePath + fileName, json);
        }
    }
    public static List<string> LoadFilesBySuffix(string filePath, string suffix)
    {
        List<string> fileList = new List<string>();
        if (!Directory.Exists(filePath))
            return fileList;
        string[] files = Directory.GetFiles(filePath);
        foreach (var fileName in files)
        {
            string[] fileNames = fileName.Split(new char[2] { '\\', '/' });
            if (fileNames != null && fileNames.Length != 0)
            {
                var name = fileNames[fileNames.Length - 1];
                if (suffix == "")
                {
                    if (name.Contains("."))
                        continue;
                }
                else
                {
                    if (!name.EndsWith(suffix))
                        continue;
                }
                if (!fileList.Contains(name))
                    fileList.Add(name);
            }
        }
        return fileList;
    }
}
