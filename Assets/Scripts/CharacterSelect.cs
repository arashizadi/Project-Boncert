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
        // spawn the character we selected and set it as our player
        CmdSelect(currentCharacterIndex);

        // hide UI
        characterSelectDisplay.SetActive(false);
    }

    [Command(requiresAuthority = false)]
    public void CmdSelect(int characterIndex, NetworkConnectionToClient conn = null)
    {
        // spawn the character we selected and set it as our player.
        // (up until this point, the client was represented by a blank gameobject with a networkidentity)
        GameObject placeholder = conn.identity.gameObject;
        GameObject newPlayer = Instantiate(characters[characterIndex].GameplayCharacterPrefab, placeholder.transform.position, Quaternion.identity);
        NetworkServer.ReplacePlayerForConnection(conn, newPlayer);
        NetworkServer.Destroy(placeholder);

        // spawn 1 drone per player too! (client gets authority over this object, but it is not the ~player object~ fyi)
        GameObject newDrone = Instantiate(dronePrefab, zones[Random.Range(0, zones.Length)].position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), Quaternion.identity);
        NetworkServer.Spawn(newDrone, conn);
        newPlayer.GetComponent<toggle>().drone = newDrone;  // toggle script needs this ref in order to take control

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
