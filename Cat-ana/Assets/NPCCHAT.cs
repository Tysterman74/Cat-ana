using UnityEngine;
using System.Collections;

public class NPCCHAT : MonoBehaviour {

    public GameObject LevelPicker;
    public GameObject Player;
    public GameObject S;

    public KeyCode PortalButton;

    private bool talking = false;

    void Start()
    {
        LevelPicker.SetActive(false);

    }

    void Update()
    {
        if (talking)
        {
            stopPlayer();
        }
    }

    void stopPlayer()
    {
        Player.GetComponent<movePlayer>().enabled = false;
    }

    void exit()
    {
        LevelPicker.SetActive(false);
        S.SetActive(true);
        talking = false;
        Player.GetComponent<movePlayer>().enabled = true;
    }

	void OnTriggerStay2D (Collider2D col) 
    {
        if (col.tag == "Player")
        {
            if(Input.GetKeyDown(PortalButton))
            {
                S.SetActive(false);
                LevelPicker.SetActive(true);
                talking = true;
            }
        }

	}
}
