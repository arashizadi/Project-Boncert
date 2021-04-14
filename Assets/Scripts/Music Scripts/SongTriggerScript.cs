using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongTriggerScript : MonoBehaviour
{
	public GameObject SongUI;

    void Start()
    {
        SongUI.SetActive(false);
    }

    /*void Update()
    {

    }*/

    void OnTriggerEnter(Collider SongMenuEnter)
    {
    	if(SongMenuEnter.gameObject.tag == "Player")
    	{
    		SongUI.SetActive(true);
            Debug.Log("UI Active");
    	}
    }

    void OnTriggerExit(Collider SongMenuExit)
    {
        if(SongMenuExit.gameObject.tag == "Player")
        {
    	   SongUI.SetActive(false);
           //Debug.Log("UI Deactive");
        }
    }
}
