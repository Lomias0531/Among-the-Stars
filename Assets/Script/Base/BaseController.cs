using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController<T> : MonoBehaviour where T:Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance != null)
                {
                    return instance;
                }
                GameObject controllerObj;
                Controller controller = GameObject.FindObjectOfType<Controller>();
                if (controller == null)
                {
                    controllerObj = new GameObject("Controller");
                    controllerObj.AddComponent<Controller>();
                }
                else
                {
                    controllerObj = controller.gameObject;
                }
                DontDestroyOnLoad(controllerObj);
                instance = controllerObj.AddComponent(typeof(T)) as T;
            }
            return instance;
        }
    }
}
