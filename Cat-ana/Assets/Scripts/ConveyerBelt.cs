using UnityEngine;
using System.Collections;

public class ConveyerBelt : MonoBehaviour {

    public float speed = 7.5f;
    public float maxSpeed = 3.0f;
    public bool left = false;
    private bool collide = false;
    private GameObject player;
    private movePlayer playerComponent;
    // Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<movePlayer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (collide)
        {
            //player.transform.Translate(new Vector2(left ? -speed : speed, 0));
            Vector2 conveyorBeltSpeed = new Vector2((left ? -speed : speed), 0);
            //playerComponent.setExternalVelocity(conveyorBeltSpeed);
            /*if (left)
            {
                if (playerComponent.isRight())
                {
                    //player.rigidbody2D.velocity 
                    if (player.rigidbody2D.velocity.x < maxSpeed)
                        player.rigidbody2D.velocity -= conveyorBeltSpeed;
                    else
                        player.rigidbody2D.velocity = new Vector2(maxSpeed, 0);
                }
                else
                {
                    print("KEEEE-HYAH");
                    if (player.rigidbody2D.velocity.x > -maxSpeed)
                        player.rigidbody2D.velocity += conveyorBeltSpeed;
                    else
                        player.rigidbody2D.velocity = new Vector2(-maxSpeed, 0);
                }
            }
            else
            {

            }*/
            //player.rigidbody2D.velocity += new Vector2(left ? -speed : speed, 0);
        }
        else
        {
            playerComponent.setExternalVelocity(new Vector2(0, 0));
        }
	}

    void OnCollisionStay2D(Collision2D col)
    {
        print("KEE-HYAH");
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
            collide = true;
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
            collide = false;
    }
}
