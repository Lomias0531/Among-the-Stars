using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : BaseController<UIController>
{
    public UniverseController universeController;
    public GameObject GalaticalMap;
    public GameObject StarSystemSelection;
    public GameObject DisplayInfo;
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
    }

    // Update is called once per frame 
    void Update()
    {
        if(StarSystemSelection.activeSelf)
        {
            float scale = 30f/ Camera.main.fieldOfView;
            StarSystemSelection.transform.localScale = new Vector3(scale,scale,1);
        }
    }
    public void CreateStarSystem(BaseSystem starSystem)
    {

    }
    public void InitStarSystem(BaseSystem starSystem)
    {

    }
    public void EnableStarSystemSelection(Vector3 pos, BaseSystem sys)
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
    public void DisableStarSystemSelection()
    {
        StarSystemSelection.SetActive(false);
        UndeployInfoDisplay();
    }
    void DeployInfoDisplay(BaseSystem sys)
    {
        DisplayInfo.GetComponent<InfoDisplay>().InitInfo(sys);
        DisplayInfo.transform.DOScaleY(1, 0.1f);
    }
    void UndeployInfoDisplay()
    {
        DisplayInfo.transform.DOScaleY(0, 0.1f);
    }
}
