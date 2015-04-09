using UnityEngine;
using System.Collections;

public class attackLogic : MonoBehaviour {

    public Transform playerPosition, attackDistance;
    
    Animator anim;

    public bool hit = false;
    public KeyCode attack;

    public bool attackClicked = false;
    float attackLength = 0.625f;
    float attackTime = 0.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
   
	void FixedUpdate()
    {
        rayCasting();

        enableAttack();
    }

    void enableAttack()
    {
        if (attackClicked)
        {
            attackTime += Time.deltaTime;

            if (attackTime >= attackLength)
            {
                attackClicked = false;

                anim.SetBool("attackClicked", attackClicked);

                attackTime = 0.0f;
            }
        }

        if (Input.GetKey(attack))
        {
            attackClicked = true;
            anim.SetBool("attackClicked", attackClicked);
        }
    }

    void rayCasting()
    {
        if (Input.GetKey(attack))
        {
            hit = Physics2D.Linecast(playerPosition.position, attackDistance.position, 1 << LayerMask.NameToLayer("Enemy"));
        } 
    }
}
