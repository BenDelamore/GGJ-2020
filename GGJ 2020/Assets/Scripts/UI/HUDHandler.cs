using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUDHandler : MonoBehaviour
{
    public GameObject ship;
    private ShipScript shipScript;

    private float shipHealth;
    public GameObject shipHealthModule;
    private Text shipHealthText;

    private float shipSpeed;
    public GameObject shipSpeedModule;
    private Text shipSpeedText;

    private float timeLeft;
    public GameObject timeLeftModule;
    private Text timeLeftText;

    private float distance;
    public GameObject distanceModule;
    private Text distanceText;

    void Start()
    {
        shipScript = ship.GetComponent<ShipScript>();
        shipSpeedText = shipSpeedModule.GetComponent<Text>();
    }

    void Update()
    {
        shipSpeedText.text = shipScript.velocityMag.ToString("0000") + " m/s";
        string speedText = shipScript.velocityMag.ToString("0000") + " ms<sup>-1</sup>";
        //Debug.Log();
    }
}
