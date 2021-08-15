using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseController : BaseController<UniverseController>
{
    public List<BaseGalaxy> universe { get; set; }
    public GameObject systemContainer;
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
        systemContainer = GameObject.Find("StarSystems");
        int galaxycount = Random.Range(1, 1);
        universe = new List<BaseGalaxy>();
        for(int i = 0;i<galaxycount;i++)
        {
            BaseGalaxy galaxy = systemContainer.AddComponent<BaseGalaxy>();
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
        CameraController.Instance.focus.transform.position = Focus;
        Camera.main.transform.position = new Vector3(Focus.x, Focus.y + 3, Focus.z - 10);
        Camera.main.transform.LookAt(Focus);
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
    public float starMinEnergy = 0;
    public int maxPlanets = 10;
    public float maxDistance = 5;
    public string description;
    public string planetGenRule;
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
    public string slotGenRule;
    public int minHeight = 0;
    public int maxHeight = 1;
}
[System.Serializable]
public class SlotType
{
    public string slotName = "Example";
    public int minConstructionNum = 5;
    public int maxConstructionNum = 10;
    public string description;
    public string resGenRule;
}
[System.Serializable]
public class ConstructionType
{
    public string constructionName = "Example";
    public int hitPoints = 100;
    public Dictionary<string, int> requiredResource = new Dictionary<string, int>();
    public List<string> requiredTech = new List<string>();
    public Dictionary<string, int> production = new Dictionary<string, int>();
    public Dictionary<string, int> maintenance = new Dictionary<string, int>();
    public string description;
}
[System.Serializable]
public class ResourcesType
{
    public string resourceName = "Example";
    public string description;
}
[System.Serializable]
public class PlanetSlotGenerateRule
{
    public string ruleName;
    public string ruleType;
    public List<PlanetSlotSingleRule> rules;
}
[System.Serializable]
public class PlanetSlotSingleRule
{
    public string SlotName;
    public int minHeight;
    public int maxHeight;
    public int weight;
}