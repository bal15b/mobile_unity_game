using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject otherObject;
    Animator animator;
    GameManager manny;
    GameObject tempy;
    

    void Start()
    {
        animator = otherObject.GetComponent<Animator> ();
        tempy = GameObject.FindGameObjectWithTag("MrManager");
        manny = tempy.GetComponent<GameManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "BadGuy" && this.animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            manny.highScore += 2;
            Destroy(other.gameObject);
        }
    }
}
