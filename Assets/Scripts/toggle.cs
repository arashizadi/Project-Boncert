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

    LookingMain cameraInputScript;
    Movement movementScript;


    // moved my Start() stuff into here to stop other clients from running it.
    // note: Start() runs before network stuff is done initializing, so i can't use hasAuthority in there.
    public override void OnStartAuthority()
    {
        // get these once + cache them instead of getting them 60x per second (performance!)
        movementScript = GetComponent<Movement>();
        cameraInputScript = Camera.main.GetComponent<LookingMain>();

        // tell main camera to start following the player + listening to input
        cameraInputScript.enabled = true;
        cameraInputScript.Follow(transform, new Vector3(0f, 2.23f, -4f));   // offset
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
                cameraInputScript.Follow(drone.transform, Vector3.zero);  // camera follows drone, no offset
            } else
            {
                cameraInputScript.Follow(transform, new Vector3(0f, 2.23f, -4f));   // camera follows player, slightly above & behind
            }

        }

    }

    public void AssignDroneToPlayer(GameObject _drone)
    {
        drone = _drone;

        shootingScript = drone.GetComponent<Shooting>();
        flightScript = drone.GetComponent<flight>();
        rayViewerScript = drone.GetComponent<RayViewer>();
    }































}