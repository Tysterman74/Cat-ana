using UnityEngine;
using System.Collections;

public class ConveyerBelt : MonoBehaviour {

    public float speed = 2.5f;
    public bool left = false;
    private bool collide = false;
    private GameObject player;
    // Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (collide) {
            //player.transform.Translate(new Vector2(left ? -speed : speed, 0));
            player.rigidbody2D.velocity -= new Vector2(left ? -speed : speed, 0);
        }
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
