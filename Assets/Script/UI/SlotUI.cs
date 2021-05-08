using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public BaseSlot thisSlot;
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
        UIController.Instance.EnableSlotSelection(transform.position, thisSlot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIController.Instance.DisableSlotSelection();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
