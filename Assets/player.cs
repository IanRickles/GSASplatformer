using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Animator animator;
    bool grounded = true;
    bool flipped = false;
    bool done = false;
    public int lives;
    public int coins = 0;
    int enemies = 0;
    public GameObject plungerprefab;
    public Text numlives;
    public Text numcoins;
    public Text lose;
    public Text win;
    public AudioSource ching;
    int maxdist = -5;
    public GameObject portalprefab;
    bool camCentered = true;
    bool camMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        lives = 5;
        lose.text = "";
        win.text = "";
        animator = GetComponent<Animator>();
        StartCoroutine("MoveCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (lives == 0)
        {
            lose.text = "You Lose!";
            Time.timeScale = 0;
        }
        if (coins >= 6 && done == false)
        {
            GameObject portal;
            portal = Instantiate(portalprefab, new Vector3(7, 3, 0), Quaternion.Euler(0, 0, 0));
            done = true;
        }
        numlives.text = "Lives: " + lives;
        numcoins.text = "Coins: " + coins;
        Vector3 myVel = GetComponent<Rigidbody2D>().velocity;
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        animator.SetFloat("speed", Mathf.Abs(myVel.x));
        if (grounded == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                myVel.x = 2f;
                flipped = false;
                transform.rotation = Quaternion.Euler(0, 0f, 0);
                GetComponent<Rigidbody2D>().velocity = myVel;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                myVel.x = -2f;
                flipped = true;
                transform.rotation = Quaternion.Euler(0, 180f, 0);
                GetComponent<Rigidbody2D>().velocity = myVel;
                

            }
        }
        if (grounded == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                myVel.x = 1f;
                flipped = false;
                transform.rotation = Quaternion.Euler(0, 0f, 0);
                GetComponent<Rigidbody2D>().velocity = myVel;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                myVel.x = -1f;
                flipped = true;
                transform.rotation = Quaternion.Euler(0, 180f, 0);
                GetComponent<Rigidbody2D>().velocity = myVel;


            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * 200f * GetComponent<Rigidbody2D>().gravityScale);
                grounded = false;
                animator.SetBool("isJumping", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject plunger;
            plunger = Instantiate(plungerprefab, transform.position, transform.rotation);
            plunger.GetComponent<Rigidbody2D>().AddForce(plunger.transform.right * 150f);
            Destroy(plunger, 5f);
        }

        if (transform.position.y <= maxdist)
        {
            Invoke("respawn",0f);


        }
    }

    IEnumerator MoveCamera()
    {

        do
        {
            Vector3 oldPos = Camera.main.transform.position;
            Vector3 temp;

            temp = transform.position - oldPos;
            temp.z = 0f;

            if (temp.magnitude > 1)
            {
                for (float t = 0f; t < 1.0f; t += .025f)
                {
                    Vector3 newPos = transform.position;
                    newPos.z = oldPos.z;

                    Camera.main.transform.position = Vector3.Lerp(oldPos, newPos, t);

                    yield return new WaitForSeconds(.025f);
                }
            }
            yield return new WaitForSeconds(.25f);
        } while (true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spike" || collision.gameObject.tag == "cherry") Invoke("respawn", 0f);
        else if (collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            coins++;
            ching.Play();
        }
        else if (collision.gameObject.tag == "portal" && coins >= 6)
        {
            win.text = "You Win!";
            Time.timeScale = 0;
        }
        else
        {
            grounded = true;
            animator.SetBool("isJumping", false);
        }
    }
    

    void respawn()
    {
        //Vector3 startpos = transform.position;
        //startpos.x = 5f;
        //startpos.y = .5f;
        //transform.position = startpos;
        transform.position = new Vector3(-5, .5f, 0);
        lives--;
    }
}
