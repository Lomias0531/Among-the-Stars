using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseController<CameraController>
{
    //public float viewDistance;
    float maxDistance = 60;
    float minDistance = 10;
    public float moveSpeed = 5;
    float x;
    float y;
    public float arcSpeed = 1f;
    public Vector2 lastMousePos;
    public GameObject focus;

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
            Vector3 originalPos = focus.transform.position;
            Quaternion originalRotation = focus.transform.rotation;
            //向右滑动时正值，向左滑动是负值。
            //transform.RotateAround(UniverseController.Instance.Focus, new Vector3(0,1f,0), arcSpeed * Input.GetAxis("Mouse X"));
            focus.transform.localEulerAngles = new Vector3(focus.transform.localEulerAngles.x - arcSpeed * Input.GetAxis("Mouse Y"), focus.transform.localEulerAngles.y + arcSpeed * Input.GetAxis("Mouse X"), focus.transform.localEulerAngles.z);

            //transform.RotateAround(UniverseController.Instance.Focus, new Vector3(1f, 0, 0), -arcSpeed * Input.GetAxis("Mouse Y"));
            //focus.transform.Rotate(new Vector3(1f, 0, 0), -arcSpeed * Input.GetAxis("Mouse Y"));

            transform.LookAt(focus.transform);

            //限制摄像机垂直滑动的距离；
            float x = focus.transform.localEulerAngles.x;
            if (x < 0 || x > 60)//当超出范围之后，我们将属性归位原来的，就是让旋转无效；
            {
                focus.transform.position = originalPos;
                focus.transform.rotation = originalRotation;
            }

        }
        float t = focus.transform.position.y;
        if (Input.GetKey("a"))
        {
            focus.transform.Translate(new Vector3(-0.01f, 0, 0), Space.Self);
        }
        if (Input.GetKey("d"))
        {
            focus.transform.Translate(new Vector3(0.01f, 0, 0), Space.Self);
        }
        if (Input.GetKey("w"))
        {
            focus.transform.Translate(new Vector3(0, 0, 0.01f), Space.Self);
        }
        if (Input.GetKey("s"))
        {
            focus.transform.Translate(new Vector3(0, 0, -0.01f), Space.Self);
        }
        focus.transform.position = new Vector3(focus.transform.position.x, t, focus.transform.position.z);
        //每次更新一下。
        offsetPosition = transform.position - UniverseController.Instance.Focus;
    }
}
