using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

    public GameObject flower;
    public Transform node;
    public float spawnTime = 1.0f;
    public float destroyTime = 4.0f;
    private float currentSpawnTime = 0.0f;
    private float currentDestroyTime = 0.0f;

    private bool seedSpawned = false;
    private bool flowerAlreadySpawned = false;

    private GameObject flowerClone;

    Yarnball yarnball;

	// Use this for initialization
	void Start () 
    {
        yarnball = FindObjectOfType(typeof(Yarnball)) as Yarnball;
	}
	
	// Update is called once per frame
	void Update () 
    {
        spawnFlower();
	}

    void spawnFlower()
    {
        if (ballThrown())
        {
            if (yarnball.seedOnGround())
            {
                if (!flowerAlreadySpawned)
                {
                    currentSpawnTime += Time.deltaTime;
                }
                

                if (currentSpawnTime >= spawnTime && !flowerAlreadySpawned)
                {
                    Destroy(flowerClone);

                    flowerClone = (GameObject)Instantiate(flower, node.transform.position + new Vector3(0, 0.7f, 0), node.transform.rotation);
                    flowerClone.transform.SetParent(yarnball.transform);
                    Physics2D.IgnoreCollision(flowerClone.GetComponent<Collider2D>(), yarnball.GetComponent<Collider2D>());
                    //Physics2D.IgnoreLayerCollision(flowerClone.gameObject.layer, LayerMask.NameToLayer("Enemy"));

                    seedSpawned = true;

                    flowerAlreadySpawned = true;

                    currentSpawnTime = 0.0f;
                }
            }
        }
        else
        {
            flowerAlreadySpawned = false;
            Destroy(flowerClone);
        }
    }


    private bool ballThrown()
    {
        if (yarnball.transform.parent == null)
        {
            return true;
        }
        else
            return false;
    }
}
