using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding
{
    public string Name { get; set; }
    public int progress { get; set; }
    public int progressMax { get; set; }
    public int health { get; set; }
    public int healthMax { get; set; }
    public void Init()
    {
        Name = "空闲";
        progress = 1;
        progressMax = 1;
        health = 1;
        healthMax = 1;
    }
}
