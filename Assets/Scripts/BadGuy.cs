using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BadGuy : MonoBehaviour
{

    public bool goForward = false;

    public float moveSpeed = 0.2f;

    public float health = 1;

    public bool shooter = false;


    public float bulletForce = 50.0f;

    public bool fireForward = false;

    public bool smart_turning = false;


    SimpleSideController user;

    Animator animator;

    public GameObject spawnPoint;
    public GameObject energyBall;
    void Start()
    {
        animator = GetComponent<Animator>();
        user = GameObject.Find("Knight_man").GetComponent<SimpleSideController>();
        if (shooter)
        {
            FireEnergyBall();
            StartCoroutine(shoot());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move back and forth between point A and B
        // move forward
        if (goForward)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        } 
        else // go backward
        {
            transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (smart_turning)
        {
            float x1 = GameObject.Find("Knight_man").transform.position.x;
            float x2 = gameObject.transform.position.x;
            if(x1 > x2)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                animator.SetBool("shootForward", true);
                fireForward = true;
                goForward = true;
            }
            else
            {
              transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
              animator.SetBool("shootForward", false);
              fireForward = false;
              goForward = false;
            }
        }
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds((float)5);   
        FireEnergyBall();
        StartCoroutine(shoot());    
    }

    void FireEnergyBall() 
    {
        // the Bullet instantiation happens here
        GameObject brandNewPewPew;
        brandNewPewPew = Instantiate(energyBall, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
 
        // get the Rigidbody2D component from the instantiated Bullet and control it
        Rigidbody2D tempRigidBody;
        tempRigidBody = brandNewPewPew.GetComponent<Rigidbody2D>();
 
        // tell the bullet to be "pushed" by an amount set by bulletForce 
        if (fireForward == true) {
            // fireForward is fire to the right
            tempRigidBody.AddForce(transform.right * bulletForce);
        } else {
            // fire left, a.k.a., "negative-right"
            tempRigidBody.AddForce(-transform.right * bulletForce);
        }
        
 
        // basic Clean Up, set the Bullets to self destruct after 5 Seconds
        Destroy(brandNewPewPew, 5.0f);
    }

    void OnCollisionEnter2D(Collision2D otherGuy) 
    {
        if (otherGuy.gameObject.tag == "Player") 
        {
            // write something to the Console just to make 
            // sure this function is being called
            if(user.shield)
            {
                Destroy(gameObject);
                //animator.SetTrigger("break");
            }
            else
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "pointA") 
        {
            goForward = false;
            animator.SetBool("goForward", false);

        }
        else if (other.gameObject.tag == "pointB")
        {
            goForward = true;
            animator.SetBool("goForward", true);

        }
        else if (other.gameObject.tag == "ally_bullet")
        {
            health --;
            if(health < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
