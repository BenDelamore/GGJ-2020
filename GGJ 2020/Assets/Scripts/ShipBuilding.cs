using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilding : MonoBehaviour
{
    bool isDragging = false;
    Vector3 mousePos;
    GameObject draggedObject;

    void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        Time.timeScale = 1;

        if (Input.GetKey(KeyCode.LeftShift)) {
            Time.timeScale = 0.1f;
        }

        if (Input.GetMouseButtonUp(0)) {
            isDragging = false;
        }
    }

    private void LateUpdate() {
        if (isDragging) {
            Dragging();
        }
    }

    public void DragStart(GameObject part) {
        if (isDragging == false) {
            Debug.Log("Hovering Over: " + part);
            isDragging = true;
            draggedObject = part;
        }
    }

    void Dragging() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        Vector3 corePos = gameObject.transform.position;

        draggedObject.transform.position = mousePos;

        Vector3 gridPos = new Vector3(Mathf.Round(draggedObject.transform.localPosition.x / 2.5f)* 2.5f, Mathf.Round(draggedObject.transform.localPosition.y / 2.5f) * 2.5f);

        draggedObject.transform.localPosition = gridPos;
        draggedObject.transform.rotation = gameObject.transform.rotation;
    }
}
