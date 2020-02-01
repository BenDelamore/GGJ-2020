using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{

    private GameObject hoverScriptHolder;

    private void Start() {
        hoverScriptHolder = GameObject.Find("Core");
    }

    private void OnMouseDrag() {
        hoverScriptHolder.GetComponent<ShipBuilding>().DragStart(gameObject);
    }
}
