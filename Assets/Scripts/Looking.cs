using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Looking : NetworkBehaviour
{
	//Multipler
	public float MouseSensitivity = 200f;
	//Mouse Positions
	float MouseX;
	float MouseY;

	//Player Body
	public Transform Body;
    public Camera playerCamera;

    void Start()
    {
        if (!hasAuthority) return;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // multiplayer: stops everyone from controlling everyone
        if (!hasAuthority) return;

        MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        //Player Body Turn
        Body.Rotate(Vector3.up * MouseX);
        //Camera Rotation
        playerCamera.transform.Rotate(Vector3.right * -MouseY);
    }

}
