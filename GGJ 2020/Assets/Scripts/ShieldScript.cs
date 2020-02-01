using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : moduleBehaviour
{
    public float pushForce;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (transform.parent)
        {
            if (!other.transform.parent)
            {
                Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
                if (otherRB)
                {
                    Vector2 Direction = (other.transform.position - transform.position);
                    Vector2 force = Direction.normalized * pushForce;

                    health -= force.magnitude * 0.005f;
                    otherRB.AddForce(force);
                    if (health <= 0.0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
        
    }
}
