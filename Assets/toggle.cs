using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class toggle : NetworkBehaviour
{
    public GameObject drone;
    Shooting shootingScript;
    flight flightScript;
    RayViewer rayViewerScript;

    public bool droneActivated = false;

    LookingMain lookingScript;
    Movement movementScript;


    void Start()
    {
        // TEMP:
        // since the player has to be spawned in, we can't drag the drone gameobject into the inspector slot - rip
        // for testing purposes i'm just gonna Find() a drone already chilling in the scene (but this is really bad and ik julian wanted to spawn a drone for each player anyway)
        drone = GameObject.Find("Drone");

        // get these once + cache them instead of getting them 60x per second (performance!)
        movementScript = GetComponent<Movement>();
        lookingScript = Camera.main.GetComponent<LookingMain>();
        
        shootingScript = drone.GetComponent<Shooting>();
        flightScript = drone.GetComponent<flight>();
        rayViewerScript = drone.GetComponent<RayViewer>();


        // tell main camera to follow the player + listen to input
        lookingScript.enabled = true;
        lookingScript.Follow(transform, new Vector3(0f, 2.23f, -4f));   // offset

    }

    void Update()
    {
        // multiplayer: stops everyone from controlling everyone
        if (!hasAuthority) return;

        if (Input.GetKeyUp(KeyCode.H))
        {
            // disable player movement
            movementScript.enabled = !movementScript.enabled;

            // enable drone scripts (leave the drone visible)
            shootingScript.enabled = !shootingScript.enabled;
            flightScript.enabled = !flightScript.enabled;
            rayViewerScript.enabled = !rayViewerScript.enabled;

            // toggle between following the drone or the player
            droneActivated = !droneActivated;
            if (droneActivated)
            {
                lookingScript.Follow(drone.transform, Vector3.zero);  // camera follows drone
            } else
            {
                lookingScript.Follow(transform, new Vector3(0f, 2.23f, -4f));   // camera follows player
            }

            //GetComponent<flight>().enabled = !GetComponent<flight>().enabled;
            //drone.SetActive(!camstate);
            //Transform.GetChild<Cube>().enabled= !GetComponentInChildren<Cube>().enabled;
            //this.gameObject.transform.GetChild(2).enabled = !this.gameObject.transform.GetChild(2).enabled;

        }

    }































}