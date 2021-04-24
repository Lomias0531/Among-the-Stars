using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Config.Instance.LoadTypes();
        UniverseController.Instance.Init();
        UIController.Instance.universeController = UniverseController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
