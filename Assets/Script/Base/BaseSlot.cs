using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlot
{
    public string Name { get; set; }
    public List<BaseBuilding> buildings { get; set; }
    public float coordX { get; set; }
    public float coordY { get; set; }
    public string slotType { get; set; }
    public bool enabled { get; set; }
    public void Init()
    {
        Name = Tools.GetRandomName(3, 5) + " " + Tools.GetRandomName(4, 8);
        buildings = new List<BaseBuilding>();
    }
}
