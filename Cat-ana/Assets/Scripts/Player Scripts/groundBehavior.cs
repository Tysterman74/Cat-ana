using UnityEngine;
using System.Collections;

public class groundBehavior : MonoBehaviour 
{
    private GameObject groundCheck;
    private float groundHeight;

    public LayerMask whatIsGround;
    public bool isOnGround = true;


	void Start () 
    {
        groundCheck = transform.FindChild("GroundCheck").gameObject;
	}
	
	void FixedUpdate () 
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.03f, whatIsGround);
	}

    public bool playerOnGround()
    {
        return isOnGround;
    }
}
