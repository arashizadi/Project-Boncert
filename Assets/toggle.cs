using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class toggle : MonoBehaviour
{
    public GameObject cam;
    public bool camstate;



    void Start()
    {
        camstate = true;
    }
   

     void Update()
    {



        if (Input.GetKeyUp(KeyCode.H))
        {
            //moving = false;
            GetComponent<Movement>().enabled = !GetComponent<Movement>().enabled;
            camstate = !camstate;
            
        }
        
        if( camstate== true)
        {
            cam.SetActive(true);
        }

        if (camstate == false)
        {
            cam.SetActive(false);
        }




    }































}