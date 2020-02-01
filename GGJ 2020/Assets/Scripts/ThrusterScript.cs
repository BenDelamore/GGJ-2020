using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScript : MonoBehaviour
{
    public float thrustForce;
    public float health;
    public GameObject UIelement;

    private void Start()
    {
        health = 100.0f;
        UIelement = transform.GetChild(0).gameObject;
        UIelement.transform.SetParent(GameObject.Find("Canvas").transform);
        //UIelement.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
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

    public void Thrust()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * Time.deltaTime);
        health -= 0.025f;
        if (health <= 0.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}