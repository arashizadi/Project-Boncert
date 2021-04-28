using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class toggle : MonoBehaviour
{
    public GameObject drone;
    public GameObject cam;
    //public GameObject cube;
    public bool camstate;
     


    void Start()
    {

        ///Movement2 moving = GetComponent < blahblahblah

        
        camstate = true;
    }
   

     void Update()
    {



        if (Input.GetKeyUp(KeyCode.H))
        {
            //moving = false;
            GetComponent<Movement>().enabled = !GetComponent<Movement>().enabled;
            //GetComponent<Movement2>().enabled = !GetComponent<Movement2>().enabled;
            camstate = !camstate;
            //GetComponent<flight>().enabled = !GetComponent<flight>().enabled;
            drone.SetActive(!camstate);
            cam.SetActive(camstate);
            //Transform.GetChild<Cube>().enabled= !GetComponentInChildren<Cube>().enabled;
            //this.gameObject.transform.GetChild(2).enabled = !this.gameObject.transform.GetChild(2).enabled;


        }

      




    }































}