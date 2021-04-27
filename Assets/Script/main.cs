using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitTestDatas();
        Config.Instance.SaveData();

        Config.Instance.LoadTypes();
        UniverseController.Instance.Init();
        UIController.Instance.Init();
        //UIController.Instance.universeController = UniverseController.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void InitTestDatas()
    {
        Config.Instance.starTypes = new List<StarType>();
        Config.Instance.planetTypes = new List<PlanetType>();
        Config.Instance.slotTypes = new List<SlotType>();
        Config.Instance.constructionTypes = new List<ConstructionType>();
        Config.Instance.resourceTypes = new List<ResourcesType>();

        StarType starType1 = new StarType();
        starType1.starName = "棕矮星";
        starType1.starEnergy = 5;
        starType1.maxPlanets = 4;
        starType1.maxPlanets = 6;
        starType1.description = "暗淡的棕色恒星";
        Config.Instance.starTypes.Add(starType1);

        StarType starType2 = new StarType();
        starType2.starName = "黄矮星";
        starType2.starEnergy = 7;
        starType2.maxPlanets = 6;
        starType2.maxPlanets = 8;
        starType2.description = "黄色的恒星";
        Config.Instance.starTypes.Add(starType2);

        PlanetType planetType1 = new PlanetType();
        planetType1.planetName = "陆地行星";
        planetType1.minSlotNum = 5;
        planetType1.maxSlotNum = 10;
        planetType1.description = "覆盖着大陆的行星";
        Config.Instance.planetTypes.Add(planetType1);

        PlanetType planetType2 = new PlanetType();
        planetType2.planetName = "海洋行星";
        planetType2.minSlotNum = 5;
        planetType2.maxSlotNum = 10;
        planetType2.description = "覆盖着海洋的行星";
        Config.Instance.planetTypes.Add(planetType2);

        ResourcesType rt1 = new ResourcesType();
        rt1.resourceName = "金属";
        rt1.description = "这是金属";
        Config.Instance.resourceTypes.Add(rt1);
        ResourcesType rt2 = new ResourcesType();
        rt2.resourceName = "有机物";
        rt2.description = "通常可以食用";
        Config.Instance.resourceTypes.Add(rt2);

        SlotType slotType1 = new SlotType();
        slotType1.slotName = "平原";
        slotType1.minConstructionNum = 5;
        slotType1.maxConstructionNum = 10;
        slotType1.resources = new Dictionary<string, int>();
        slotType1.resources.Add("金属", 5);
        slotType1.resources.Add("有机物", 8);
        Config.Instance.slotTypes.Add(slotType1);

        SlotType slotType2 = new SlotType();
        slotType2.slotName = "平原";
        slotType2.minConstructionNum = 5;
        slotType2.maxConstructionNum = 10;
        slotType2.resources = new Dictionary<string, int>();
        slotType2.resources.Add("金属", 5);
        slotType2.resources.Add("有机物", 8);
        Config.Instance.slotTypes.Add(slotType2);

        ConstructionType constructionType1 = new ConstructionType();
        constructionType1.constructionName = "基础发电厂";
        constructionType1.hitPoints = 100;
        constructionType1.requiredResource = new Dictionary<string, int>();
        constructionType1.requiredResource.Add("金属", 10);
        constructionType1.requiredResource.Add("有机物", 8);
        constructionType1.description = "用于发电的厂房";
        Config.Instance.constructionTypes.Add(constructionType1);

        ConstructionType constructionType2 = new ConstructionType();
        constructionType2.constructionName = "基础农场";
        constructionType2.hitPoints = 120;
        constructionType2.requiredResource = new Dictionary<string, int>();
        constructionType2.requiredResource.Add("金属", 5);
        constructionType2.requiredResource.Add("有机物", 12);
        constructionType2.description = "用于生产有机物";
        Config.Instance.constructionTypes.Add(constructionType2);

        }
}
