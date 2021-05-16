using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongTriggerScript : MonoBehaviour
{
	public GameObject SongUI;
    public Text TitleOfSong;
    public Text AuthorOfSong;
    public Text SongNumberText;
    int SongNumber = 0;
    float[] SongTime = {175f, 345f, 538f, 775f, 1023f, 1318f, 1582f, 1827f, 2233f, 2450f, 2766f};

    void Start()
    {
        SongUI.SetActive(false);
    }

    void Update()
    {
        TitleSongs();

        if(SongNumber > 11)
        {
            SongNumber = 0;
        }
        else if(SongNumber < 0)
        {
            SongNumber = 11;
        }

        SongNumberText.text = "Song " + (SongNumber + 1) + "/12";
    }

    //Turn UI on
    void OnTriggerEnter(Collider SongMenuEnter)
    {
    	if(SongMenuEnter.gameObject.tag == "Player")
    	{
    		SongUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Debug.Log("UI Active");
        }
    }

    //Turn UI off
    void OnTriggerExit(Collider SongMenuExit)
    {
        if(SongMenuExit.gameObject.tag == "Player")
        {
    	   SongUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Debug.Log("UI Deactive");
        }
    }

    //Press button to change song titles and authors
    public void NextSongNumber()
    {
        SongNumber += 1;
        
    }

    //Press button to change song titles and authors
    public void PrevSongNumber()
    {
        SongNumber -= 1;
    }

    void AutomaticTitleChange()
    {
        if(Time.time >= SongTime[SongNumber])
        {
            SongNumber += 1;
        }
    }

    //Display Song titles and Authors
    void TitleSongs()
    {
        if(SongNumber == 11)
        {
            TitleOfSong.text = "Final Boss";
            AuthorOfSong.text = "By Nitro Fun";
        }
        else if(SongNumber == 10)
        {
            TitleOfSong.text = "Annihilate";
            AuthorOfSong.text = "By Destroid";
        }
        else if(SongNumber == 9)
        {
            TitleOfSong.text = "Cheat Codes";
            AuthorOfSong.text = "By Nitro Fun";
        }
        else if(SongNumber == 8)
        {
            TitleOfSong.text = "Tanz Zu Der Musik";
            AuthorOfSong.text = "By QUAL & FREUDE";
        }
        else if(SongNumber == 7)
        {
            TitleOfSong.text = "Cool Friends (Murtagh & Veschell Remix)";
            AuthorOfSong.text = "By Silva Hound";
        }
        else if(SongNumber == 6)
        {
            TitleOfSong.text = "New Game";
            AuthorOfSong.text = "By Nitro Fun";
        }
        else if(SongNumber == 5)
        {
            TitleOfSong.text = "Termination Shock";
            AuthorOfSong.text = "By Sabrepulse";
        }
        else if(SongNumber == 4)
        {
            TitleOfSong.text = "Laundry Matter";
            AuthorOfSong.text = "By Pixl";
        }
        else if(SongNumber == 3)
        {
            TitleOfSong.text = "Renegade";
            AuthorOfSong.text = "By Shirobon";
        }
        else if(SongNumber == 2)
        {
            TitleOfSong.text = "CyberStrike ft. Sabrepulse";
            AuthorOfSong.text = "By Shirobon";
        }
        else if(SongNumber == 1)
        {
            TitleOfSong.text = "Into The Zone";
            AuthorOfSong.text = "By Shirobon";
        }
        else if(SongNumber == 0)
        {
            TitleOfSong.text = "Ralf's Touch";
            AuthorOfSong.text = "By Shirobon";
        }
    }
}
