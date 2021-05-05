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
    // Start is called before the first frame update
    void OnEnable()
    {

    }
    public void Init()
    {
        StarSystemSelection = Instantiate((GameObject)Resources.Load("Prefab/StarSystemSelection"));
        StarSystemSelection.transform.SetParent(GameObject.Find("Canvas").transform);
        DisplayInfo = Instantiate((GameObject)Resources.Load("Prefab/DisplayInfo"));
        DisplayInfo.transform.SetParent(GameObject.Find("Canvas").transform);
        StarSystemSelection.SetActive(false);
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
    public void SwitchSight(SightStatus sight)
    {
        switch(sight)
        {
            case SightStatus.Galaxy:
                {
                    curStarSystem = null;
                    StarSystemSight.SetActive(false);
                    break;
                }
            case SightStatus.StarSystem:
                {
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
            GameObject plt = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Planet"));
            byte[] bytes = File.ReadAllBytes(Application.streamingAssetsPath.Replace("StreamingAssets", "Resources") + "/Image/PlanetPic/" + _planet.planetType + ".png");
            Texture2D tex = new Texture2D(100, 100);
            tex.LoadImage(bytes);
            plt.GetComponent<Image>().sprite = Sprite.Create(tex,new Rect(0,0,100,100),new Vector2(0.5f,0.5f));
            plt.transform.SetParent(planetsContainer);
            plt.transform.localPosition = new Vector2(_planet.distance * 100f, 0f);
            plt.transform.localScale = new Vector2(_planet.districtCount / 25f, _planet.districtCount / 25f);
            starSystemPlanets.Add(plt);
        }
    }
    void InitPlanetSight(BasePlanet planet)
    {
        SwitchSight(SightStatus.Planet);
        curSight = SightStatus.Planet;
    }
}
