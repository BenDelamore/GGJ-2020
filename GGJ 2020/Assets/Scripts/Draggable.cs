using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool isConnectedToCore = false;
    public bool isTouchingCore = false;

    private GameObject hoverScriptHolder;

    private void Start() {
        hoverScriptHolder = GameObject.Find("Core");
    }

    private void OnMouseDrag() {
        hoverScriptHolder.GetComponent<ShipBuilding>().DragStart(gameObject);
    }

    private void Update() {
        coreConnection();
    }

    void coreConnection() {
        if (transform.parent) {
            if (transform.parent.name == "Core") {
                isTouchingCore = false;
                isConnectedToCore = false;

                if (Within(transform.position, transform.parent.transform.position, 3.84f, 5.12f)) {
                    isTouchingCore = true;
                    isConnectedToCore = true;
                }

                if (GetComponent<FixedJoint2D>().connectedAnchor != null) {
                    foreach (Transform child in transform.parent.transform) {
                        if (Within(transform.position, child.transform.position, 3.84f, 3.86f) && child.gameObject != gameObject) {
                            if (isConnectedToCore == true) {
                                Debug.Log("Here");
                                child.GetComponent<Draggable>().isConnectedToCore = true; //This doesnt set connected objects to true
                            }
                        }
                    }
                }
            }
        }
    }

    bool Within(Vector3 a, Vector3 b, float xMax, float yMax) {
        bool isWithin = false;

        if (Mathf.Abs(a.x - b.x) <= xMax && Mathf.Abs(a.y - b.y) <= yMax) {
            isWithin = true;
        }

        return isWithin;
    }
}
