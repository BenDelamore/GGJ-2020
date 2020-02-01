using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScript : MonoBehaviour
{
    public float thrustForce;
    public float health;
    private GameObject UIelement;

    private void Start()
    {
        health = 100.0f;
        UIelement = transform.GetChild(0).gameObject;
        UIelement.transform.SetParent(GameObject.Find("Canvas").transform);
        UIelement.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void Update()
    {
        Vector3 worldToScreen = Camera.main.WorldToScreenPoint(transform.position);
        GameObject canvas = GameObject.Find("Canvas");
        //Vector3 worldToScreen = WorldToCanvasPosition(canvas.GetComponent<Canvas>(), canvas.GetComponent<RectTransform>(), Camera.main, transform.position);
        UIelement.transform.localPosition = worldToScreen;
    }

    Vector2 WorldToCanvasPosition(Canvas canvas, RectTransform canvasRect, Camera camera, Vector3 position)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, position);
        Vector2 result;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : camera, out result);
        return canvas.transform.TransformPoint(result);
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