using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlanet
{
    public string Name { get; set; }
    public int districtCount { get; set; }
    //public List<BaseSlot> district { get; set; }
    public Dictionary<Vector2, BaseSlot> district { get; set; }
    public PlanetType planetType { get; set; }
    public float distance { get; set; }
    public bool isInited = false;
    public void Init()
    {
        isInited = true;
        //Name = systemName + " " + Tools.intToRoman(num);
        //districtCount = Random.Range(planetType.minSlotNum, planetType.maxSlotNum);
        district = new Dictionary<Vector2, BaseSlot>();
        Dictionary<Vector2, float> calNoise = new Dictionary<Vector2, float>();
        int rangeX = (int)Mathf.Clamp((12f * (districtCount / 20f)) + 1, 0, 12f);
        int rangeY = (int)Mathf.Clamp((10f * (districtCount / 20f)) + 1, 0, 10f);

        for (int x = -12; x <= 12; x++)
        {
            for (int y = -10; y <= 10; y++)
            {
                if(x>=-rangeX && x<=rangeX && y>=-rangeY && y<=rangeY)
                {
                    calNoise.Add(new Vector2(x, y), Random.Range(planetType.minHeight, planetType.maxHeight));
                }
                BaseSlot slot = new BaseSlot();
                slot.enabled = false;
                district.Add(new Vector2(x, y), slot);
            }
        }
        for (int x = -rangeX; x <= rangeX; x ++)
        {
            for (int y = -rangeY; y <= rangeY; y ++)
            {
                int count = 0;
                float height = 0;
                try
                {
                    height += calNoise[new Vector2(x - 1, y)];
                    count += 1;
                }catch
                {

                }
                try
                {
                    height += calNoise[new Vector2(x - 1, y - 1)];
                    count += 1;
                }
                catch
                {

                }
                try
                {
                    height += calNoise[new Vector2(x, y - 1)];
                    count += 1;
                }
                catch
                {

                }
                try
                {
                    height += calNoise[new Vector2(x, y + 1)];
                    count += 1;
                }
                catch
                {

                }
                try
                {
                    height += calNoise[new Vector2(x + 1, y)];
                    count += 1;
                }
                catch
                {

                }
                try
                {
                    height += calNoise[new Vector2(x + 1, y - 1)];
                    count += 1;
                }
                catch
                {

                }
                calNoise[new Vector2(x, y)] = height / count;
            }
        }
        for (int x = -rangeX; x <= rangeX; x++)
        {
            for (int y = -rangeY; y <= rangeY; y++)
            {
                district[new Vector2(x, y)].slotType = Tools.GetPlanetSlotRule(planetType.slotGenRule, calNoise[new Vector2(x, y)]);
            }
        }

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
        isInited = true;
    }
}
