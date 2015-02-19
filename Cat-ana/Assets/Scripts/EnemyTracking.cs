using UnityEngine;
using System.Collections;

public class EnemyTracking : MonoBehaviour {

    public float viewDistance = 5.0f;
	public float hitDistance = 1.0f;
	public float defaultSpeed = 3.0f;
	public float alertedSpeed = 10.0f;
	public int defaultCount = 100;
	public float travelingRadius = 10.0f;
	//public float alertedDistance = 15.0f;
	
	 
    private GameObject point1;
    private GameObject point2;
    private GameObject enemy;


    private bool facingRight;

    private Vector2 direction;


	
	private float velocity;
	private bool detectedPlayer;
	private int count; //this is for the time when enemy loses the sight of player (sort of timer)
	private Vector3 directionVector;

	private Vector3 centerPoint;
	private Vector3 radius;

	//temp
	private LineRenderer detectionLine;
	
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
        
		directionVector =  new Vector3(viewDistance, 0.0f);
		radius = new Vector3 (travelingRadius, 0.0f);
		centerPoint = enemy.transform.position;
		updateEndPoints ();

		//temp
		detectionLine = enemy.GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        
		//detects everything that intersects with the enemy's detectionLine.

		RaycastHit2D [] detect = Physics2D.RaycastAll(enemy.transform.position, direction, viewDistance);

		//raise detection flag if player is detected, reset detection counter
		for (int i = 0; i < detect.Length; i++) {
			//print (detect[i].collider.name);
			//Vader should be changed to Player
            //Tyler: Will, I instead used a tag here. It's a little bit safer
            //compared to using .name.
			if (detect[i].collider.tag == "Player"){
				detectedPlayer = true;
				count = defaultCount;

				//for complex AI on how enemy reacts to player's presence
				updateCenterPoint (1,detect[i].collider.transform.position);

				//this break exists so that enemy ignores wall detection once it detects the player
				//this may not work if the wall is detected before the player
				break;
			}
			//Cube should be changed to Wall
            //Tyler: Here I changed it to Wall, and gave the cube gameobjects a tag of "Wall".
			else if (detect[i].collider.tag == "Wall")
			{
				//change direction
				facingRight = !facingRight;
				//and update center point
				updateCenterPoint (2,enemy.transform.position);
			}

		}

		RaycastHit2D [] hit = Physics2D.RaycastAll(enemy.transform.position, direction, hitDistance);
		for (int i = 0; i < hit.Length; i++) {
			//Vader should be changed to Player
			if (hit[i].collider.name == "Vader" && detectedPlayer){
				//attack player

				break;
			}
		}
		//always update end point based on the center point calculation.
		updateEndPoints();

		//special case: if player is detected, ignore normal behaviour
		if (detectedPlayer){
			velocity = alertedSpeed;
			updateSpecialDirection();



		}
		else
			//if collides with endpoints while not detected
			if(intersectEndPoint())
			//then update its direction
				updateDirection();
		
        //based on the above calculation, set the actual direction and detection line
        if (facingRight)
        {
            
            direction = Vector2.right;

			//temp
            detectionLine.SetPosition(0, enemy.transform.position);
			detectionLine.SetPosition(1, (enemy.transform.position + directionVector));
        }
        else
        {
            direction = -Vector2.right;

			//temp
			detectionLine.SetPosition(0, enemy.transform.position);
            detectionLine.SetPosition(1, (enemy.transform.position - directionVector));
        }
		
		

	
		//update its speed
		updateSpeed();
		
		//actually apply the velocity to the game
        enemy.rigidbody2D.velocity = new Vector2((direction.x * velocity) , enemy.rigidbody2D.velocity.y);

		
    }
	

	//increase the speed of enemy if it detects player
	//back to normal speed if enemy has not seen player for certain time.
	void updateSpeed()
	{
		//in case player is detected
		if (detectedPlayer)
		{
			velocity = alertedSpeed;
			//if player has not been detected for certain time, then turn off the alarm state
			if (count <= 0)
			{
				detectedPlayer = false;
				count = defaultCount;
			}
			else
				count -= 1;
		}
		//if player is not detected, set speed to default speed
		else
			velocity = defaultSpeed;

	}

	//if enemy detects player, special direction update may be required
	void updateSpecialDirection()
	{
		//nothing for now
	}

    //may need to get re-coded
	void updateDirection()
	{


		//face right if enemy intersects the left endpoint
		facingRight = enemy.renderer.bounds.Intersects(point1.renderer.bounds);
		
	}

	//update the center point where enemy patrols
	//may need to get re-coded
	void updateCenterPoint(int option, Vector3 newPos)
	{
		//option 1: update center
		if (option == 1)
			centerPoint = newPos;
		else if (option == 2){
			if (facingRight)
				centerPoint = newPos + radius;
			else
				centerPoint = newPos - radius;
		}
		else
			print ("Invalid call at UpdateCenterPoint");
			
	}

	//update the position of left and right end points.
	void updateEndPoints()
	{
		
		point1.transform.position = centerPoint - radius;
		point2.transform.position = centerPoint + radius;
	}
	
    //if an enemy touches any chosen endpoint, return true
	bool intersectEndPoint()
	{
		return enemy.renderer.bounds.Intersects(point1.renderer.bounds) || enemy.renderer.bounds.Intersects(point2.renderer.bounds);
	}
	
}
