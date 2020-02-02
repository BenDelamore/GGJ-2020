using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject core;
    public float scrollSpeed;

    private Renderer renderer;
    private Vector2 savedOffset;

    void Start() {
        renderer = GetComponent<Renderer>();
    }

    void Update() {
        transform.position = core.transform.position + new Vector3(0,0,10);

        float x = core.transform.position.x / 100;
        float y = core.transform.position.y / 100;
        Vector2 offset = new Vector2(x, y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
