using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("runleft", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("runleft", 0f);
        Invoke("runright", 1f);
    }
    void runleft()
    {
        Vector3 myVel = GetComponent<Rigidbody2D>().velocity;
        myVel.x = -1.5f;
        //transform.rotation = Quaternion.Euler(0, 0f, 0);

    }
    void runright()
    {
        Vector3 myVel = GetComponent<Rigidbody2D>().velocity;
        myVel.x = -1.5f;
        transform.rotation = Quaternion.Euler(0, 180f, 0);
    }
}
