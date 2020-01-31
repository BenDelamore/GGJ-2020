using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilding : MonoBehaviour
{


    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);

        Time.timeScale = 1;

        if (Input.GetKey(KeyCode.LeftShift)) {
            Time.timeScale = 0.25f;
        }

        if (Input.GetMouseButton(0)) {
            
            foreach (Transform child in transform) {
                if (Vector3.Distance(mousePos, child.transform.position) <= 2) {
                    Debug.Log("Near " + child.name);
                }
            }
        }
    }

    public void HoveringOver(GameObject part) {
        Debug.Log("Hovering Over: " + part);
    }
}
