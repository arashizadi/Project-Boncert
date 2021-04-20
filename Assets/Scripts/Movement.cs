using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	CharacterController PlayerController;

	//Movement
	Vector3 PlayerMovement;
    float PlayerX;
    float PlayerZ;
    //Multipler
    public float MovementSpeed = 5;

    //Gravity
    Vector3 GravityVector;
    public float Gravity = -9.81f;


    

    void Start()
    {
        PlayerController = GetComponent<CharacterController>();
    }

    void Update()
    {
    	//Movement positions
        PlayerX = Input.GetAxis("Horizontal");
        PlayerZ = Input.GetAxis("Vertical");
        PlayerMovement = transform.right * PlayerX + transform.forward * PlayerZ;

        //Gravity Position
        GravityVector = new Vector3(0, Gravity, 0);

        //To See Code Works
        Debugging();

        //Movement
        PlayerController.Move(PlayerMovement * MovementSpeed * Time.deltaTime);
        //Gravity
        PlayerController.Move(GravityVector * Time.deltaTime);



        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.GetComponent<DroneMovement>().enabled = false;
        }









    }

	//Debugs
	void Debugging()
	{
		//Touching Ground
        /*if(PlayerController.isGrounded)
        {
        	Debug.Log("Grounded");
        }*/

        //Console Writes
    	if(Input.GetKey(KeyCode.W))
    	{
    		Debug.Log("Forward");
    	}
    	else if(Input.GetKey(KeyCode.S))
    	{
    		Debug.Log("Backwards");
    	}
    	else if(Input.GetKey(KeyCode.D))
    	{
    		Debug.Log("Right");
    	}
        else if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("Left");
        }
	}
}
