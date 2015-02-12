using UnityEngine;
using System.Collections;

public class BoxHidingScript : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}

    void FixedUpdate()
    {

    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D col) 
    {
        if (col.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                print("HIDING");
                //Send message to player that we are hiding.
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //Send message to player we are not hiding.
        /*if (col.tag == "Player")
        {
            //if (Input.GetKeyDown(KeyCode.F))
            //{
                col.gameObject.SendMessage("setHiding", false);
            //}
        }*/
    }
}

