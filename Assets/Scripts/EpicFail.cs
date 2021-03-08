using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class EpicFail : MonoBehaviour
{
    SimpleSideController user;

    public GameObject barrier;
    Animator animator;

    GameManager manny;
    GameObject tempy;

    void Start()
    {
        barrier = GameObject.Find("Barrier");

        animator = barrier.GetComponent<Animator>();
        tempy = GameObject.FindGameObjectWithTag("MrManager");
        manny = tempy.GetComponent<GameManager>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            user = GameObject.Find("Knight_man").GetComponent<SimpleSideController>();
            // write something to the Console just to make 
            // sure this function is being called
            if(user.shield)
            {
                Destroy(gameObject);
                animator.SetTrigger("break");
                StartCoroutine(i_frames(user));
            }
            else
            {
                manny.numLivesLeft --;
                if (manny.numLivesLeft < 0)
                {
                    SceneManager.LoadScene("GameOver");
                }
                else 
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

        }

    }

    IEnumerator i_frames(SimpleSideController user)
    {
        user.shield = false;
        animator.SetBool("create", false);

        yield return new WaitForSeconds((float)0.1);
    }
}
