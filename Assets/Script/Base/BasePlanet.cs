using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlanet
{
    public string Name { get; set; }
    public int districtCount { get; set; }
    public List<BaseSlot> district { get; set; }
    public string planetType { get; set; }
    public float distance { get; set; }
    public void Init(string systemName,int num,float dist,PlanetType type)
    {
        Name = systemName + " " + Tools.intToRoman(num);
        distance = dist;
        districtCount = Random.Range(3, 15);
        district = new List<BaseSlot>();
        for(int i = 0;i<districtCount;i++)
        {
            BaseSlot slot = new BaseSlot();
            //slot.slotType = Tools.getRule(planetType);
            slot.Init();
            district.Add(slot);
        }
    }
    public void Init(BaseSystem sys)
    {
        Name = sys.Name;
        distance = 0;
        districtCount = (int)sys.StarEnergy;
        district = new List<BaseSlot>();
        for(int i = 0;i<districtCount;i++)
        {
            BaseSlot slot = new BaseSlot();
            slot.Init();
            district.Add(slot);
        }
    }
}
