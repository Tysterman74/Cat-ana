using UnityEngine;
using System.Collections;

public class TestMovement : MonoBehaviour {

    float speed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}

    void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * direction, GetComponent<Rigidbody2D>().velocity.y);
        /*if (direction > 0)
        {

        }
        else
        {
            rigidbody2D.velocity = new Vector2(speed 
        }*/
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
