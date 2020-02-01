using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScript : moduleBehaviour
{
    public float thrustForce;

    public void Thrust()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * Time.deltaTime);
        health -= 0.25f;
        if (health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}