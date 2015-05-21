using UnityEngine;
using System.Collections;

public class Yarnball : MonoBehaviour {
    private Vector2 externalVelocity;
    private bool thrown = false;
    //Boolean to check if the ball has been first launched.
    private bool launched = false;
    private bool facingRight = false;
    private bool onConveyorBelt = false;

    private GameObject groundCheck;
    private float groundHeight;

    public float throwDistance = 0.3f;

    public LayerMask whatIsGround;
    public bool isOnGround = true;

	// Use this for initialization
	void Start () {
        print("layer: " + LayerMask.LayerToName(this.gameObject.layer));
        GetComponent<Collider2D>().isTrigger = true;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Enemy"));
        GetComponent<Rigidbody2D>().isKinematic = true;

        groundCheck = transform.FindChild("SeedGroundCheck").gameObject;

	}
	

	// Update is called once per frame
    void Update()
    {
        print("External Velocity: " + externalVelocity);
        if (!thrown)
        {
            externalVelocity = Vector2.zero;
        }

        if (externalVelocity.x != 0)
        {
            GetComponent<Rigidbody2D>().velocity = externalVelocity;
        }
        //else if (externalVelocity.x == 0 
        //         && GetComponent<Rigidbody2D>().transform.parent != null)
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //}
           
        isOnGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.03f, whatIsGround);
        stopOnGround();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("OMFG");
            GetComponent<Collider2D>().isTrigger = true;
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
            GetComponent<Rigidbody2D>().isKinematic = true;
            col.gameObject.SendMessage("PickupYarn");
            externalVelocity = Vector2.zero;
            thrown = false;
            launched = false;
        }
    }


    void setDirection(bool b)
    {
        facingRight = b;
    }

    void launchYarnball(int charge)
    {
        transform.parent = null;
        GetComponent<Rigidbody2D>().isKinematic = false;

        //Change to grabbing x and y velocity and add in velocity equation.
        if (facingRight)
            GetComponent<Rigidbody2D>().velocity = new Vector2(charge * throwDistance, charge * 0.035f);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(-charge * throwDistance, charge * 0.035f);
        StartCoroutine(resetCollider());
        thrown = true;
        launched = true;
    }

    IEnumerator resetCollider()
    {
        yield return new WaitForSeconds(0.25f);
        GetComponent<Collider2D>().isTrigger = false;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>(), false);
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void setExternalVelocity(Vector2 vel)
    {
        externalVelocity = vel;    
    }

    public bool isThrown() 
    {
        return thrown;
    }


    public void stopOnGround()
    {
        if (isOnGround && launched)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            launched = false;
        }
    }

    public bool seedOnGround()
    {
        return isOnGround;
    }

}



