using UnityEngine;
using System.Collections;

public class yarnBallBehavior : MonoBehaviour {

    private int charge;
    public int maxcharge;

    private GameObject yarnball;
    private bool yarnballThrown = false;

    public KeyCode yarn;

    private SpriteRenderer renderPlayer;

    groundBehavior groundBehavior;

    movePlayer movePlayer;

	// Use this for initialization
	void Start () 
    {
        renderPlayer = GetComponent<SpriteRenderer>();
        groundBehavior = FindObjectOfType(typeof(groundBehavior)) as groundBehavior;
        movePlayer = FindObjectOfType(typeof(movePlayer)) as movePlayer;

        charge = 0;
        yarnball = transform.FindChild("YarnBall").gameObject;
	}

    void Update()
    {
        ballCharge();
    }

    IEnumerator resetCollider()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        GetComponent<Collider2D>().isTrigger = false;
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
                yarnball.SendMessage("setDirection", movePlayer.isRight());
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

    void PickupYarn()
    {
        yarnball.transform.position = transform.position;
        yarnball.transform.parent = transform;
    }
}
