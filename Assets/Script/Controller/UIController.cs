using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : BaseController<UIController>
{
    public UniverseController universeController;
    public GameObject GalaticalMap;
    public GameObject StarSystemSelection;
    // Start is called before the first frame update
    void OnEnable()
    {

    }
    public void Init()
    {
        StarSystemSelection = Instantiate((GameObject)Resources.Load("Prefab/StarSystemSelection"));
        StarSystemSelection.transform.SetParent(GameObject.Find("Canvas").transform);
        StarSystemSelection.SetActive(false);
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
    public void EnableStarSystemSelection(Vector3 pos)
    {
        StarSystemSelection.SetActive(true);
        StarSystemSelection.transform.position = pos;
    }
    public void DisableStarSystemSelection()
    {
        StarSystemSelection.SetActive(false);
    }
}
