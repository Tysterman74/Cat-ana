using UnityEngine;
using System.Collections;

public class movePlayer : MonoBehaviour 
{

	// Key inputs for player actions
    // Used for different player actions
	public KeyCode right;
	public KeyCode left;
	public KeyCode jump;

	// Player properties
	public float speed = 5.0f;
    public float maxSpeed = 7.5f;
    public float jumpSpeed = 20.0f;
    private float gravScale;
    private bool facingRight = true;
    private bool playerMoving;

    public AudioClip jumpSound1;
    public AudioClip jumpSound2;
    public AudioClip jumpSound3;
    private bool oneJumpDone = false;
    private bool twoJumpsDone = false;
   
    private AudioSource source;

    Animator anim;

    groundBehavior groundBehavior;

    private Vector2 externalVelocity; 
    
    void Start()
    {
        // Initializes groundBehavior object used for checking if the player is on the ground
        // with the playerOnGround() method that returns a bool
        groundBehavior = FindObjectOfType(typeof(groundBehavior)) as groundBehavior;

        gravScale = GetComponent<Rigidbody2D>().gravityScale;

        // Initializes animator object used for determining what animations to play based on
        // player movement
        anim = GetComponent<Animator>();

        source = GetComponent<AudioSource>();
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

            facingRight = true;
            playerMoving = true;
        }

        // Left arrow key pressed?
        else if (Input.GetKey(left))
        {
            //Set the velocity for what the PLAYER itself has.
            totalVelocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            transform.eulerAngles = new Vector2(0, 180); // Flips player direction to left

            anim.SetFloat("speed", 1);

            facingRight = false;
            playerMoving = true;
        }
        else
        {
            playerMoving = false;
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

                playerMoving = true;

                playJumpSound();
            }

            GetComponent<Rigidbody2D>().velocity = totalVelocity;
        }
        else
        {
            playerMoving = false;
        }

        //Add in the external velocity that other forces are acting on the player.
        totalVelocity += externalVelocity;

        GetComponent<Rigidbody2D>().velocity = totalVelocity;
    }

    void playJumpSound()
    {
        if (oneJumpDone)
        {
            source.PlayOneShot(jumpSound2);

            oneJumpDone = false;
            twoJumpsDone = true;
        }
        else if (twoJumpsDone)
        {
            source.PlayOneShot(jumpSound3);

            oneJumpDone = false;
            twoJumpsDone = false;
        }
        else
        {
            source.PlayOneShot(jumpSound1, 1F);

            oneJumpDone = true;
        }
    }

    public void setExternalVelocity(Vector2 vel)
    {
        externalVelocity = vel;
    }

    public bool playerIsFacingRight()
    {
        return facingRight;
    }
 
    public bool playerIsMoving()
    {
        return playerMoving;
    }

    public float playerGravity()
    {
        return gravScale;
    }
}
