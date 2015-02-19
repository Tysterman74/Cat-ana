using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour 
{	
	public GameObject groundCheck;
	public LayerMask whatIsGround;

	// Key inputs
	public KeyCode right;
	public KeyCode left;
	public KeyCode jump;

	// Player properties
	public float speed = 5.0f;


	// Player jumping properties
	public float jumpSpeed = 600.0f;
	private float groundHeight; 


	void FixedUpdate () 
	{
		bool isGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.03f, whatIsGround);

		// Right arrow key pressed?
		if (Input.GetKey(right)) 
		{
			//rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            transform.Translate(new Vector2(speed, 0.0f));
		}
		
		// Down arrow key pressed?
		else if (Input.GetKey(left)) 
		{
			//rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
            transform.Translate(new Vector2(-speed, 0.0f));
		}

		else
		{
			rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
		}


		// Up arrow key (jump) pressed once?
		if(isGround)
		{
			if (Input.GetKey(jump)) 
			{
				rigidbody2D.AddForce(new Vector2(0.0f, jumpSpeed));
			}
		}
	}
}
