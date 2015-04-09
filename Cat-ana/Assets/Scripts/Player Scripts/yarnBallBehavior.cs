using UnityEngine;
using System.Collections;

public class yarnBallBehavior : MonoBehaviour {

    private ParticleSystem smoke;
    private int charge;
    public int maxcharge;

    private GameObject yarnball;
    private bool yarnballThrown = false;
    public KeyCode yarn;

    public KeyCode right;
    public KeyCode left;
    public KeyCode jump;

    private bool hiding = false;
    private bool facingRight = true;

    private SpriteRenderer renderPlayer;

    groundBehavior groundBehavior;

	// Use this for initialization
	void Start () 
    {
        smoke = transform.FindChild("Smoke").GetComponent<ParticleSystem>();
        renderPlayer = GetComponent<SpriteRenderer>();
        charge = 0;
        yarnball = transform.FindChild("YarnBall").gameObject;

        groundBehavior = FindObjectOfType(typeof(groundBehavior)) as groundBehavior;
	}

    IEnumerator resetCollider()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        GetComponent<Collider2D>().isTrigger = false;
    }

    void Update()
    {
        renderHiding();
        ballCharge();
        hideFacingLeft();
        hideFacingRight();
        jumpFromHiding();
    }

    void renderHiding()
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
    }

    void ballCharge()
    {
        //When grounded, If holding the button for yarn then release.
        if (groundBehavior.playerOnGround())
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

    void hideFacingRight()
    {
        if (Input.GetKey(right))
        {
            if (hiding)
                setHiding();
            facingRight = true;
        }
    }

    void hideFacingLeft()
    {
        if (Input.GetKey(left))
        {
            if (hiding)
                setHiding();
            facingRight = false;
        }
    }

    void jumpFromHiding()
    {
        if (Input.GetKey(jump))
        {
            if (hiding)
                setHiding();
        }
    }
}
