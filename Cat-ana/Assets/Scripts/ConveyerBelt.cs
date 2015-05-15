using UnityEngine;
using System.Collections;

public class ConveyerBelt : MonoBehaviour {

    public float speed = 7.5f;
    public float maxSpeed = 3.0f;
    public bool left = false;
    private bool collide = false;
    private bool ballOnConveyerBelt = false;
    private GameObject player;
    private GameObject yarn;
    private movePlayer playerComponent;
    private Yarnball yarnComponent;
    // Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<movePlayer>();
        yarn = GameObject.Find("YarnBall");
        yarnComponent = yarn.GetComponent<Yarnball>();

    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerComponent.setExternalVelocity(new Vector2(left ? -speed : speed, 0));
            //Rigidbody2D playerrigidbody = col.gameObject.rigidbody2D;
            //playerrigidbody.velocity = speed * Time.deltaTime * transform.forward;
        }
        else if (col.gameObject.tag == "YarnBall")
        {
            print("SADFDSA");
            yarnComponent.setExternalVelocity(new Vector2(left ? -speed : speed, 0));
            //col.gameObject.SendMessage("setExternalVelocity", new Vector2(left ? -speed : speed, 0));
            ballOnConveyerBelt = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerComponent.setExternalVelocity(new Vector2(0, 0));
        }
        else if (col.gameObject.tag == "YarnBall")
        {
            yarnComponent.setExternalVelocity(new Vector2(0, 0));
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Player")
            collide = true;

        else if (col.gameObject.tag == "YarnBall")
            collide = true;
    }

}
