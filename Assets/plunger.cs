using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plunger : MonoBehaviour
{
    public AudioSource suction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "frog")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag != "player" && collision.gameObject.tag != "plunger")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            suction.Play();
        }
    }
}
