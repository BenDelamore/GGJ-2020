using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilding : MonoBehaviour
{
    bool isDragging = false;
    bool snapToShip;
    GameObject draggedObject;

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
            draggedObject.GetComponent<Collider2D>().enabled = false;
            draggedObject.transform.parent = null;
        }
    }

    void Dragging() {
        snapToShip = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        Vector3 corePos = gameObject.transform.position;

        draggedObject.transform.position = mousePos;

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

        if (snapToShip == false && Vector3.Distance(mousePos, transform.position) < 7.5) {
            snapToShip = true;
        }

        if (snapToShip == true) {
            Snap();
        }
    }

    void DragStop() {
        isDragging = false;
        
        bool repaired = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        foreach (Transform child in transform) {
            if (Within(mousePos, child.transform.position, 1.25f, 1.25f) && child.gameObject != draggedObject) {
                repair(child.gameObject);
                repaired = true;
                break;
            }
        }
        if (repaired == false) {
            if (Within(mousePos, transform.position, 3.75f, 3.75f)) {
                repair(gameObject);
                repaired = true;
            }
        }


        if (snapToShip == true) {
            draggedObject.GetComponent<Collider2D>().enabled = true;
            draggedObject.GetComponent<FixedJoint2D>().enabled = true;
            draggedObject.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            draggedObject.transform.parent = transform;
        }
        

    }

    void Snap() {
        draggedObject.transform.parent = transform;

        Vector3 gridPos = new Vector3(Mathf.Round(draggedObject.transform.localPosition.x / 2.5f) * 2.5f, Mathf.Round(draggedObject.transform.localPosition.y / 2.5f) * 2.5f);

        draggedObject.transform.localPosition = gridPos;

        draggedObject.transform.localRotation = new Quaternion(0,0,0,0);
    }

    void repair(GameObject target) {
        Debug.Log("repaired " + target.name);
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
