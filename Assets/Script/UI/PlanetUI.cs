using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BasePlanet thisPlanet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //void OnMouseEnter()
    //{
    //    Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
    //    UIController.Instance.EnablePlanetSelection(pos, thisPlanet);
    //}
    //void OnMouseExit()
    //{
    //    UIController.Instance.DisablePlanetSelection();
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        UIController.Instance.EnablePlanetSelection(transform.position, thisPlanet);
    }

    public void OnPointerExit(PointerEventData eventData)
    {UIController.Instance.DisablePlanetSelection();
        
    }
}
