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
    public Dictionary<string, int> generateRule;
}
public class StarType
{
    public string starName;
    public float starEnergy;
    public int maxPlanets;
    public float maxDistance;
}
public class PlanetType
{
    public string planetName;
    public int minSlotNum;
    public int maxSlotNum;
}
public class SlotType
{
    public string slotName;
    public int minConstructionNum;
    public int maxConstructionNum;
    public List<ResourcesType> resources;
}
public class ConstructionType
{
    public string constructionName;
}
public class ResourcesType
{
    public string resourceName;
}