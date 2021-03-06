using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StarSysUI : MonoBehaviour
{
    public BaseSystem thisStarSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        UIController.Instance.EnableStarSystemSelection(pos,thisStarSystem);
    }
    void OnMouseExit()
    {
        UIController.Instance.DisableStarSystemSelection();
    }
}
