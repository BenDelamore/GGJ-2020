using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float velocityMag;
   
    void Start()
    {

    }

    void Update()
    {
        velocityMag = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;

        if (Input.GetKey("w"))
        {
            Forwards();
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