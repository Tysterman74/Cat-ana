using UnityEngine;
using System.Collections;

public class EnemyTracking : MonoBehaviour {

    public float viewDistance = 5.0f;

    private GameObject point1;
    private GameObject point2;
    private GameObject player;

    private bool facingRight;
    private Vector2 direction;
    private LineRenderer line;
	// Use this for initialization
	void Start () {
	    //Find the children of this gameobject, which is Endpoint1 and Endpoint2
        point1 = transform.FindChild("EndPoint1").gameObject;
        point2 = transform.FindChild("EndPoint2").gameObject;
        player = transform.FindChild("Enemy").gameObject;
        facingRight = true;
        direction = Vector2.right;
        line = player.GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 directionVector =  new Vector3(viewDistance, 0.0f);
        line.SetPosition(0, player.transform.position);
        if (facingRight)
        {
            direction = Vector2.right;
            line.SetPosition(1, (directionVector + player.transform.position));
        }
        else
        {
            direction = -Vector2.right;
            line.SetPosition(1, -(directionVector + player.transform.position));
        }
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction, viewDistance);

        player.rigidbody2D.velocity = new Vector2((direction.x * 5.0f) , player.rigidbody2D.velocity.y);

        //move to endpoint 2...when hit endpoint 2 face the other direction
        //Move to endpoint 1.
    }
}
