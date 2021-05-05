using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class toggle : NetworkBehaviour
{
    public GameObject drone;
    public GameObject cam;
    //public GameObject cube;
    public bool camstate;

    LookingMain lookingScript;


    // main camera starts off displaying the cityscape
    // once the player spawns in, tell the camera to follow the player
    public override void OnStartLocalPlayer()
    {
        lookingScript = Camera.main.GetComponent<LookingMain>();
        lookingScript.enabled = true;

        lookingScript.Follow(transform, new Vector3(0f, 2.23f, -4f));
    }


    void Start()
    {   
        camstate = true;
    }
   
     void Update()
    {



        //if (Input.GetKeyUp(KeyCode.H))
        //{
        //    //moving = false;
        //    GetComponent<Movement>().enabled = !GetComponent<Movement>().enabled;
        //    //GetComponent<Movement2>().enabled = !GetComponent<Movement2>().enabled;
        //    camstate = !camstate;
        //    //GetComponent<flight>().enabled = !GetComponent<flight>().enabled;
        //    drone.SetActive(!camstate);
        //    cam.SetActive(camstate);
        //    //Transform.GetChild<Cube>().enabled= !GetComponentInChildren<Cube>().enabled;
        //    //this.gameObject.transform.GetChild(2).enabled = !this.gameObject.transform.GetChild(2).enabled;


        //}

      




    }































}