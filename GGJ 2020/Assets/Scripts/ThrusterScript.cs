using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScript : MonoBehaviour
{
    public float thrustForce;

    public void Thrust()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce);
    }
}