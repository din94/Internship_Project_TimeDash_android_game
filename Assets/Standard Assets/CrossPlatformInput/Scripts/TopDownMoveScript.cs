using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TopDownMoveScript : MonoBehaviour {


	//Variable to hold Input directions in X and Y axis

	float dirX, dirY;

	//Move speed variable can be set in Inspector with slider
	[Range(1f, 20f)]
	public float moveSpeed = 5f, jumpForce = 11f;


	//Reference to Rigidbody 2D component
	Rigidbody2D rb;



	//use this for initialization

	void Start(){

		//Getting Rigidbody2D component to operate it
		rb = GetComponent<Rigidbody2D>();



	}






	//Update is called once per frame

	void Update(){


		//Getting move direction according to button pressed
		// multiplied by move speed and time passed

		dirX = CrossPlatformInputManager.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		dirY = CrossPlatformInputManager.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;


		// setting new transform position of game object
		transform.position = new Vector2(transform.position.x + dirX, transform.position.y + dirY);



		// If Jump Button is pressed then DoJump method is inovked

		if (CrossPlatformInputManager.GetButtonDown ("Jump"))
			DoJump ();

		
	}


	void FixedUpdate()
	{

		// Pass a velocuty to Rigidbody2D component according to dirX and dirY value multiplied by move speed
		rb.velocity = new Vector2 ( dirX * moveSpeed, rb.velocity.y);



	}


	//Public jump method which is invoked when jump UI button is pressed

	public void DoJump()
	{

		//simple check to not allow the cat to jump while in the air

		if (rb.velocity.y == 0)

			// add force to rigidbody 2d component in Y direction

			rb.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Force);



	}





}
