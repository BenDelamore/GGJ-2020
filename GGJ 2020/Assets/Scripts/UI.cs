using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject core;

    public GameObject speedText;
   
    void Start()
    {
        
    }

    void Update()
    {
        speedText.GetComponent<TextMeshProUGUI>().text = "Speed " + core.GetComponent<Rigidbody2D>().velocity.magnitude + " ms<sup>-1</sup>";
    }
}
