using UnityEngine;
using System.Collections;

public class hidingBehavior : MonoBehaviour {

    public bool hiding = false;

    private ParticleSystem smoke;

    private SpriteRenderer renderPlayer;

    movePlayer movePlayer;

	// Use this for initialization
	void Start () 
    {
        smoke = transform.FindChild("Smoke").GetComponent<ParticleSystem>();
        renderPlayer = GetComponent<SpriteRenderer>();
        movePlayer = FindObjectOfType(typeof(movePlayer)) as movePlayer;
	}
	
	// Update is called once per frame
	void Update () 
    {
        renderHiding();
        hidePlayer();
	}

    void setHiding()
    {
        if (hiding == false)
        {
            hiding = true;
            smoke.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
            gameObject.tag = "Hidden";
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<Collider2D>().isTrigger = true;
        }
        else if (hiding == true)
        {
            hiding = false;
            gameObject.tag = "Player";
            GetComponent<Rigidbody2D>().gravityScale = movePlayer.playerGravity();
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void renderHiding()
    {
        //Makes it transparent when hiding.
        if (!hiding)
        {
            renderPlayer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            renderPlayer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    
    void hidePlayer()
    {
        if (movePlayer.playerIsMoving())
        {
            if (hiding)
                setHiding();
        }
    }
}
