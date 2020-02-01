using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public GameObject core;
    private Rigidbody2D thisRB;
    public float health;

    public float velocityMag;
   
    void Start()
    {
        health = 100.0f;
        thisRB = core.GetComponent<Rigidbody2D>();
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

    // 0 = forwards, 1 = backwards, 2 = left, 3 = right
    void Move(int _direction)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 0.2f)
        {
            if (collision.gameObject.name.Contains("Thruster"))
            {
                health -= 2.0f;
            }
        }
    }
}