using UnityEngine;
using System.Collections;

public class EnemyAttacking : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
            Application.LoadLevel("playerMovement3");
    }
}
