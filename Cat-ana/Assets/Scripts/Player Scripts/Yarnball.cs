﻿using UnityEngine;
using System.Collections;

public class Yarnball : MonoBehaviour {
    private Vector2 externalVelocity;
    private bool thrown = false;
    private bool facingRight = false;
    private bool onConveyorBelt = false;

	// Use this for initialization
	void Start () {
        GetComponent<Collider2D>().isTrigger = true;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
        GetComponent<Rigidbody2D>().isKinematic = true;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (externalVelocity.x != 0)
            GetComponent<Rigidbody2D>().velocity = externalVelocity;
    }

	void Update () {
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        print("Myah");
        if (col.gameObject.tag == "Player")
        {
            GetComponent<Collider2D>().isTrigger = true;
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
            GetComponent<Rigidbody2D>().isKinematic = true;
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
        GetComponent<Rigidbody2D>().isKinematic = false;

        //Change to grabbing x and y velocity and add in velocity equation.
        if (facingRight)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(charge * 13.5f, charge * 3.5f));
        else
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-charge * 13.5f, charge * 3.5f));
        StartCoroutine(resetCollider());
        thrown = true;
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
        print(vel);
        externalVelocity = vel;
    
    }

    public bool isThrown() 
    {
        return thrown;
    }




}



