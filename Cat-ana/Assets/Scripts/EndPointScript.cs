using UnityEngine;
using System.Collections;

public class EndPointScript : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            print("hit");
            col.transform.parent.GetComponent<EnemyTracking>().setAtEndPoint(true);
        }
    }
}
