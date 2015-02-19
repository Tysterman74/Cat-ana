using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour 
{
    private ParticleSystem smoke;
	private GameObject groundCheck;
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

    private SpriteRenderer renderPlayer;
    private bool hiding = false;

    void Start()
    {
        groundCheck = transform.FindChild("GroundCheck").gameObject;
        smoke = transform.FindChild("Smoke").GetComponent<ParticleSystem>();
        renderPlayer = GetComponent<SpriteRenderer>();
    }

	void FixedUpdate () 
	{
        if (hiding == false)
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
            if (isGround)
            {
                if (Input.GetKeyDown(up))
                {
                    rigidbody2D.AddForce(new Vector2(0.0f, jumpSpeed));
                }
            }
        }

	}

    void Update()
    {
        if (!hiding)
        {
            renderPlayer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            renderPlayer.color = new Color(0f, 0f, 0f, 1f);
        }
    }

    void setHiding()
    {
		if (hiding == false) 
		{
			//rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            transform.Translate(new Vector2(speed, 0.0f));
			hiding = true;
            smoke.Play();
		}
		else if (hiding == true)
		{
			//rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
            transform.Translate(new Vector2(-speed, 0.0f));
			hiding = false;
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
