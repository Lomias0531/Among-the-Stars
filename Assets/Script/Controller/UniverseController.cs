using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseController : BaseController<UniverseController>
{
    public List<BaseGalaxy> universe { get; set; }
    public Vector3 Focus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        int galaxycount = Random.Range(1, 1);
        universe = new List<BaseGalaxy>();
        for(int i = 0;i<galaxycount;i++)
        {
            BaseGalaxy galaxy = new BaseGalaxy();
            galaxy.Init();
            universe.Add(galaxy);
        }
        float x = 0;
        float y = 0;
        float z = 0;
        foreach (var item in universe[0].systems)
        {
            x += item.star.transform.position.x;
            y += item.star.transform.position.y;
            z += item.star.transform.position.z;
        }
        Focus = new Vector3(x / universe[0].systemCount, y / universe[0].systemCount, z / universe[0].systemCount);
        Camera.main.transform.position = new Vector3(Focus.x, Focus.y + 2, Focus.z - 10);
    }
}

public class GenerateRule
{
    public string ruleName;
    public string ruleType;
    public Dictionary<string, int> generateRule;
}
[System.Serializable]
public class StarType
{
    public string starName = "Example";
    public float starEnergy = 0;
    public int maxPlanets = 10;
    public float maxDistance = 5;
    public string description;
}
[System.Serializable]
public class PlanetType
{
    public string planetName = "Example";
    public int minSlotNum = 5;
    public int maxSlotNum = 10;
    public float minStarEnergy = 0;
    public float maxstarEnergy = 0;
    public string description;
}
[System.Serializable]
public class SlotType
{
    public string slotName = "Example";
    public int minConstructionNum = 5;
    public int maxConstructionNum = 10;
    public string description;
}
[System.Serializable]
public class ConstructionType
{
    public string constructionName = "Example";
    public int hitPoints = 100;
    public Dictionary<string, int> requiredResource = new Dictionary<string, int>();
    public string description;
}
[System.Serializable]
public class ResourcesType
{
    public string resourceName = "Example";
    public string description;
}