using UnityEngine;
using System.Collections;

public class Yarnball : MonoBehaviour {

    private bool thrown = false;
    private bool facingRight = false;

	// Use this for initialization
	void Start () {
        collider2D.isTrigger = true;
        rigidbody2D.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        print("Myah");
        if (col.gameObject.tag == "Player")
        {
            collider2D.isTrigger = true;
            rigidbody2D.isKinematic = true;
            col.gameObject.SendMessage("PickupYarn");
            thrown = false;
        }
    }

    void setDirection(bool b)
    {
        facingRight = b;
    }

    void launchYarnball(int charge)
    {
        transform.parent = null;
        rigidbody2D.isKinematic = false;
        rigidbody2D.gravityScale = 1.5f;
        if (facingRight)
            rigidbody2D.AddForce(new Vector2(charge * 13.5f, charge * 3.5f));
        else
            rigidbody2D.AddForce(new Vector2(-charge * 13.5f, charge * 3.5f));
        StartCoroutine(resetCollider());
        thrown = true;
    }

    IEnumerator resetCollider()
    {
        yield return new WaitForSeconds(0.25f);
        collider2D.isTrigger = false;
        rigidbody2D.isKinematic = false;
    }
}
