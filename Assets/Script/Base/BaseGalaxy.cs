using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGalaxy:MonoBehaviour
{
    public List<BaseSystem> systems { get; set; }
    public string Name { get; set; }
    public int systemCount { get; set; }
    private int isInitFinished = 0;

    public void Init()
    {
        Name = Tools.GetRandomName(4, 8);
        int x = Random.Range(15, 25);
        int y = Random.Range(15, 25);
        systemCount = x * y;
        systems = new List<BaseSystem>();
        isInitFinished = 0;
        initStarSystems();
    }
    private void initStarSystems()
    {
        for(int i = 0;i< systemCount; i++)
        {
            BaseSystem system = new BaseSystem();
            system.coordX = (float)(Tools.Rnd.NextDouble() * 0.5 - 0.5);
            system.coordY = (float)(Tools.Rnd.NextDouble() * 0.5 - 0.5);
            system.coordZ = (float)(Tools.Rnd.NextDouble() * 0.5 - 0.5);
            string Name = Tools.getRule("StarGen");
            StarType type = Config.Instance.starTypes[Name];
            system.Init(type);
            systems.Add(system);
        }
    }
}
