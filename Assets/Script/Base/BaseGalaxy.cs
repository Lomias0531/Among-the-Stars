using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGalaxy
{
    public List<BaseSystem> systems { get; set; }
    public string Name { get; set; }
    public int systemCount { get; set; }

    public void Init()
    {
        Name = Tools.GetRandomName(4, 8);
        int x = Random.Range(15, 25);
        int y = Random.Range(15, 25);
        systemCount = x * y;
        systems = new List<BaseSystem>();
        for(int i = 0;i<x;i++)
        {
            for(int k = 0;k<y;k++)
            {
                BaseSystem system = new BaseSystem();
                system.coordX = x;
                system.coordY = y;
                system.coordZ = 0;
                string Name = Tools.getRule("StarGen");
                StarType type = Config.Instance.starTypes[Name];
                system.Init(type);
                systems.Add(system);
            }
        }
    }
}
