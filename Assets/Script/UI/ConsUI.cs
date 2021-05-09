using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsUI : MonoBehaviour
{
    public Slider sld_Progress;
    public Slider sld_HP;
    public Text txt_Name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitCons(BaseBuilding cons)
    {
        if(string.IsNullOrEmpty(cons.Name))
        {
            txt_Name.text = "空闲";
        }
        else
        {
            txt_Name.text = cons.Name;
        }
        sld_Progress.maxValue = cons.progressMax;
        sld_Progress.value = cons.progress;
        sld_HP.maxValue = cons.healthMax;
        sld_HP.value = cons.health;
    }
}
