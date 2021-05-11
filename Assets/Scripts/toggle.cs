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

            // enable drone scripts (keep the drone visible at all times?)
            shootingScript.enabled = !shootingScript.enabled;
            flightScript.enabled = !flightScript.enabled;
            rayViewerScript.enabled = !rayViewerScript.enabled;

            // toggle between following the drone or the player
            droneActivated = !droneActivated;
            if (droneActivated)
            {
                lookingScript.Follow(drone.transform, Vector3.zero);  // camera follows drone, no offset
            } else
            {
                lookingScript.Follow(transform, new Vector3(0f, 2.23f, -4f));   // camera follows player, slightly above & behind
            }

        }

    }































}