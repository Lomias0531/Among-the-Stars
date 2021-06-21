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
        for(int i = 0;i<4;i++)
        {
            StartCoroutine(initStarSystems(x, y));
        }
        do
        {

        } while (isInitFinished<4);
    }
    private IEnumerator initStarSystems(int x,int y)
    {
        for (int i = 0; i < x/2; i++)
        {
            for (int k = 0; k < y/2; k++)
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
        isInitFinished += 1;
        yield return null;
    }
}
