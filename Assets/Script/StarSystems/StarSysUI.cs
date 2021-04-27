using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StarSysUI : MonoBehaviour
{

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
        Debug.Log("EEE1" + gameObject.name);
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        UIController.Instance.EnableStarSystemSelection(pos);
    }
    void OnMouseExit()
    {
        Debug.Log("RRR1" + gameObject.name);
        UIController.Instance.DisableStarSystemSelection();
    }
}
