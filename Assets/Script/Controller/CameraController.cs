using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public float viewDistance;
    float maxDistance = 60;
    float minDistance = 10;
    public float moveSpeed = 5;
    float x;
    float y;
    public float arcSpeed = 1f;
    public Vector2 lastMousePos;

    float distance;
    Vector3 offsetPosition;
    bool isRotating;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel < 0 && Camera.main.fieldOfView < maxDistance)
        {
            Camera.main.fieldOfView += moveSpeed;
        }

        if (mouseWheel > 0 && Camera.main.fieldOfView > minDistance)
        {
            Camera.main.fieldOfView -= moveSpeed;
        }
        ScrollView();
        RotateView();
    }
    private void ScrollView()
    {
        distance = 10;
        offsetPosition = offsetPosition.normalized * distance;//方向不变，将长度变为distance
    }
    void RotateView()
    {
        if (Input.GetMouseButtonUp(1))//1代表鼠标左键。
        {
            isRotating = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (isRotating)
        {
            //向右滑动时正值，向左滑动是负值。
            transform.RotateAround(UniverseController.Instance.Focus, new Vector3(0,1f,0), arcSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(UniverseController.Instance.Focus, new Vector3(1f, 0, 0), -arcSpeed * Input.GetAxis("Mouse Y"));

            transform.LookAt(UniverseController.Instance.Focus);

            //限制摄像机垂直滑动的距离；
            float x = transform.eulerAngles.x;
            if (x < 10 || x > 40)//当超出范围之后，我们将属性归位原来的，就是让旋转无效；
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }



        }
        //每次更新一下。
        offsetPosition = transform.position - UniverseController.Instance.Focus;
    }
}
