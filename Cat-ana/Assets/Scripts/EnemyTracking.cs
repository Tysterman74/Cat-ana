using UnityEngine;
using System.Collections;

public class EnemyTracking : MonoBehaviour {

    public float viewDistance = 5.0f;
	public float defaultSpeed = 5.0f;
	public float alertedSpeed = 10.0f;
	public int defaultCount = 100;
	
	
    private GameObject point1;
    private GameObject point2;
    private GameObject enemy;

    private bool facingRight;
    private Vector2 direction;
	
    private LineRenderer detectionLine;
	
	private float velocity;
	private bool detectedPlayer;
	private int count;
	private Vector3 directionVector;
	
	// Use this for initialization
	void Start () {
	    //Find the children of this gameobject, which is Endpoint1 and Endpoint2
        point1 = transform.FindChild("EndPoint1").gameObject;
        point2 = transform.FindChild("EndPoint2").gameObject;
        enemy = transform.FindChild("Enemy").gameObject;
        facingRight = true;
		detectedPlayer = false;
		velocity = defaultSpeed;
		count = defaultCount;
        direction = Vector2.right;
        detectionLine = enemy.GetComponent<LineRenderer>();
		directionVector =  new Vector3(viewDistance, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        
		//detects anything that intersects with the enemy's detectionLine.
		RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, viewDistance);


		if (hit.collider.tag == "Player")
		{
			detectedPlayer = true;
			count = defaultCount;
		}


		//special case: if player is detected, ignore normal behaviour
		if (detectedPlayer)
			velocity = alertedSpeed;
		else
			//if collides with endpoints while not detected
			if(intersectEndPoint())
			//then update its direction
				updateDirection();
		
        if (facingRight)
        {
            direction = Vector2.right;
            detectionLine.SetPosition(0, enemy.transform.position);
			detectionLine.SetPosition(1, (enemy.transform.position + directionVector));
        }
        else
        {
            direction = -Vector2.right;
			detectionLine.SetPosition(0, enemy.transform.position);
            detectionLine.SetPosition(1, (enemy.transform.position - directionVector));
        }
		
		

		
		//update its speed
		updateSpeed();
		
		//actually apply the velocity to the game
        enemy.rigidbody2D.velocity = new Vector2((direction.x * velocity) , enemy.rigidbody2D.velocity.y);

        //move to endpoint 2...when hit endpoint 2 face the other direction
        //Move to endpoint 1.
		
    }
	
	//this function is called every time update is called
	//increase the speed of enemy if it detects player
	//back to normal speed if enemy has not seen player for certain time.
	void updateSpeed()
	{
		//in case player is detected
		if (detectedPlayer)
		{
			velocity = alertedSpeed;
			//if player has not been detected for certain time
			if (count <= 0)
			{
				detectedPlayer = false;
				count = defaultCount;
			}
			else
				count -= 1;
		}
		//if player is not detected
		else
			velocity = defaultSpeed;

	}
	
	void updateDirection()
	{
		//face right if enemy intersects the left endpoint
		//may need to get re-coded
		facingRight = enemy.renderer.bounds.Intersects(point1.renderer.bounds);

		
	}
	
	bool intersectEndPoint()
	{
		return enemy.renderer.bounds.Intersects(point1.renderer.bounds) || enemy.renderer.bounds.Intersects(point2.renderer.bounds);
	}
	
}
