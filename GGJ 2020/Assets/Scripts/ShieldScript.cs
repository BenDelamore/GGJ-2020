﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float pushForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                    otherRB.AddForce(force);
                }
            }
        }
        
    }
}
