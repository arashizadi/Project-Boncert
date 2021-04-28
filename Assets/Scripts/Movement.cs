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
    [SerializeField]
    public float MovementSpeed = 5f;

    //Gravity
    Vector3 GravityVector;
    [SerializeField]
    private float Gravity = 1f;

    // Jump
    [SerializeField]
    private float JumpSpeed = 3.5f;

    private float PlayerDirection_Y;

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerDirection_Y = JumpSpeed;
            
        }

        PlayerDirection_Y -= Gravity * Time.deltaTime;

        PlayerMovement.y = PlayerDirection_Y;

        //Movement
        PlayerController.Move(PlayerMovement * MovementSpeed * Time.deltaTime);
        //Gravity
        PlayerController.Move(GravityVector * Time.deltaTime);



       








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
