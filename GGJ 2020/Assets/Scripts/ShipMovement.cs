using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    public float ThrustForce;
    // Start is called before the first frame update
    void Start()
    {
        //foreach (Transform child in transform) {
        //}
    }

    // Update is called once per frame
    void Update()
    {
        forwards();
    }

    void forwards() {
        foreach (Transform child in transform) {
            if (child.name.Contains("Thruster")) {
                applyForce(child.gameObject);
            }
        }
    }

    void applyForce(GameObject thruster) {
        Debug.Log("here");
        thruster.GetComponent<Rigidbody2D>().AddForce(thruster.transform.up * ThrustForce);
    }
}
