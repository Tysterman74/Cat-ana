using UnityEngine;
using System.Collections;

public class playerRayCast : MonoBehaviour {

    public Transform playerPosition, attackDistance;

    public bool hit = false;
   
	void FixedUpdate()
    {
        rayCasting();
        behaviors();
    }

    void rayCasting()
    {
        Debug.DrawLine(playerPosition.position, attackDistance.position, Color.green);
        hit = Physics2D.Linecast(playerPosition.position, attackDistance.position);
    }

    void behaviors()
    {

    }
}
