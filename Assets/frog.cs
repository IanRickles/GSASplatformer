using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour
{
    public GameObject cherryprefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("throwCherry", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void throwCherry()
    {
        GameObject cherry;
        cherry = Instantiate(cherryprefab, transform.position, transform.rotation);
        cherry.GetComponent<Rigidbody2D>().AddForce(cherry.transform.right * -150f);
        cherry.GetComponent<Rigidbody2D>().AddForce(cherry.transform.up * 100f);
        //Destroy(cherry, 1f);
        Invoke("throwCherry", .5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "plunger")
        {
            Destroy(collision.gameObject);
        }
    }
}
