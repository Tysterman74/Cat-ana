using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

    public GameObject flower;
    public Transform node;
    public int spawnTime = 1;

    private bool seedSpawned = false;

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

    IEnumerator spawnRoutine()
    {
        yield return new WaitForSeconds(1);

        flowerClone = (GameObject) Instantiate(flower, node.transform.position + new Vector3(0, 0.7f, 0), node.transform.rotation);
        flowerClone.transform.SetParent(yarnball.transform);
        Physics2D.IgnoreCollision(flowerClone.GetComponent<Collider2D>(), yarnball.GetComponent<Collider2D>());
        Physics2D.IgnoreLayerCollision(flowerClone.gameObject.layer, LayerMask.NameToLayer("Enemy"));
    }

    void spawnFlower()
    {
        if (yarnball.seedOnGround())
        {
            if (!seedSpawned)
            {
                StartCoroutine(spawnRoutine());

                seedSpawned = true;
            }
        }
        else
        {
            if (seedSpawned)
            {
                Destroy(flowerClone);
            }

            seedSpawned = false;
        }
    }
}
