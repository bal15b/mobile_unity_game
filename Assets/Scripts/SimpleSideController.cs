using UnityEngine;
using System.Collections;


public class SimpleSideController : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    public float jumpForce = 300.0f;

    public bool isGrounded = false;

    public float bulletForce = 50.0f;

    public bool isAttacking = false;

    private bool left = false;

    public bool doublejump = false;
    public bool doublejump_unlock = true;

    public bool shield = false;
    public bool shield_unlock = false;

    public Joystick joystick;

    Rigidbody2D blahblah;

    Animator animator;
    Animator animator_shield;

    SpriteRenderer spriteRenderer;

    public GameObject spawnPoint;
    public GameObject energyBall;
    public GameObject barrier;

    public bool fireForward = true;

    // Start is called before the first frame update
    void Start()
    {
        blahblah = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        barrier = GameObject.Find("Barrier");

        animator_shield = barrier.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // What Moves Us
        float horizontalInput = joystick.Horizontal;
        //Get the value of the Horizontal input axis.

        transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);

        if (shield)
        {
            animator_shield.SetBool("create", true);
        }

        if (horizontalInput > 0) 
        {
            animator.SetBool("isRunning", true);
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
            fireForward = true;
            if (left)
            {
                transform.position = new Vector3(transform.position.x+(float)0.3,transform.position.y,transform.position.z); 
            }
            left = false;
        } 
        else if (horizontalInput < 0) 
        {
            animator.SetBool("isRunning", true);
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
            fireForward = false;
            if(!left)
            {
                transform.position = new Vector3(transform.position.x-(float)0.3,transform.position.y,transform.position.z); 
            }
            left = true;

        } 
        else 
        {
            animator.SetBool("isRunning", false);
        }
    }

    void FixedUpdate() 
    {
        if (Input.GetButton("Jump") && (isGrounded || doublejump))
        {
            blahblah.AddForce(transform.up * jumpForce);
            animator.SetTrigger("isJumpStart");
            animator.SetBool("isJumping", true);
            if(isGrounded && doublejump_unlock)
            {
                StartCoroutine(jump_seperator());
            }
            else
            {
                doublejump = false;
            }
            isGrounded = false;

            
        }

        if (Input.GetButtonDown("Fire1") && !isAttacking) 
        {
            StartCoroutine(Swing());
            StartCoroutine(move_lag((float)0.5));
        }

        if (Input.GetButtonDown("Fire2") && isGrounded && !isAttacking) 
        {
            isAttacking = true;
            animator.SetTrigger("isAttack2");
            // now instantiate the ball and propel forward
            StartCoroutine(Example());
            StartCoroutine(move_lag((float)1.5));

        }
    }

    public void jump()
    {
        if (isGrounded || doublejump)
        {
            blahblah.AddForce(transform.up * jumpForce);
            animator.SetTrigger("isJumpStart");
            animator.SetBool("isJumping", true);
            if(isGrounded && doublejump_unlock)
            {
                StartCoroutine(jump_seperator());
            }
            else
            {
                doublejump = false;
            }
            isGrounded = false;

            
        }
    }
    public void attack1()
    {
        if (!isAttacking) 
        {
            StartCoroutine(Swing());
            StartCoroutine(move_lag((float)0.5));
        }
    }

    public void attack2()
    {
        if (isGrounded && !isAttacking) 
        {
            isAttacking = true;
            animator.SetTrigger("isAttack2");
            // now instantiate the ball and propel forward
            StartCoroutine(Example());
            StartCoroutine(move_lag((float)1.5));

        }
    }

    IEnumerator jump_seperator()
    {
        yield return new WaitForSeconds((float)0.2);
        doublejump = true;
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds((float)0.5);
        FireEnergyBall();
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds((float)0.25);
        isAttacking = true;
        animator.SetTrigger("isAttack");
    }

    IEnumerator move_lag(float lag)
    {
        yield return new WaitForSeconds(lag);
        isAttacking = false;
        
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

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Jumpy") 
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Jumpy") 
        {
            isGrounded = false;
        }
    }

}
