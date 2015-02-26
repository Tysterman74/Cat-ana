using UnityEngine;
using System.Collections;

public class attackLogic : MonoBehaviour {

    public Transform playerPosition, attackDistance;

    public bool hit = false;
    public KeyCode attack;
   
	void FixedUpdate()
    {
        rayCasting();
    }

    void rayCasting()
    {
        Debug.DrawLine(playerPosition.position, attackDistance.position, Color.green);
        if (Input.GetKey(attack))
        {
            hit = Physics2D.Linecast(playerPosition.position, attackDistance.position, 1 << LayerMask.NameToLayer("Enemy"));
        } 
    }
}
