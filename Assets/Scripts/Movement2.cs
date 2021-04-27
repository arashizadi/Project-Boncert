using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
	CharacterController PlayerController;
    Animator Animator;

	// Movement
	Vector3 PlayerMovement;
    float PlayerX;
    float PlayerZ;
    [SerializeField]
    public float MovementSpeed = 5f;    // multipler

    // Gravity
    [SerializeField]
    private float Gravity = 4f;
    private float PlayerDirection_Y;

    // Jump
    [SerializeField]
    private float JumpSpeed = 3.5f;


    void Start()
    {
        PlayerController = GetComponent<CharacterController>();
        Animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        Dance();


        // Drone POV
        if (Input.GetKeyUp(KeyCode.Q))
        {
            this.GetComponent<DroneMovement>().enabled = false;
        }

    }


    void Move()
    {
        // Calculate WASD movement
        PlayerX = Input.GetAxis("Horizontal");
        PlayerZ = Input.GetAxis("Vertical");
        PlayerMovement = transform.right * PlayerX + transform.forward * PlayerZ;
        Animator.SetFloat("ForwardSpeed", PlayerZ);     // for the walk/idle animations

        // Press Space to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDirection_Y = JumpSpeed;
        }

        // Add gravity to Y-axis movement
        PlayerDirection_Y -= Gravity * Time.deltaTime;
        PlayerMovement.y = PlayerDirection_Y;

        // Finally, move the player
        PlayerController.Move(PlayerMovement * MovementSpeed * Time.deltaTime);
        //Debugging();

    }

    void Dance()
    {
        // Press 1, 2 or 3 to dance
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Animator.SetTrigger("Dance1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Animator.SetTrigger("Dance2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Animator.SetTrigger("Dance3");
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
