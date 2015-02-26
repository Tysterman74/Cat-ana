using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour 
{
    private ParticleSystem smoke;
	private GameObject groundCheck;
	public LayerMask whatIsGround;
    private int charge;
    public int maxcharge;
    private GameObject yarnball;

	// Key inputs
	public KeyCode right;
	public KeyCode left;
	public KeyCode up;
    public KeyCode yarn;

	// Player properties
	public float speed = 5.0f;


	// Player jumping properties
	public float jumpSpeed = 600.0f;
	private float groundHeight;

    private SpriteRenderer renderPlayer;
    private bool hiding = false;

    IEnumerator resetCollider()
    {
        yield return new WaitForSeconds(1.0f);
        rigidbody2D.gravityScale = 1.5f;
        collider2D.isTrigger = false;
    }

    void Start()
    {
        groundCheck = transform.FindChild("GroundCheck").gameObject;
        smoke = transform.FindChild("Smoke").GetComponent<ParticleSystem>();
        renderPlayer = GetComponent<SpriteRenderer>();
        charge = 0;
        yarnball = transform.FindChild("YarnBall").gameObject;
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
                if (Input.GetKey(yarn))
                {
                    print("charge");
         
                    charge += 1;
                    if (charge > 100)
                    {
                        charge = 100;
                    
                    }
                
                }
                if (Input.GetKeyUp(yarn))
                {
                    print("thrown");
                    print(charge);
                    rigidbody2D.gravityScale = 0.0f;
                    collider2D.isTrigger = true;
                    yarnball.transform.parent = null;
                    yarnball.rigidbody2D.AddForce(new Vector2(charge*10,0));
                    StartCoroutine(resetCollider());
                
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
			hiding = true;
            smoke.Play();
		}
		else if (hiding == true)
		{
			hiding = false;
		}

        //Player cannot move when hiding
        //When the player presses F again, movement is given back and the player is shown.
        //print("MYAAAAH: " + hide);
    }
}
