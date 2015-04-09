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

	// Key inputs
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
    public float attackLength = 0.625f;
    public float attackTime = 0.0f;


	// Player jumping properties
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
        //if nur(hiding == false)
        //{
        isGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.03f, whatIsGround);

        // Right arrow key pressed?
        Vector2 test = new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
        if (Input.GetKey(right))
        {
            test = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetFloat("speed", 1);
            if (hiding)
                setHiding();
            facingRight = true;
        }

        // Left arrow key pressed?
        else if (Input.GetKey(left))
        {
            print("test");
            test = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
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

        test += externalVelocity;
        //print("Move Player: " + GetComponent<Rigidbody2D>().velocity);
        print("External Velocity: " + externalVelocity);
        GetComponent<Rigidbody2D>().velocity = test;
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
        if (!hiding)
        {
            renderPlayer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            renderPlayer.color = new Color(1f, 1f, 1f, 0.5f);
        }

        // Up arrow key (jump) pressed once?
        if (isGround)
        {
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
