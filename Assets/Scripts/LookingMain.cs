using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingMain : MonoBehaviour
{
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    float sensitivity = 1.5f;
    float smoothing = 4f;
    float MouseX, MouseY;

    // for camera following
    Transform target;
    Vector3 cameraOffset;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // rotates the camera according to player input 
    void Update()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(MouseX, MouseY), Vector2.one * sensitivity * smoothing);
        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
        currentMouseLook += appliedMouseDelta;

        // stops the camera from rolling too much vertically
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -20, 50);

        // rotates camera, as well as the target
        this.transform.rotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        target.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
    }

    // camera follows a target: the character, their drone, etc.
    // (this waits until all objects in the scene have moved in order to avoid jitter)
    void LateUpdate()
    {
        // original feel: follows as if it were a child of the the target
        //transform.position = target.TransformPoint(cameraOffset);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, target.transform.eulerAngles.y, transform.eulerAngles.z);

        // smoother, "elastic" follow:
        //transform.position = Vector3.Slerp(transform.position, target.TransformPoint(cameraOffset), 0.1f);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Quaternion.LookRotation(target.position - transform.position).eulerAngles.y, transform.eulerAngles.z);


        // lol sike the drone gets all glitchy with the elastic follow so here's a fallback
        if (cameraOffset == Vector3.zero)
        {
            transform.position = target.TransformPoint(cameraOffset);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, target.transform.eulerAngles.y, transform.eulerAngles.z);
        } else {
            transform.position = Vector3.Slerp(transform.position, target.TransformPoint(cameraOffset), 0.1f);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Quaternion.LookRotation(target.position - transform.position).eulerAngles.y, transform.eulerAngles.z);
        }

    }

    public void Follow(Transform _target, Vector3 _cameraOffset)
    {
        target = _target;
        cameraOffset = _cameraOffset;
        transform.LookAt(target);
    }

}
