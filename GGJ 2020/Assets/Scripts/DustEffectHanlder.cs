﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffectHanlder : MonoBehaviour
{
    private ParticleSystem ps;
    public GameObject ship;
    private ShipScript shipMove;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        shipMove = ship.GetComponent<ShipScript>();

    }

    void Update()
    {
        var main = ps.main;
        var emission = ps.emission;
        main.startSpeed = -shipMove.velocityMag;
        emission.rateOverTime = Mathf.RoundToInt(shipMove.velocityMag/2f);
    }
}
