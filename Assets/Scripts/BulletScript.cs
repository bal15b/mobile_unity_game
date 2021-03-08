using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Animator animator;
    public float delay = 1f;
    Rigidbody2D blahblah;
    BadGuy enemy;

    void Start()
    {
        animator = GetComponent<Animator>();
        blahblah = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "BadGuy" || other.gameObject.tag == "Wall" || other.gameObject.tag == "boss") && gameObject.tag == "ally_bullet")
        {   
            animator.SetTrigger("collision");
            //Destroy(gameObject);
            Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
            
            blahblah.velocity = Vector2.zero;
            blahblah.angularVelocity = (float)0; 
        }
        else if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Wall") && gameObject.tag == "evil_bullet")
        {   
            animator.SetTrigger("collision");
            //Destroy(gameObject);
            Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
            
            blahblah.velocity = Vector2.zero;
            blahblah.angularVelocity = (float)0; 
        }

    }
}
