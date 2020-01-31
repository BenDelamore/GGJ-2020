using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public GameObject core;

    public float velocityMag;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        velocityMag = core.GetComponent<Rigidbody2D>().velocity.magnitude;

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