using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public GameObject core;
    public float thrustForce;

    public float velocityMag;
    // Start is called before the first frame update
    void Start()
    {
        //foreach (Transform child in transform) {
        //}
    }

    // Update is called once per frame
    void Update()
    {
        velocityMag = core.GetComponent<Rigidbody2D>().velocity.magnitude;

        Forwards();
    }

    void Forwards() {
        foreach (Transform child in transform) {
            if (child.name.Contains("Thruster")) {
                ApplyForce(child.gameObject);
            }
        }
    }

    void ApplyForce(GameObject thruster) {
        thruster.GetComponent<Rigidbody2D>().AddForce(thruster.transform.up * thrustForce);
    }
}
