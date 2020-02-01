using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform core;

    void Update() {
        transform.position = new Vector3(core.position.x, core.position.y, -10);
        //transform.rotation = core.rotation;
    }
}
