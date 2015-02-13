using UnityEngine;
using System.Collections;

public class EnemyTracking : MonoBehaviour {

    public float viewDistance = 5.0f;

    private GameObject point1;
    private GameObject point2;
    private GameObject player;

    private bool facingRight;
    private Vector2 direction;
	// Use this for initialization
	void Start () {
	    //Find the children of this gameobject, which is Endpoint1 and Endpoint2
        point1 = transform.FindChild("EndPoint1").gameObject;
        point2 = transform.FindChild("EndPoint2").gameObject;
        player = transform.FindChild("Enemy").gameObject;
        facingRight = true;
        direction = Vector2.right;
    }
	
	// Update is called once per frame
	void Update () {
        if (facingRight)
            direction = Vector2.right;
        else
            direction = -Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction, viewDistance);
        //move to endpoint 2...when hit endpoint 2 face the other direction
        //Move to endpoint 1.
    }
}
