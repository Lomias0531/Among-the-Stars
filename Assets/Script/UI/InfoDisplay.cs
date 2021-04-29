using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    public InputField txt_SystemName;
    public InputField txt_SystenType;
    public InputField txt_SystemInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitInfo(BaseSystem sysInfo)
    {
        txt_SystemName.text = sysInfo.Name;
        txt_SystenType.text = sysInfo.systemType;
        string info = "";
        foreach (var planet in sysInfo.planets)
        {
            info += planet.Name + "," + planet.planetType + "\r\n";
        }
        txt_SystemInfo.text = info;
    }
}
