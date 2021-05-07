using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;

public enum SightStatus
{
    Galaxy,
    StarSystem,
    Planet,
}
public class UIController : BaseController<UIController>
{
    public UniverseController universeController;
    //public GameObject GalaticalMap;
    public GameObject StarSystemSelection;
    public GameObject PlanetSelection;
    public GameObject DisplayInfo;
    public GameObject StarSystemSight;
    public GameObject PlanetSight;
    public SightStatus curSight;

    public BaseSystem curStarSystem;
    public BasePlanet curPlanet;
    public BaseSlot curSlot;

    //恒星系视图
    [Header("恒星系视图")]
    public GameObject planet;
    public Transform planetsContainer;
    public Transform fleetsContainer;
    List<GameObject> starSystemPlanets;
    List<GameObject> starSystemFleets;

    //行星视图
    [Header("行星视图")]
    public GameObject slot;
    public Transform slotsContainer;
    List<GameObject> planetSlots;
    // Start is called before the first frame update
    void OnEnable()
    {

    }
    public void Init()
    {
        StarSystemSelection = Instantiate((GameObject)Resources.Load("Prefab/StarSystemSelection"));
        StarSystemSelection.transform.SetParent(GameObject.Find("Canvas").transform);
        PlanetSelection = Instantiate((GameObject)Resources.Load("Prefab/PlanetSelection"));
        PlanetSelection.transform.SetParent(StarSystemSight.transform);
        DisplayInfo = Instantiate((GameObject)Resources.Load("Prefab/DisplayInfo"));
        DisplayInfo.transform.SetParent(GameObject.Find("Canvas").transform);
        StarSystemSelection.SetActive(false);
        PlanetSelection.SetActive(false);
        DisplayInfo.transform.localScale = new Vector3(1, 0, 1);
        universeController = UniverseController.Instance;
        curSight = SightStatus.Galaxy;
        starSystemPlanets = new List<GameObject>();
        starSystemFleets = new List<GameObject>();
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

                        }else
                        {
                            return;
                        }
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
            if (pos.x < 135)
            {
                pos.x = 140;
            }
            if (pos.x > Screen.width - 135)
            {
                pos.x = Screen.width - 140;
            }
            if (pos.y < 210)
            {
                pos.y = 215;
            }
            if (pos.y > Screen.height - 210)
            {
                pos.y = Screen.height - 215;
            }
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
            curPlanet = planet;
        }
    }
    public void DisablePlanetSelection()
    {
        PlanetSelection.SetActive(false);
        curPlanet = null;
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
        }
    }
}
