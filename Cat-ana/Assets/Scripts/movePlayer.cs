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

    Animator anim;

    public KeyCode attack;
    public bool attackClicked = false;
    float attackLength = 0.625f;
    float attackTime = 0.0f;


	// Player jumping properties
	public float jumpSpeed = 600.0f;
	private float groundHeight;

    private SpriteRenderer renderPlayer;
    private bool hiding = false;

    void Start()
    {
        
        anim = GetComponent<Animator>();
        groundCheck = transform.FindChild("GroundCheck").gameObject;
        smoke = transform.FindChild("Smoke").GetComponent<ParticleSystem>();
        renderPlayer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //if (hiding == false)
        //{
            bool isGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.03f, whatIsGround);

            // Right arrow key pressed?
            if (Input.GetKey(right))
            {
                rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
                transform.eulerAngles = new Vector2(0, 0);
                anim.SetFloat("speed", speed);
                if (hiding)
                    setHiding();
            }

            // Left arrow key pressed?
            else if (Input.GetKey(left))
            {
                rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
                transform.eulerAngles = new Vector2(0, 180);
                anim.SetFloat("speed", speed);
                if (hiding)
                    setHiding();
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
                anim.SetFloat("speed", 0.0f);
            }


            // Up arrow key (jump) pressed once?
            if (isGround)
            {
                if (Input.GetKey(jump))
                {
                    rigidbody2D.AddForce(new Vector2(0.0f, jumpSpeed));
                    if (hiding)
                        setHiding();
                }
            }

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
            // Attack key (mouse) pressed once?
            if (Input.GetKey(attack))
            {
                attackClicked = true;
                anim.SetBool("attackClicked", attackClicked);   
            
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
            rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
            gameObject.tag = "Hidden";
            rigidbody2D.gravityScale = 0.0f;
            collider2D.isTrigger = true;
		}
		else if (hiding == true)
		{
			hiding = false;
            gameObject.tag = "Player";
            rigidbody2D.gravityScale = 1.5f;
            collider2D.isTrigger = false;
		}
	}
}
