using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlanet
{
    public string Name { get; set; }
    public int districtCount { get; set; }
    //public List<BaseSlot> district { get; set; }
    public Dictionary<Vector2, BaseSlot> district { get; set; }
    public string planetType { get; set; }
    public float distance { get; set; }
    public void Init(string systemName, int num, float dist, PlanetType type)
    {
        Name = systemName + " " + Tools.intToRoman(num);
        distance = dist;
        districtCount = Random.Range(type.minSlotNum, type.maxSlotNum);
        district = new Dictionary<Vector2, BaseSlot>();
        for (int x = -12; x <= 12; x++)
        {
            for (int y = -10; y <= 10; y++)
            {
                float curX = 0;
                float curY = 0;
                if (x % 2 != 0)
                {
                    curY = 13;
                }
                curX += x * 22.5f;
                curY += y * 26;
                //if (Mathf.Sqrt(Mathf.Pow(curX, 2) + Mathf.Pow(curY, 2)) > 280)
                //{
                //    continue;
                //}
                BaseSlot slot = new BaseSlot();
                slot.enabled = false;
                district.Add(new Vector2(x, y), slot);
            }
        }
        int rangeX = (int)Mathf.Clamp((12f * (districtCount / 20f)) + 1, 0, 12f);
        int rangeY = (int)Mathf.Clamp((10f * (districtCount / 20f)) + 1, 0, 10f);
        int reX;
        int reY;
        for (int i = 0; i < districtCount; i++)
        {
            do
            {
                do
                {
                    reX = Random.Range(-rangeX, rangeX);
                    reY = Random.Range(-rangeY, rangeY);
                } while (!district.ContainsKey(new Vector2(reX, reY)));
            } while (district[new Vector2(reX, reY)].enabled == true);
            district[new Vector2(reX, reY)].enabled = true;
            district[new Vector2(reX, reY)].slotType = Tools.getRule(type.slotGenRule);
            district[new Vector2(reX, reY)].Init(Config.Instance.slotTypes[district[new Vector2(reX, reY)].slotType]);
        }
    }
    public void Init(BaseSystem sys)
    {
        Name = sys.Name;
        distance = 0;
        districtCount = (int)sys.StarEnergy * 2;
        district = new Dictionary<Vector2, BaseSlot>();
        for (int x = -12; x <= 12; x++)
        {
            for (int y = -10; y <= 10; y++)
            {
                float curX = 0;
                float curY = 0;
                if (x % 2 != 0)
                {
                    curY = 13;
                }
                curX += x * 22.5f;
                curY += y * 26;
                //if (Mathf.Sqrt(Mathf.Pow(curX, 2) + Mathf.Pow(curY, 2)) > 280)
                //{
                //    continue;
                //}
                BaseSlot slot = new BaseSlot();
                slot.enabled = false;
                district.Add(new Vector2(x, y), slot);
            }
        }
        int rangeX = (int)Mathf.Clamp((12f * (districtCount / 50f)) + 1, 0, 12f);
        int rangeY = (int)Mathf.Clamp((10f * (districtCount / 50f)) + 1, 0, 10f);
        int reX;
        int reY;
        for (int i = 0; i < districtCount; i++)
        {
            do
            {
                do
                {
                    reX = Random.Range(-rangeX, rangeX);
                    reY = Random.Range(-rangeY, rangeY);
                } while (!district.ContainsKey(new Vector2(reX, reY)));
            } while (district[new Vector2(reX, reY)].enabled == true);
            district[new Vector2(reX, reY)].enabled = true;
            district[new Vector2(reX, reY)].slotType = "恒星物质";
            district[new Vector2(reX, reY)].Init(Config.Instance.slotTypes[district[new Vector2(reX, reY)].slotType]);
        }
    }
}
