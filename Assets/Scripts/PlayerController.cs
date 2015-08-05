using UnityEngine;
using System.Collections;
using System;

#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;
#endif

public class PlayerController : MonoBehaviour {

	// PUBLIC VARIABLES //
	public float baseSpeed;
	public float baseJump;
	public float jumpAdjust;
	public float slow; 			// Slows the player's movement when not grounded
	public float coins;
	public float multiplier;
	public float elevatorSpeed;	
	public float boostSpeed;
	
	public bool moveLeft;
	public bool moveRight;
	public bool jump;
	
	//public float lastPlatform;
	//public Vector2 lastPlatformVector;
	//public float tempPlayerPos;
	//public float distanceToLastPlat = 10;
	
	// PRIVATE VARIABLES //
	private float currentSpeed;
	private float currentJump;
	private float maxSpeed;
	public bool grounded = false;

	void Awake () 
	{
		Application.targetFrameRate = 60;
	}
	
	void Start ()
	{	
		
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		// Detect if player is touching the ground, allow player to jump
		if (coll.gameObject.tag == "Ground" && coll.gameObject.transform.position.y < gameObject.transform.position.y)
		{
			grounded = true;
			
			//lastPlatform = Math.Abs(coll.contacts[0].point.y);
			//tempPlayerPos = Math.Abs(gameObject.transform.position.y);
			
			//distanceToLastPlat = Math.Abs(tempPlayerPos - lastPlatform);
			
		}
	}
	
	void AdjustMultiplier()
	{
		if (coins < 10)
		{
			multiplier = 1 + (coins/10);
		}
	}
	
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground")
		{
			grounded = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		// Right Booster
		if (coll.gameObject.tag == "RightBoost")
		{
			GetComponent<Rigidbody2D>().AddForce(boostSpeed * Vector2.right);	
		}	
		
		// Coins
		if (coll.gameObject.tag == "Coin")
		{
			Destroy(coll.gameObject);
			coins++;
			AdjustMultiplier();
		}
	}
	
	void OnTriggerStay2D(Collider2D coll)
	{
		// Up Elevator
		if (coll.gameObject.tag == "Elevator")
		{
			GetComponent<Rigidbody2D>().AddForce(elevatorSpeed * Vector2.up);	
        }	
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	
	#if UNITY_EDITOR
	
		if (Input.GetKey(KeyCode.RightArrow))
		{
			moveRight = true;
		}
		
		else 
		{
			moveRight = false;
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			moveLeft = true;
		}
		
		else 
		{
			moveLeft = false;
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			jump = true;
		}
		
		else
		{
			jump = false;
		}
		
		#endif
		
		#if UNITY_ANDROID && !UNITY_EDITOR
		
		float axisP1LsX = OuyaSDK.OuyaInput.GetAxis(0, OuyaController.AXIS_LS_X);
		
		if (OuyaSDK.OuyaInput.GetButton(0, OuyaController.BUTTON_DPAD_RIGHT) || (axisP1LsX > .4))
		{
			moveRight = true;
		}
		
		else 
		{
			moveRight = false;
		}
		
		if (OuyaSDK.OuyaInput.GetButton(0, OuyaController.BUTTON_DPAD_LEFT) || (axisP1LsX < -.4))
		{
			moveLeft = true;
		}
		
		else 
		{
			moveLeft = false;
		}
		
		if (OuyaSDK.OuyaInput.GetButton(0, OuyaController.BUTTON_O))
		{
			jump = true;
		}
		
		else 
		{
			jump = false;
		}
		
		#endif
	
		// Move Right
		if (moveRight)
		{	
			if (grounded == true)
			{
				GetComponent<Rigidbody2D>().AddTorque(-baseSpeed * multiplier * Time.deltaTime);
				GetComponent<Rigidbody2D>().AddForce(jumpAdjust * multiplier * Vector2.right);
			}
			
			else
			{
				GetComponent<Rigidbody2D>().AddTorque((-baseSpeed * multiplier * slow) * Time.deltaTime);
				GetComponent<Rigidbody2D>().AddForce(jumpAdjust * multiplier * Vector2.right);
			}
		}
		
		// Move Left
		else if (moveLeft)
		{
			if (grounded == true)
			{
				GetComponent<Rigidbody2D>().AddTorque(baseSpeed * multiplier * Time.deltaTime);
				GetComponent<Rigidbody2D>().AddForce(-jumpAdjust * multiplier * Vector2.right);
			}
			
			else
			{
				GetComponent<Rigidbody2D>().AddTorque((baseSpeed * multiplier * slow) * Time.deltaTime);
				GetComponent<Rigidbody2D>().AddForce(-jumpAdjust * multiplier * Vector2.right);
			}
		}
		
		// Jump
		if (jump && grounded)
		{
			GetComponent<Rigidbody2D>().AddForce(baseJump * Vector2.up * Time.deltaTime, ForceMode2D.Force);
			grounded = false;
		}
		
	}
}
