using System.Collections;
using UnityEngine;

public class LoadonClick : MonoBehaviour
{
    public string level;

    public void LoadScene(string level)
    {
            Application.LoadLevel(level);
    
    }

}
