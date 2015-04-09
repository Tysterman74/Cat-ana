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
    private bool yarnballThrown = false;

	// Key inputs for player actions
    // Used for different player actions
	public KeyCode right;
	public KeyCode left;
	public KeyCode jump;
    public KeyCode yarn;

	// Player properties
	public float speed = 5.0f;
    public float maxSpeed = 7.5f;

    Animator anim;

    public KeyCode attack;
    public bool attackClicked = false;
    float attackLength = 0.625f;
    float attackTime = 0.0f;


	// Player jumping properties
    //Possibly change this to velocity.
	public float jumpSpeed = 600.0f;
	private float groundHeight;

    private Vector2 externalVelocity; 
    private SpriteRenderer renderPlayer;
    private bool hiding = false;
    private bool isGround = true;
    private bool facingRight = true;

    IEnumerator resetCollider()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        GetComponent<Collider2D>().isTrigger = false;
    }

    void Start()
    {
        
        anim = GetComponent<Animator>();
        groundCheck = transform.FindChild("GroundCheck").gameObject;
        smoke = transform.FindChild("Smoke").GetComponent<ParticleSystem>();
        renderPlayer = GetComponent<SpriteRenderer>();
        charge = 0;
        yarnball = transform.FindChild("YarnBall").gameObject;
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.03f, whatIsGround);

        // Right arrow key pressed?

        // TODO:
        // Y velocity, define a maxHeight for how far the player jumps as well as reset y velocity to 0 when you're ground.

        //Placeholder for calculating external forces.
        Vector2 totalVelocity = new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);

        //Used Get Key to read in the input.
        if (Input.GetKey(right))
        {
            //Set the velocity for what the PLAYER itself has.
            totalVelocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetFloat("speed", 1);
            if (hiding)
                setHiding();
            facingRight = true;
        }

        // Left arrow key pressed?
        else if (Input.GetKey(left))
        {
            print("totalVelocity");
            //Set the velocity for what the PLAYER itself has.
            totalVelocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            transform.eulerAngles = new Vector2(0, 180);
            anim.SetFloat("speed", 1);
            if (hiding)
                setHiding();
            facingRight = false;
        }
        else
        {
            //rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
            anim.SetFloat("speed", -1.0f);
        }

        //Add in the external velocity that other forces are acting on the player.
        totalVelocity += externalVelocity;
        //print("Move Player: " + GetComponent<Rigidbody2D>().velocity);
        //print("External Velocity: " + externalVelocity);

        GetComponent<Rigidbody2D>().velocity = totalVelocity;
        //if (Mathf.Abs(rigidbody2D.velocity.x) < maxSpeed)
        //    rigidbody2D.velocity += externalVelocity;
        //else
        //    rigidbody2D.velocity = new Vector2(facingRight ? maxSpeed : -maxSpeed, rigidbody2D.velocity.y);
            

        if (attackClicked)
        {
            attackTime += Time.deltaTime;

            if (attackTime >= attackLength)
            {
                attackClicked = false;
                anim.SetBool("attackClicked", attackClicked);
                attackTime = 0.0f;
            }
        }

        if (isGround)
        {
            if (Input.GetKey(jump))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpSpeed));
                if (hiding)
                    setHiding();

            }
        }

        // Attack key (mouse) pressed once?
        if (Input.GetKey(attack))
        {
            attackClicked = true;
            anim.SetBool("attackClicked", attackClicked);   
            
        }
    }

    public void setExternalVelocity(Vector2 vel)
    {
        externalVelocity = vel;
    }

    void Update()
    {
        //Makes it transparent when hiding.
        if (!hiding)
        {
            renderPlayer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            renderPlayer.color = new Color(1f, 1f, 1f, 0.5f);
        }

        //When grounded, If holding the button for yarn then release.
        if (isGround)
        {
            //TODO:
            //Add particle effects and asethics to charge
            //Used for charging the yarn
            if (Input.GetKey(yarn))
            {
                print("charge");

                charge += 1;
                if (charge > 100)
                {
                    charge = 100;
                }

            }
            //When release yarn button
            if (Input.GetKeyUp(yarn))
            {
                yarnball.SendMessage("setDirection", facingRight);
                print("thrown");
                print(charge);
                GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                GetComponent<Collider2D>().isTrigger = true;
                yarnball.SendMessage("launchYarnball", charge);
                StartCoroutine(resetCollider());
                charge = 0;
            }
        }
    }

    void setHiding()
    {
		if (hiding == false) 
		{
			hiding = true;
            smoke.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
            gameObject.tag = "Hidden";
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<Collider2D>().isTrigger = true;
		}
		else if (hiding == true)
		{
			hiding = false;
            gameObject.tag = "Player";
            GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            GetComponent<Collider2D>().isTrigger = false;
		}
	}

    void PickupYarn()
    {
        yarnball.transform.position = transform.position;
        yarnball.transform.parent = transform;

    }

    public bool isRight()
    {
        return facingRight;
    }
}
