using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingDrone : MonoBehaviour
{
    //Multipler
    public float MouseSensitivity = 200f;
    //Mouse Positions
    float MouseX;
    float MouseY;
    float newX;

    //Player Body
    public Transform Body;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //LookingDrone.localPosition = Vector3.zero;
        //LookingDrone.localEulerAngle = Vector3.zero;
    }

    void Update()
    {
        MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        //newX = Mathf.Clamp(MouseX, 30, 60);

        //Player Body Turn
        Body.Rotate(Vector3.up * MouseX);
        //Camera Rotation
        transform.Rotate(Vector3.right * -MouseY);
    }
}
