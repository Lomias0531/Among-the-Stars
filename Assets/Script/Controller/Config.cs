using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using LitJson;

public class Config : BaseController<Config>
{
    public string PlayerFactionName;
    public string PlayerMotherWorldName;
    public string PlayerName;

    public Dictionary<string, StarType> starTypes;
    public Dictionary<string, PlanetType> planetTypes;
    public Dictionary<string, SlotType> slotTypes;
    public Dictionary<string, ConstructionType> constructionTypes;
    public Dictionary<string, ResourcesType> resourceTypes;
    public GenerateRule rule;

    public Dictionary<int, Color> colorDic = new Dictionary<int, Color>()
    {
        {0,new Color(0.57f,1,0)},
        {1,new Color(0.85f,0.63f,0.38f)},
        {2,new Color(0,0.41f,1)},
        {3,new Color(0.08f,0.63f,0)},
        {4,new Color(0.19f,0.35f,0.17f)},
        {5,new Color(0,0.86f,0)},
        {6,new Color(1,0.34f,0)},
        {7,new Color(0,0,0)},
        {8,new Color(1,0.28f,0)},
        {9,new Color(1,0.91f,0)},
        {10,new Color(0.8f,1,0)},
        {11,new Color()},
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public string GetFactionName()
    {
        return PlayerFactionName;
    }
    public void LoadTypes()
    {
        GetStarTypes();
        GetPlanetTypes();
        GetResourceTypes();
        GetSlotTypes();
        GetConstructionTypes();
    }
    //public void SaveData()
    //{
    //    Tools.Serialize(starTypes, Application.streamingAssetsPath + "/Types/", "Stars.json");
    //    Tools.Serialize(planetTypes, Application.streamingAssetsPath + "/Types/", "Planets.json");
    //    Tools.Serialize(slotTypes, Application.streamingAssetsPath + "/Types/", "Slots.json");
    //    Tools.Serialize(constructionTypes, Application.streamingAssetsPath + "/Types/", "Constructions.json");
    //    Tools.Serialize(resourceTypes, Application.streamingAssetsPath + "/Types/", "Resources.json");
    //    Tools.Serialize(rule, Application.streamingAssetsPath + "/Rule/", "SystemType.json");
    //}
    public void GetStarTypes()
    {
        string filePath = Application.streamingAssetsPath + "/Types/Stars.json";
        starTypes = JsonConvert.DeserializeObject<Dictionary<string, StarType>>(File.ReadAllText(filePath));
    }
    public void GetPlanetTypes()
    {
        string filePath = Application.streamingAssetsPath + "/Types/Planets.json";
        planetTypes = JsonConvert.DeserializeObject<Dictionary<string, PlanetType>>(File.ReadAllText(filePath));
    }
    public void GetSlotTypes()
    {
        string filePath = Application.streamingAssetsPath + "/Types/Slots.json";
        //slotTypes = JsonMapper.ToObject<List<SlotType>>(File.ReadAllText(filePath));
        slotTypes = JsonConvert.DeserializeObject<Dictionary<string, SlotType>>(File.ReadAllText(filePath));
    }
    public void GetConstructionTypes()
    {
        string filePath = Application.streamingAssetsPath + "/Types/Constructions.json";
        constructionTypes = JsonConvert.DeserializeObject<Dictionary<string, ConstructionType>>(File.ReadAllText(filePath));
    }
    public void GetResourceTypes()
    {
        string filePath = Application.streamingAssetsPath + "/Types/Resources.json";
        resourceTypes = JsonConvert.DeserializeObject<Dictionary<string, ResourcesType>>(File.ReadAllText(filePath));
    }
}
