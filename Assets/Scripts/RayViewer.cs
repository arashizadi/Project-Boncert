using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    public float weaponRange = 50f;
    public Camera Camera;

     void Start()
    {
        Camera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        Vector3 lineOrigin = Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(lineOrigin, Camera.transform.forward * weaponRange, Color.green);
    }

}
