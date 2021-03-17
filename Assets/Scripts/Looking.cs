using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
	//Multipler
	public float MouseSensitivity = 200f;
	//Mouse Positions
	float MouseX;
	float MouseY;

	//Player Body
	public Transform Body;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        //Player Body Turn
        Body.Rotate(Vector3.up * MouseX);
        //Camera Rotation
        transform.Rotate(Vector3.right * -MouseY);
    }
}
