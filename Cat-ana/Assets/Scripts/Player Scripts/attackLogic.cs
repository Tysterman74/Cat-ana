using UnityEngine;
using System.Collections;

public class attackLogic : MonoBehaviour {

    public Transform playerPosition, attackDistance;

    Animator anim;

    public bool hit = false;
    public KeyCode attack;
    public AudioClip attackSound1;
    public AudioClip attackSound2;

    public bool attackClicked = false;
    float attackLength = 0.625f;
    float attackTime = 0.0f;
    float attackSoundTime = 0.0f;

    private AudioSource source;
    private bool soundEnabled = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
   
	void FixedUpdate()
    {
        rayCasting();
        enableAttack();
        playAttackSound();
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

        if (Input.GetKey(attack) && gameObject.tag != "Hidden")
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

    void playAttackSound()
    {      
        if (attackClicked)
        {
            attackSoundTime += Time.deltaTime;

            if (attackSoundTime <= 0.02)
            {
                source.PlayOneShot(attackSound1, 1F);
            }

            if (attackSoundTime >= attackLength/2 && attackSoundTime <= (attackLength/2)+0.01)
            {
                source.PlayOneShot(attackSound2, 1F);
            }
            
            if (attackSoundTime >= attackLength)
            {
                attackSoundTime = 0.0f;
            }
        }
    }
}
