using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUDHandler : MonoBehaviour
{
    public GameObject ship;

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
        
    }

    void Update()
    {
        
    }
}
