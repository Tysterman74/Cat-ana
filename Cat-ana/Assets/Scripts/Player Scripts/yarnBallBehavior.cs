using UnityEngine;
using System.Collections;

public class yarnBallBehavior : MonoBehaviour {

    private int charge;
    public int maxcharge;

    private GameObject yarnball;
    private bool yarnballThrown = false;

    private GameObject chargeParticles;
    private ParticleSystem particles;
    private bool showParticles = false;

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
        chargeParticles = transform.FindChild("YarnCharge").gameObject;
        particles = chargeParticles.GetComponent<ParticleSystem>();

        chargeParticles.SetActive(false);
	}

    void Update()
    {
        ballCharge();
    }

    void resetCollider()
    {
//        yield return new WaitForSeconds(1.0f);
        GetComponent<Rigidbody2D>().gravityScale = movePlayer.playerGravity();
        //GetComponent<Collider2D>().isTrigger = false;
    }

    void ballCharge()
    {
        if (showParticles)
        {
            particles.startSize += particles.startSize / (100.0f/3.0f);
            if (particles.startSize >= 3.0f)
            {
                particles.startSize = 3.0f;
            }
        }

        //When grounded, If holding the button for yarn then release.
        //if (groundBehavior.playerOnGround())
        //{
        //TODO:
        //Add particle effects and asethics to charge
        //Used for charging the yarn
        if (!yarnball.GetComponent<Yarnball>().isThrown())
        {
            if (Input.GetKey(yarn))
            {
                print("charge");

                charge += 1;
                if (charge > 100)
                {
                    charge = 100;
                }

                showParticles = true;
                chargeParticles.SetActive(true);

            }
            //When release yarn button
            if (Input.GetKeyUp(yarn))
            {
                yarnball.SendMessage("setDirection", movePlayer.playerIsFacingRight());
                print("thrown");
                print(charge);
                GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                //GetComponent<Collider2D>().isTrigger = true;
                yarnball.SendMessage("launchYarnball", charge);
                resetCollider();
                charge = 0;

                showParticles = false;
                chargeParticles.SetActive(false);
                particles.startSize = 1;

            } 
        }
        //}
    }

    void PickupYarn()
    {
        yarnball.transform.position = transform.position;
        yarnball.transform.parent = transform;
    }
}
