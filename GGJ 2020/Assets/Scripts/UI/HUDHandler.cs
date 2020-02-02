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
        shipHealthText = shipHealthModule.GetComponent<Text>();
    }

    void Update()
    {
        shipSpeedText.text = shipScript.velocityMag.ToString("0000") + " m/s";
        if (shipScript.health < 20.0f)
        {
            Color newColour = Color.white;
            newColour.r = 0.949f;
            newColour.g = 0.051f;
            newColour.b = 0.274f;
            shipHealthText.color = newColour;
        }
        else
        {
            shipHealthText.color = Color.white;
        }
        shipHealthText.text = shipScript.health.ToString("0") + "%";
        string speedText = shipScript.velocityMag.ToString("0000") + " ms<sup>-1</sup>";

        Debug.Log(GlobalData.seed);


    }
}
