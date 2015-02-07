using UnityEngine;
using System.Collections;

public class EnemyTracking : MonoBehaviour {

    private GameObject point1;
    private GameObject point2;
	// Use this for initialization
	void Start () {
	    //Find the children of this gameobject, which is Endpoint1 and Endpoint2
        point1 = transform.FindChild("EndPoint1").gameObject;
        point2 = transform.FindChild("EndPoint2").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	    //To-Do:
        //1.  When game starts, enemy should go to point1. Once it does, have it track to point2.
	    //  Sidenote: Don't worry about having the enemy jump for now, assume that it's on a flat surface.
        //2. Create a bounding box placed in front of the enemy. This will be the Look range of the enemy. If the player 
        //  or the distraction (yarn ball) enters this bounding box, then it'll trigger the enemy to walk to the player/distraction.
        //Just work on these two tasks for now, then we will discuss something further from there.
    }
}
