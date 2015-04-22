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
            col.gameObject.SendMessage("setNearHidable", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Hidden")
        {
            col.gameObject.SendMessage("setNearHidable", false);
        }
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

