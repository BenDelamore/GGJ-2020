using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Rigidbody2D thisRB;

    public float velocityMag;
   
    void Start()
    {
        thisRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        velocityMag = thisRB.velocity.magnitude;

        if (Input.GetKey("q"))
        {
            thisRB.AddTorque(10.0f);
        }
        if (Input.GetKey("e"))
        {
            thisRB.AddTorque(-10.0f);
        }
        if (Input.GetKey("w"))
        {
            Forwards();
        }

        if (Input.GetKey("s"))
        {
        }

        if (Input.GetKey("a"))
        {
        }

        if (Input.GetKey("d"))
        {
        }
    }

    void Forwards()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Thruster"))
            {
                child.GetComponent<ThrusterScript>().Thrust();
            }
        }
    }
}