using UnityEngine;
using System.Collections;

public class EnemyTracking : MonoBehaviour {

    public float viewDistance = 5.0f;
	public float hitDistance = 1.0f;
	public float defaultSpeed = 3.0f;
	public float alertedSpeed = 10.0f;
	public int defaultAlertTimer = 100; // Enemy will not be alarmed after certain time. (This may not be needed)
    public float defaultTurnAroundTimer = 3.0f; //even if the enemy doesn't reach the end point, it will turn around after certain time
	public float distractedTimer = 3.0f;
	public float travelingRadius = 10.0f;
    public bool facingRight; //true means right
	//public float alertedDistance = 15.0f;
	
	 
    private GameObject point1;
    private GameObject point2;
    private GameObject enemy;
    private GameObject flower;


    private Vector2 direction;

    private bool turning;
    private bool atEndPoint;
	private float velocity;
	private bool detectedPlayer;
	private int alertTimer; //this is for the time when enemy loses the sight of player
    private float turnAroundTimer; //this is for the time when enemy needs to turn around
	private Vector3 directionVector;

	private Vector3 centerPoint;
	private Vector3 radius;

	//temp
	private LineRenderer detectionLine;
	


    //Things to work on:
    //1. After certain amount of time, the enemy will turn even if it didn't reach the end point
    //2. Enemy will sometime stand idle to add a sense of random behaviour
    //3. Child classes for enemy: melee, range, alarm, etc.
    //4. Implement attack for enemy: melee, range
    //






	// Use this for initialization
	void Start () {
	    //Find the children of this gameobject, which is Endpoint1 and Endpoint2
        
        point1 = transform.FindChild("EndPoint1").gameObject;
        point2 = transform.FindChild("EndPoint2").gameObject;
        enemy = transform.FindChild("Enemy").gameObject;
        flower = null;


        turning = false;
        atEndPoint = false;
		detectedPlayer = false;
		velocity = defaultSpeed;
		alertTimer = defaultAlertTimer;
        turnAroundTimer = defaultTurnAroundTimer;
        direction = Vector2.right;
        
		directionVector =  new Vector3(viewDistance, 0.0f);
		radius = new Vector3 (travelingRadius, 0.0f);
		centerPoint = enemy.transform.position;
		//updateEndPoints ();

		//temp
		detectionLine = enemy.GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
		//detects everything that intersects with the enemy's detectionLine.

		RaycastHit2D [] detect = Physics2D.RaycastAll(enemy.transform.position, direction, viewDistance);

		//raise detection flag if player is detected, reset detection counter
		for (int i = 0; i < detect.Length; i++) {
			//print (detect[i].collider.name);

			if (detect[i].collider.tag == "Player"){
				detectedPlayer = true;
                //reset the timer
                alertTimer = defaultAlertTimer;

				//for complex AI on how enemy reacts to player's presence
				//updateCenterPoint (1,detect[i].collider.transform.position);

				//this break exists so that enemy ignores wall detection once it detects the player
				//this may not work if the wall is detected before the player
				break;
			}

            else if (detect[i].collider.tag == "Flower")
            {
                print("AKFASFKWEGNKADNC");
                flower = detect[i].collider.gameObject;

				
				if (distractedTimer <= 0.0f)
				{ //if time is up, kill the flower
					print ("kill flower");
					Destroy (GameObject.FindWithTag("Flower"));
				}
				else
				{ 
					//subtract time from distracted timer
					print("subtractTime");
					distractedTimer -= Time.deltaTime;
				}


            }

            //this may be changed to else statement such that enemy turns around if it detects anything but player.
            else if (detect[i].collider.tag == "Wall")
            {
                //change direction
                facingRight = !facingRight;
                //and update center point
                //updateCenterPoint(2, enemy.transform.position);
            }

		}

		RaycastHit2D [] hit = Physics2D.RaycastAll(enemy.transform.position, direction, hitDistance);
		for (int i = 0; i < hit.Length; i++) {
			//Vader should be changed to Player
			if (hit[i].collider.tag == "Player" && detectedPlayer){
				//attack player

				break;
			}
		}
		//always update end point based on the center point calculation.
		//updateEndPoints();

		//special case: if player is detected, ignore normal behaviour
        if (detectedPlayer)
        {
            velocity = alertedSpeed;
            updateSpecialDirection();



        }
        else if (flower != null){

			//calculate distance. Stop if certain distance away 
			float distance = Vector3.Distance(transform.position,GameObject.FindWithTag("Flower").transform.position);
			print (distance);

			if(distance <= 2.0 ){
				print ("enemy stops");
				enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
        }
        else if (atEndPoint)
        {

            if (turnAroundTimer <= 0.0f)
            {
                //decrease direction timer by 1 every tick
                //turnAroundTimer -= 1;
                //print("Intersect: " + intersectEndPoint());

                //if collides with endpoints while not detected
                //if (atEndPoint)
                //{
                //    print("Turning");
                //    //then update its direction
                //    updateDirection();
                //    //and reset timer
                //    turnAroundTimer = defaultTurnAroundTimer;
                //}
                //else if (turnAroundTimer <= 0)
                //{
                //    facingRight = !facingRight;
                //    turnAroundTimer = defaultTurnAroundTimer;
                //}
                //facingRight != facingRight;
                facingRight = !facingRight;
                //turning = true;
                //updateDirection();
                atEndPoint = false;
                turnAroundTimer = defaultTurnAroundTimer;
            }
            else
            {
                print("asd");
                turnAroundTimer -= Time.deltaTime;
            }
            //print(turnAroundTimer);

        }
        //else if (turning) 
        //{
        //    turnAroundTimer -= 1;
        //    if (turnAroundTimer <= 0)
        //    {
        //        turning = false;
        //        facingRight = !facingRight;
        //        updateDirection();
        //    }
        //}
		
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
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2((direction.x * velocity) , enemy.GetComponent<Rigidbody2D>().velocity.y);

		
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
            if (alertTimer <= 0)
            {
                detectedPlayer = false;
                alertTimer = defaultAlertTimer;
            }
            else
                alertTimer -= 1;
        }
        //if player is not detected, set speed to default speed
        else
        {
            if (facingRight)
            {
                direction = Vector2.right;
            }
            else
            {
                direction = -Vector2.right;
            }

            if (!atEndPoint)
            {
                velocity = defaultSpeed;
            }
            else
            {
                velocity = 0.0f;
            }
        }

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
		//facingRight = enemy.GetComponent<Renderer>().bounds.Intersects(point1.GetComponent<Renderer>().bounds);
		
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
        return (enemy.transform.position.x == point1.transform.position.x && enemy.transform.position.y == point1.transform.position.y)
            || (enemy.transform.position.x == point2.transform.position.x && enemy.transform.position.y == point2.transform.position.y);
		//return enemy.GetComponent<Renderer>().bounds.Intersects(point1.GetComponent<Renderer>().bounds) || enemy.GetComponent<Renderer>().bounds.Intersects(point2.GetComponent<Renderer>().bounds);
	}

    public void setAtEndPoint(bool b) 
    {
        print("setting");
        atEndPoint = b;
    }
	
}
