using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    public InputField txt_SystemName;
    public InputField txt_SystenType;
    public InputField txt_SystemInfo;

    public Text txt_Name;
    public Text txt_Type;
    public Text txt_Info;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AlterPosition()
    {
        Vector2 pos = transform.position;
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
        transform.position = pos;
    }
    public void InitInfo(BaseSystem sysInfo)
    {
        AlterPosition();
        txt_Name.text = "星系名称";
        txt_Type.text = "星系类型";
        txt_Info.text = "星系详情";
        txt_SystemName.text = sysInfo.Name;
        txt_SystenType.text = sysInfo.systemType;
        string info = "";
        foreach (var planet in sysInfo.planets)
        {
            info += planet.Name + "," + planet.planetType + "\r\n";
        }
        txt_SystemInfo.text = info;
    }
    public void InitInfo(BasePlanet planetInfo)
    {
        AlterPosition();
        txt_Name.text = "星球名称";
        txt_Type.text = "星球类型";
        txt_Info.text = "星球详情";
        txt_SystemName.text = planetInfo.Name;
        txt_SystenType.text = planetInfo.planetType;
        string info = "";
        foreach (var slot in planetInfo.district)
        {
            if(slot.Value.enabled)
                info += slot.Value.Name + "," + slot.Value.slotType + "\r\n";
        }
        txt_SystemInfo.text = info;
    }
}
