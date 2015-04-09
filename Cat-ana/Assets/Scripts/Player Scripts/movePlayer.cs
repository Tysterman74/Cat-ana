using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour 
{
//    private ParticleSystem smoke;
//    private int charge;
//    public int maxcharge;

//    private GameObject yarnball;
//    private bool yarnballThrown = false;

	// Key inputs for player actions
    // Used for different player actions
	public KeyCode right;
	public KeyCode left;
	public KeyCode jump;
//    public KeyCode yarn;

	// Player properties
	public float speed = 5.0f;
    public float maxSpeed = 7.5f;
    public float jumpSpeed = 20.0f;

    Animator anim;
 //   private SpriteRenderer renderPlayer;

    groundBehavior groundBehavior;
//    yarnBallBehavior yarnBallBehavior;

    private Vector2 externalVelocity; 
    
//    private bool hiding = false;
 //   private bool facingRight = true;

    void Start()
    {
        // Initializes groundBehavior object used for checking if the player is on the ground
        // with the playerOnGround() method that returns a bool
        groundBehavior = FindObjectOfType(typeof(groundBehavior)) as groundBehavior;

 //       yarnBallBehavior = FindObjectOfType(typeof(yarnBallBehavior)) as yarnBallBehavior;

        // Initializes animator object used for determining what animations to play based on
        // player movement
        anim = GetComponent<Animator>();

//        smoke = transform.FindChild("Smoke").GetComponent<ParticleSystem>();
//        renderPlayer = GetComponent<SpriteRenderer>();
//        charge = 0;
//        yarnball = transform.FindChild("YarnBall").gameObject;
    }

    void FixedUpdate()
    {      
        horizontalMovement();
        verticalMovement();  
    }

    public void horizontalMovement()
    {
        Vector2 totalVelocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

        //Used Get Key to read in the input.
        if (Input.GetKey(right))
        {
            //Set the velocity for what the PLAYER itself has.
            totalVelocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            transform.eulerAngles = new Vector2(0, 0); // Flips player direction to right

            anim.SetFloat("speed", 1);

//            if (hiding)
//                setHiding();
//            facingRight = true;
        }

        // Left arrow key pressed?
        else if (Input.GetKey(left))
        {
            //Set the velocity for what the PLAYER itself has.
            totalVelocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            transform.eulerAngles = new Vector2(0, 180); // Flips player direction to left

            anim.SetFloat("speed", 1);

//            if (hiding)
//                setHiding();
//            facingRight = false;
        }
        else
        {
            anim.SetFloat("speed", -1);
        }
        //Add in the external velocity that other forces are acting on the player.
        totalVelocity += externalVelocity;

        GetComponent<Rigidbody2D>().velocity = totalVelocity;
    }

    public void verticalMovement()
    {
        Vector2 totalVelocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);

        if (groundBehavior.playerOnGround())
        {
            if (Input.GetKey(jump))
            {
                totalVelocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);

//                if (hiding)
//                    setHiding();
            }

            GetComponent<Rigidbody2D>().velocity = totalVelocity;
        }

        //Add in the external velocity that other forces are acting on the player.
        totalVelocity += externalVelocity;

        GetComponent<Rigidbody2D>().velocity = totalVelocity;
    }

    //IEnumerator resetCollider()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    //    GetComponent<Collider2D>().isTrigger = false;
    //}

    public void setExternalVelocity(Vector2 vel)
    {
        externalVelocity = vel;
    }

    //void Update()
    //{
    //    //Makes it transparent when hiding.
    //    if (!hiding)
    //    {
    //        renderPlayer.color = new Color(1f, 1f, 1f, 1f);
    //    }
    //    else
    //    {
    //        renderPlayer.color = new Color(1f, 1f, 1f, 0.5f);
    //    }

    //    //When grounded, If holding the button for yarn then release.
    //    if (groundBehavior.playerOnGround())
    //    {
    //        //TODO:
    //        //Add particle effects and asethics to charge
    //        //Used for charging the yarn
    //        if (Input.GetKey(yarn))
    //        {
    //            print("charge");

    //            charge += 1;
    //            if (charge > 100)
    //            {
    //                charge = 100;
    //            }

    //        }
    //        //When release yarn button
    //        if (Input.GetKeyUp(yarn))
    //        {
    //            yarnball.SendMessage("setDirection", facingRight);
    //            print("thrown");
    //            print(charge);
    //            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    //            GetComponent<Collider2D>().isTrigger = true;
    //            yarnball.SendMessage("launchYarnball", charge);
    //            StartCoroutine(resetCollider());
    //            charge = 0;
    //        }
    //    }
    //}

    //void setHiding()
    //{
    //    if (hiding == false) 
    //    {
    //        hiding = true;
    //        smoke.Play();
    //        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
    //        gameObject.tag = "Hidden";
    //        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    //        GetComponent<Collider2D>().isTrigger = true;
    //    }
    //    else if (hiding == true)
    //    {
    //        hiding = false;
    //        gameObject.tag = "Player";
    //        GetComponent<Rigidbody2D>().gravityScale = 1.5f;
    //        GetComponent<Collider2D>().isTrigger = false;
    //    }
    //}

    //void PickupYarn()
    //{
    //    yarnball.transform.position = transform.position;
    //    yarnball.transform.parent = transform;

    //}

    //public bool isRight()
    //{
    //    return facingRight;
    //}
}
