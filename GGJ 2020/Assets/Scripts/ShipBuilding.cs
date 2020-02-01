using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilding : MonoBehaviour
{
    bool isDragging = false;
    bool snapToShip;
    GameObject draggedObject;

    float shipOffset = 1.28f;

    void Update() {
        Time.timeScale = 1;

        if (Input.GetKey(KeyCode.LeftShift)) {
            Time.timeScale = 0.1f;
        }

        if (Input.GetMouseButtonUp(0) && isDragging ==true) {
            DragStop();
        }
    }

    private void LateUpdate() {
        if (isDragging) {
            Dragging();
        }
    }

    public void DragStart(GameObject part) {
        if (isDragging == false) {
            isDragging = true;
            draggedObject = part;

            draggedObject.GetComponent<FixedJoint2D>().enabled = false;
            draggedObject.GetComponent<BoxCollider2D>().enabled = false;
            draggedObject.transform.parent = null;
        }
    }

    void Dragging() {
        snapToShip = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        draggedObject.transform.position = mousePos;


        //check if near children
        foreach (Transform child in transform) {
            if (Within(mousePos, child.transform.position, 3.75f, 3.75f) && child.gameObject != draggedObject) {
                snapToShip = true;
            }
            else {
                if (snapToShip == false) {
                    snapToShip = false;
                }
            }
        }

        //check if near core
        if (snapToShip == false && Within(mousePos, transform.position + transform.up * shipOffset, 3.75f, 5.12f) == true) {
            snapToShip = true;
        }

        if (snapToShip == true) {
            Snap();
        }
    }

    void DragStop() {
        isDragging = false;
        
        bool repaired = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (Transform child in transform) {
            if (Within(mousePos, child.transform.position, 1.25f, 1.25f) && child.gameObject != draggedObject) {
                repair(child.gameObject);
                repaired = true;
                break;
            }
        }
        if (repaired == false) {
            if (Within(mousePos, transform.position + transform.up * shipOffset, 1.5f, 3f)) {
                repair(gameObject);
                repaired = true;
            }
        }

        
        if (snapToShip == true) {
            draggedObject.GetComponent<BoxCollider2D>().enabled = true;
            draggedObject.GetComponent<FixedJoint2D>().enabled = true;
            draggedObject.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            draggedObject.transform.parent = transform;
        }


        foreach (Collider2D collider in draggedObject.GetComponents<Collider2D>())
        {
            if (!collider.isTrigger)
            {
                collider.enabled = true;
            }
            break;
        }
    }

    void Snap() {
        draggedObject.transform.parent = transform;

        Vector3 gridPos = new Vector3(Mathf.Round(draggedObject.transform.localPosition.x / 2.56f) * 2.56f, Mathf.Round((draggedObject.transform.localPosition.y) / 2.56f) * 2.56f);

        draggedObject.transform.localPosition = gridPos;

        draggedObject.transform.localRotation = new Quaternion(0,0,0,0);
    }

    void repair(GameObject target) {
        Debug.Log("repaired " + target.name);
        Destroy(draggedObject.GetComponent<ThrusterScript>().UIelement);
        Destroy(draggedObject);
    }

    bool Within(Vector3 a, Vector3 b, float xMax, float yMax) {
        bool isWithin = false;

        if (Mathf.Abs(a.x - b.x) <= xMax && Mathf.Abs(a.y - b.y) <= yMax) {
            isWithin = true;
        }

        return isWithin;
    }
}
