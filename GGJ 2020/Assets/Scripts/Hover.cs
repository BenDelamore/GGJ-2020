using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{

    private GameObject hoverScriptHolder;

    private void Start() {
        hoverScriptHolder = transform.parent.gameObject;
    }

    private void OnMouseDrag() {
        hoverScriptHolder.GetComponent<ShipBuilding>().HoveringOver(gameObject);
    }
    
}
