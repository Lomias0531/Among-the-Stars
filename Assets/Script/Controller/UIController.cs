using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public enum SightStatus
{
    Galaxy,
    StarSystem,
    Planet,
}
public class UIController : BaseController<UIController>
{
    public UniverseController universeController;
    [Header("银河系视图")]
    //public GameObject GalaticalMap;
    public GameObject StarSystemSelection;
    public GameObject DisplayInfo;
    public GameObject StarSystemSight;
    public GameObject PlanetSight;
    public SightStatus curSight;

    public BaseSystem curStarSystem;
    public BasePlanet curPlanet;
    public BaseSlot curSlot;

    //恒星系视图
    [Header("恒星系视图")]
    public GameObject PlanetSelection;
    public GameObject planet;
    public Transform planetsContainer;
    public Transform fleetsContainer;
    List<GameObject> starSystemPlanets;
    List<GameObject> starSystemFleets;

    //行星视图
    [Header("行星视图")]
    public GameObject SlotSelection;
    public GameObject slot;
    public Transform slotsContainer;
    List<GameObject> planetSlots;
    public InputField slotName;
    public InputField slotType;
    public Button btn_SlotResources;
    public Button btn_SlotConstructions;
    public Button btn_SlotTroops;
    public GameObject ConsContainer;
    public GameObject TroopsContainer;
    public GameObject ResContainer;
    bool isSlotSelected;
    List<GameObject> constructions;
    public GameObject ConsPrefab;
    // Start is called before the first frame update
    void OnEnable()
    {
        KeyBindings();
    }
    public void Init()
    {
        StarSystemSelection = Instantiate(StarSystemSelection);
        StarSystemSelection.transform.SetParent(GameObject.Find("Canvas").transform);
        PlanetSelection = Instantiate(PlanetSelection);
        PlanetSelection.transform.SetParent(StarSystemSight.transform);
        SlotSelection = Instantiate(SlotSelection);
        SlotSelection.transform.SetParent(slotsContainer.transform);
        DisplayInfo = Instantiate(DisplayInfo);
        DisplayInfo.transform.SetParent(GameObject.Find("Canvas").transform);
        StarSystemSelection.SetActive(false);
        PlanetSelection.SetActive(false);
        SlotSelection.SetActive(false);
        DisplayInfo.transform.localScale = new Vector3(1, 0, 1);
        universeController = UniverseController.Instance;
        curSight = SightStatus.Galaxy;
        starSystemPlanets = new List<GameObject>();
        starSystemFleets = new List<GameObject>();
        planetSlots = new List<GameObject>();
        constructions = new List<GameObject>();
    }
    void KeyBindings()
    {
        btn_SlotConstructions.onClick.AddListener(TriggerSlotConsPanel);
        btn_SlotTroops.onClick.AddListener(TriggerSlotTroopsPanel);
        btn_SlotResources.onClick.AddListener(TriggerSlotResPanel);
    }

    // Update is called once per frame 
    void Update()
    {
        if(StarSystemSelection.activeSelf)
        {
            float scale = 30f/ Camera.main.fieldOfView;
            StarSystemSelection.transform.localScale = new Vector3(scale,scale,1);
        }
        if(Input.GetMouseButtonDown(0))
        {
            switch(curSight)
            {
                case SightStatus.Galaxy:
                    {
                        if(curStarSystem != null)
                        {
                            InitStarSystemSight(curStarSystem);
                            UndeployInfoDisplay();
                        }else
                        {
                            return;
                        }
                        break;
                    }
                case SightStatus.StarSystem:
                    {
                        if(curPlanet != null)
                        {
                            InitPlanetSight(curPlanet);
                            UnDeployPlanetInfo();
                        }else
                        {
                            return;
                        }
                        break;
                    }
                case SightStatus.Planet:
                    {
                        if(curSlot != null)
                        {
                            isSlotSelected = true;
                            displaySlotDetail();
                        }else
                        {
                            return;
                        }
                        break;
                    }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            switch(curSight)
            {
                case SightStatus.Planet:
                    {
                        isSlotSelected = false;
                        curSlot = null;
                        DisableSlotSelection();
                        UnloadSlotInfo();
                        UnloadSlotDetail();
                        break;
                    }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(curSight)
            {
                case SightStatus.Galaxy:
                    {
                        break;
                    }
                case SightStatus.StarSystem:
                    {
                        SwitchSight(SightStatus.Galaxy);
                        curSight = SightStatus.Galaxy;
                        break;
                    }
                case SightStatus.Planet:
                    {
                        SwitchSight(SightStatus.StarSystem);
                        curSight = SightStatus.StarSystem;
                        break;
                    }
            }
        }
    }
    public void EnableStarSystemSelection(Vector3 pos, BaseSystem sys)
    {
        if(curSight == SightStatus.Galaxy)
        {
            StarSystemSelection.SetActive(true);
            StarSystemSelection.transform.position = pos;
            DisplayInfo.transform.position = pos;
            DeployInfoDisplay(sys);
        }
    }
    public void DisableStarSystemSelection()
    {
        StarSystemSelection.SetActive(false);
        UndeployInfoDisplay();
    }
    void DeployInfoDisplay(BaseSystem sys)
    {
        curStarSystem = sys;
        DisplayInfo.GetComponent<InfoDisplay>().InitInfo(sys);
        DisplayInfo.transform.DOScaleY(1, 0.1f);
    }
    void UndeployInfoDisplay()
    {
        curStarSystem = null;
        DisplayInfo.transform.DOScaleY(0, 0.1f);
    }
    public void EnablePlanetSelection(Vector3 pos,BasePlanet planet)
    {
        if(curSight == SightStatus.StarSystem)
        {
            PlanetSelection.SetActive(true);
            PlanetSelection.transform.position = pos;
            DisplayInfo.transform.position = pos;
            DeployPlanetInfo(planet);
        }
    }
    public void DisablePlanetSelection()
    {
        PlanetSelection.SetActive(false);
        UnDeployPlanetInfo();
    }
    void DeployPlanetInfo(BasePlanet planet)
    {
        curPlanet = planet;
        DisplayInfo.GetComponent<InfoDisplay>().InitInfo(planet);
        DisplayInfo.transform.DOScaleY(1, 0.1f);
    }
    void UnDeployPlanetInfo()
    {
        curPlanet = null;
        DisplayInfo.transform.DOScaleY(0, 0.1f);
    }
    public void EnableSlotSelection(Vector3 pos,BaseSlot slot)
    {
        if(!isSlotSelected)
        {
            SlotSelection.SetActive(true);
            SlotSelection.transform.position = pos;
            curSlot = slot;
            TriggerSlotInfo(slot);
        }
    }
    public void DisableSlotSelection()
    {
        if(!isSlotSelected)
        {
            SlotSelection.SetActive(false);
            UnloadSlotInfo();
        }
    }
    public void SwitchSight(SightStatus sight)
    {
        switch(sight)
        {
            case SightStatus.Galaxy:
                {
                    universeController.systemContainer.SetActive(true);
                    curStarSystem = null;
                    StarSystemSight.SetActive(false);
                    break;
                }
            case SightStatus.StarSystem:
                {
                    universeController.systemContainer.SetActive(false);
                    StarSystemSelection.SetActive(false);
                    curPlanet = null;
                    PlanetSight.SetActive(false);
                    StarSystemSight.SetActive(true);
                    break;
                }
            case SightStatus.Planet:
                {
                    PlanetSelection.SetActive(false);
                    StarSystemSight.SetActive(false);
                    PlanetSight.SetActive(true);
                    break;
                }
        }
    }
    void InitStarSystemSight(BaseSystem sys)
    {
        SwitchSight(SightStatus.StarSystem);
        curSight = SightStatus.StarSystem;
        foreach (var item in starSystemPlanets)
        {
            Destroy(item);
        }
        starSystemPlanets.Clear();
        foreach (var item in starSystemFleets)
        {
            Destroy(item);
        }
        starSystemFleets.Clear();
        foreach (var _planet in sys.planets)
        {
            GameObject plt = GameObject.Instantiate(planet);
            byte[] bytes = File.ReadAllBytes(Application.streamingAssetsPath.Replace("StreamingAssets", "Resources") + "/Image/PlanetPic/" + _planet.planetType + ".png");
            Texture2D tex = new Texture2D(100, 100);
            tex.LoadImage(bytes);
            plt.GetComponent<Image>().sprite = Sprite.Create(tex,new Rect(0,0,100,100),new Vector2(0.5f,0.5f));
            plt.transform.SetParent(planetsContainer);
            plt.transform.localPosition = new Vector2((_planet.distance == 0 ? _planet.distance : _planet.distance + 1) * 100f, 0f);
            plt.transform.localScale = new Vector2(_planet.districtCount / 25f, _planet.districtCount / 25f);
            starSystemPlanets.Add(plt);
            plt.AddComponent<PlanetUI>().thisPlanet = _planet;
        }
    }
    void InitPlanetSight(BasePlanet planet)
    {
        SwitchSight(SightStatus.Planet);
        curSight = SightStatus.Planet;
        foreach (var item in planetSlots)
        {
            Destroy(item);
        }
        planetSlots.Clear();
        foreach (var plt in planet.district)
        {
            GameObject slt = GameObject.Instantiate(slot);
            slt.transform.SetParent(slotsContainer);
            float curX = 0;
            float curY = 0;
            if (plt.Key.x % 2 != 0)
            {
                curY = 13;
            }
            curX += plt.Key.x * 22.5f;
            curY += plt.Key.y * 26;
            slt.transform.localPosition = new Vector2(curX, curY);
            if(string.IsNullOrEmpty(plt.Value.slotType))
            {
                slt.GetComponent<Image>().color = Color.gray;
            }else
            {
                if (!plt.Value.enabled)
                {
                    int col = Config.Instance.slotTypes.Keys.ToList().IndexOf(plt.Value.slotType);
                    slt.GetComponent<Image>().color = Config.Instance.colorDic[col];
                    GameObject sle = Instantiate(slot);
                    sle.transform.SetParent(slt.transform);
                    sle.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.3f);
                    sle.transform.localPosition = new Vector3(0, 0, 0);
                }
                else
                {
                    slt.AddComponent<SlotUI>().thisSlot = plt.Value;
                    int col = Config.Instance.slotTypes.Keys.ToList().IndexOf(plt.Value.slotType);
                    slt.GetComponent<Image>().color = Config.Instance.colorDic[col];
                }
            }
            planetSlots.Add(slt);
        }
        SlotSelection.transform.SetAsLastSibling();
    }
    void TriggerSlotInfo(BaseSlot slot)
    {
        slotName.text = slot.Name;
        slotType.text = slot.slotType;
    }
    void UnloadSlotInfo()
    {
        slotName.text = "";
        slotType.text = "";
    }
    void displaySlotDetail()
    {
        foreach (var item in constructions)
        {
            Destroy(item);
        }
        constructions.Clear();
        foreach (var cons in curSlot.buildings)
        {
            GameObject construction = Instantiate(ConsPrefab);
            construction.transform.SetParent(ConsContainer.transform);
            constructions.Add(construction);
            construction.GetComponent<ConsUI>().InitCons(cons);
        }
    }
    void UnloadSlotDetail()
    {
        foreach (var item in constructions)
        {
            Destroy(item);
        }
        constructions.Clear();
    }
    void TriggerSlotConsPanel()
    {
        ConsContainer.transform.SetAsLastSibling();
    }
    void TriggerSlotTroopsPanel()
    {
        TroopsContainer.transform.SetAsLastSibling();
    }
    void TriggerSlotResPanel()
    {
        ResContainer.transform.SetAsLastSibling();
    }
}
