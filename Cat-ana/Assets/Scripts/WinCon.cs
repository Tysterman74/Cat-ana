using UnityEngine;
using System.Collections;

public class WinCon : MonoBehaviour {

    public string levelToLoad;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
            Application.LoadLevel(levelToLoad);
    }
}
