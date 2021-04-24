using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Config : BaseController<Config>
{
    public string PlayerFactionName;
    public string PlayerMotherWorldName;
    public string PlayerName;

    public List<StarType> starTypes;
    public List<PlanetType> planetTypes;
    public List<SlotType> slotTypes;
    public List<ConstructionType> constructionTypes;
    public List<ResourcesType> resourceTypes;
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
        GetSlotTypes();
        GetConstructionTypes();
        GetResourceTypes();
    }
    public void GetStarTypes()
    {
        string folderPath = Application.streamingAssetsPath + "Types/Stars/";
        List<string> files = Tools.LoadFilesBySuffix(folderPath, ".json");
        if (files.Count == 0)
        {
            Debug.Log("Get stars type error!");
            return;
        }
        foreach (var item in files)
        {
            starTypes.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<StarType>(File.ReadAllText(folderPath + item)));
        }
    }
    public void GetPlanetTypes()
    {
        string folderPath = Application.streamingAssetsPath + "Types/Planets/";
        List<string> files = Tools.LoadFilesBySuffix(folderPath, ".json");
        if (files.Count == 0)
        {
            Debug.Log("Get planets type error!");
            return;
        }
        foreach (var item in files)
        {
            planetTypes.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<PlanetType>(File.ReadAllText(folderPath + item)));
        }
    }
    public void GetSlotTypes()
    {
        string folderPath = Application.streamingAssetsPath + "Types/Slots/";
        List<string> files = Tools.LoadFilesBySuffix(folderPath, ".json");
        if (files.Count == 0)
        {
            Debug.Log("Get slots type error!");
            return;
        }
        foreach (var item in files)
        {
            slotTypes.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<SlotType>(File.ReadAllText(folderPath + item)));
        }
    }
    public void GetConstructionTypes()
    {
        string folderPath = Application.streamingAssetsPath + "Types/Constructions/";
        List<string> files = Tools.LoadFilesBySuffix(folderPath, ".json");
        if (files.Count == 0)
        {
            Debug.Log("Get constructions type error!");
            return;
        }
        foreach (var item in files)
        {
            constructionTypes.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<ConstructionType>(File.ReadAllText(folderPath + item)));
        }
    }
    public void GetResourceTypes()
    {
        string folderPath = Application.streamingAssetsPath + "Types/Resources/";
        List<string> files = Tools.LoadFilesBySuffix(folderPath, ".json");
        if (files.Count == 0)
        {
            Debug.Log("Get resources type error!");
            return;
        }
        foreach (var item in files)
        {
            resourceTypes.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<ResourcesType>(File.ReadAllText(folderPath + item)));
        }
    }
}
