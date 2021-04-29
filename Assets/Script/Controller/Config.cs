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
    public void SaveData()
    {
        Tools.Serialize(starTypes, Application.streamingAssetsPath + "/Types/", "Stars.json");
        Tools.Serialize(planetTypes, Application.streamingAssetsPath + "/Types/", "Planets.json");
        Tools.Serialize(slotTypes, Application.streamingAssetsPath + "/Types/", "Slots.json");
        Tools.Serialize(constructionTypes, Application.streamingAssetsPath + "/Types/", "Constructions.json");
        Tools.Serialize(resourceTypes, Application.streamingAssetsPath + "/Types/", "Resources.json");
    }
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
