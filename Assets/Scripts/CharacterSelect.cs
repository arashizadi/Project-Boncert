using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// credits to Dapper Dino's "How to Make a Multiplayer Game in Unity - Character Selection" vid on YouTube!

public class CharacterSelect : NetworkBehaviour
{
    [SerializeField] GameObject characterSelectDisplay;     // enable/disable to show/hide the character select screen
    [SerializeField] Transform characterPreviewParent;      // where to put the character preview
    [SerializeField] Character[] characters;

    int currentCharacterIndex = 0;
    List<GameObject> characterInstances = new List<GameObject>();

    float turnSpeed = 32f;


    [SerializeField] GameObject dronePrefab;
    [SerializeField] Transform[] zones;

    public override void OnStartClient()
    {
        if (characterInstances.Count == 0)     // making sure the characters are only instantiated once
        {
            foreach (var character in characters)
            {
                GameObject characterInstance = Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);
                characterInstance.SetActive(false);
                characterInstances.Add(characterInstance);
            }
        }

        // only display the character that's currently selected
        characterInstances[currentCharacterIndex].SetActive(true);

        // reveal the UI!
        characterSelectDisplay.SetActive(true);
    }


    void Update()
    {
        characterPreviewParent.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
    }


    public void Select()
    {
        CmdSelect(currentCharacterIndex);

        //newPlayer.GetComponent<toggle>().AssignDroneToPlayer(newDrone);     // bc newPlayer and newDrone were initialized in a command, their values live on the server only. they are null on the client. that's why this didn't work

        // hide UI
        characterSelectDisplay.SetActive(false);
    }

    [Command(requiresAuthority = false)]
    public void CmdSelect(int characterIndex, NetworkConnectionToClient conn = null)
    {
        // spawn a drone
        // (client gets authority over this object, but it is not the ~player object~ fyi)
        GameObject newDrone = Instantiate(dronePrefab, zones[Random.Range(0, zones.Length)].position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), Quaternion.identity);
        NetworkServer.Spawn(newDrone, conn);

        // spawn the character we selected
        GameObject placeholder = conn.identity.gameObject;  // blank gameobject with a networkidentity
        GameObject newPlayer = Instantiate(characters[characterIndex].GameplayCharacterPrefab, placeholder.transform.position, Quaternion.identity);

        // set the character as our player
        NetworkServer.ReplacePlayerForConnection(conn, newPlayer);
        NetworkServer.Destroy(placeholder);  // no longer needed

        // lastly, assign the drone to our player

        //newPlayer.GetComponent<toggle>().AssignDroneToPlayer(newDrone);     //****NOT HAPPENING FOR SOME REASON?
                                                                              // on the host (a.k.a. client 1), everyone has their drones properly assigned.
                                                                              // client 2, who literally CALLED THE COMMAND, has no idea which drone they own and never runs Follow().
                                                                              // ...maybe move this line out of the command?

                                                                            // ^^^ yuuuup. this entire function ONLY runs on the server build, even though it was called here on the client.
                                                                            // use a targetRPC function to run stuff on the client that called this command
        NetworkIdentity playerIdentity = newPlayer.GetComponent<NetworkIdentity>();
        TargetAssignDroneToPlayer(playerIdentity.connectionToClient, newPlayer, newDrone);

    }

    [TargetRpc]
    public void TargetAssignDroneToPlayer(NetworkConnection target, GameObject _newPlayer, GameObject _newDrone)
    {
        _newPlayer.GetComponent<toggle>().AssignDroneToPlayer(_newDrone);
    }


    public void Right()
    {
        characterInstances[currentCharacterIndex].SetActive(false);
        currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;  // modulo prevents index from going out of bounds
        characterInstances[currentCharacterIndex].SetActive(true);
    }

    public void Left()
    {
        characterInstances[currentCharacterIndex].SetActive(false);
        currentCharacterIndex--;

        // prevents index from going out of bounds
        if (currentCharacterIndex < 0) { currentCharacterIndex += characterInstances.Count; }

        characterInstances[currentCharacterIndex].SetActive(true);
    }
}
