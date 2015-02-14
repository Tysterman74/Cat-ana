using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour 
{	
	private GameObject groundCheck;
	public LayerMask whatIsGround;

	// Key inputs
	public KeyCode right;
	public KeyCode left;
	public KeyCode up;

	// Player properties
	public float speed = 5.0f;


	// Player jumping properties
	public float jumpSpeed = 600.0f;
	private float groundHeight;

    private bool hiding = false;

    void Start()
    {
        groundCheck = transform.FindChild("GroundCheck").gameObject;
    }

	void FixedUpdate () 
	{
		bool isGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.03f, whatIsGround);

		// Right arrow key pressed?
		if (Input.GetKey(right)) 
		{
			rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
		}
		
		// Down arrow key pressed?
		else if (Input.GetKey(left)) 
		{
			rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
		}

		else
		{
			rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
		}


		// Up arrow key (jump) pressed once?
		if(isGround)
		{
			if (Input.GetKeyDown(up)) 
			{
				rigidbody2D.AddForce(new Vector2(0.0f, jumpSpeed));
			}
		}
	}

    void setHiding(bool hide)
    {

        //Player cannot move when hiding
        //When the player presses F again, movement is given back and the player is shown.
        print("MYAAAAH: " + hide);
    }
}
