using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Rigidbody ball;
    void Start()
    {
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Rigidbody clone = (Rigidbody)Instantiate(ball, transform.position, transform.rotation);
            clone.velocity = transform.forward * 10f;
            Destroy(clone.gameObject, 3);
        }
    }
}