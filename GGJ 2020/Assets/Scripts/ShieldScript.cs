using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float pushForce;
    public float health;
    public GameObject UIelement;

    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        UIelement = transform.GetChild(0).gameObject;
        UIelement.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    private void Update()
    {
        Vector3 worldToScreen = Camera.main.WorldToScreenPoint(transform.position);
        GameObject canvas = GameObject.Find("Canvas");
        worldToScreen.x -= canvas.GetComponent<Canvas>().pixelRect.width * 0.5f;
        worldToScreen.y -= canvas.GetComponent<Canvas>().pixelRect.height * 0.5f;
        UIelement.transform.localPosition = worldToScreen;
    }
    private void OnMouseEnter()
    {
        UIelement.transform.GetComponent<ModHealthUI>().showHealth = true;
    }

    private void OnMouseExit()
    {
        UIelement.transform.GetComponent<ModHealthUI>().showHealth = false;
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

                    health -= force.magnitude * 0.005f;
                    otherRB.AddForce(force);
                    if (health <= 0.0f)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
        
    }
}
