using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class toggle : MonoBehaviour
{
    public bool moving = true;



    void Start()
    {
    
    }
   







    void Update()
    {



        if (Input.GetKeyUp(KeyCode.F) || moving == true)
        {
            moving = false;
            GetComponent<Movement>().enabled = false;

        }

        if (Input.GetKeyUp(KeyCode.F) || moving == false)
        {
            GetComponent<Movement>().enabled = true;
        }
    }































}