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
    public void Init(string systemName,int num)
    {
        Name = systemName + " " + Tools.intToRoman(num);
        districtCount = Random.Range(3, 15);
        district = new List<BaseSlot>();
        for(int i = 0;i<districtCount;i++)
        {
            BaseSlot slot = new BaseSlot();
            slot.slotType = Tools.getTypeName(planetType);
            slot.Init();
            district.Add(slot);
        }
    }
}
