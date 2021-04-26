using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : BaseController<UIController>
{
    public UniverseController universeController;
    public GameObject GalaticalMap;
    public GameObject StarSystemSelection;
    // Start is called before the first frame update
    void Start()
    {
        StarSystemSelection = (GameObject)Resources.Load("Prefab/StarSystemSelection");
        StarSystemSelection.SetActive(false);
    }

    // Update is called once per frame 
    void Update()
    {
        
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
