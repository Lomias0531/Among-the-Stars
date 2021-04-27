using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StarSysUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        UIController.Instance.EnableStarSystemSelection(pos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIController.Instance.DisableStarSystemSelection();
    }
    void OnMouseEnter()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        UIController.Instance.EnableStarSystemSelection(pos);
    }
    void OnMouseExit()
    {
        UIController.Instance.DisableStarSystemSelection();
    }
}
