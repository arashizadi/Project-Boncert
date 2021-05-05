using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Movement : NetworkBehaviour
{
	CharacterController PlayerController;
    Animator Animator;
    NetworkAnimator networkAnimator;

    // Movement
    Vector3 PlayerMovement;
    float PlayerX;
    float PlayerZ;
    [SerializeField]
    public float MovementSpeed = 5f;     // Multipler

    // Gravity
    Vector3 GravityVector;
    [SerializeField]
    private float Gravity = 7f;

    // Jump
    [SerializeField]
    private float JumpSpeed = 1.4f;
    private float PlayerDirection_Y;


    void Start()
    {
        PlayerController = GetComponent<CharacterController>();
        Animator = GetComponentInChildren<Animator>();
        networkAnimator = GetComponent<NetworkAnimator>();
    }

    void Update()
    {
        // multiplayer: stops everyone from controlling everyone
        if (!hasAuthority) return;

        Move();
        Dance();
        //Debugging();

    }

    void Move()
    {
        // WASD movement
        PlayerX = Input.GetAxis("Horizontal");
        PlayerZ = Input.GetAxis("Vertical");
        PlayerMovement = transform.right * PlayerX + transform.forward * PlayerZ;

        // Walk + idle animations based on movement
        Animator.SetFloat("ForwardSpeed", PlayerZ);     

        // Press Space to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDirection_Y = JumpSpeed;
            networkAnimator.SetTrigger("Jump");    // Jump + fist pump animation
        }

        // Add gravity to Y-axis movement
        PlayerDirection_Y -= Gravity * Time.deltaTime;
        PlayerMovement.y = PlayerDirection_Y;

        // Finally, move the player
        PlayerController.Move(PlayerMovement * MovementSpeed * Time.deltaTime);

    }

    void Dance()
    {
        // Press 1, 2 or 3 to dance
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            networkAnimator.SetTrigger("Dance1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            networkAnimator.SetTrigger("Dance2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            networkAnimator.SetTrigger("Dance3");
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
