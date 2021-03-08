using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    PointCounter pointCounter;

    GameManager manny;
    GameObject tempy;

    SimpleSideController user;

    public int id = 0;

    // Start is called before the first frame update
    void Start()
    {
        pointCounter = GameObject.Find("Canvas").GetComponent<PointCounter>();
        user = GameObject.Find("Knight_man").GetComponent<SimpleSideController>();
        tempy = GameObject.FindGameObjectWithTag("MrManager");
        manny = tempy.GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") 
        {
            if(id == 0)
            {
                manny.highScore ++;    
            }
            else if (id == 1)
            {
                user.doublejump_unlock = true;
            }
            else if (id == 2)
            {
                user.shield_unlock = true;
                user.shield = true;
            }
            Destroy(gameObject);
        }
    }
}
