﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawnScript : MonoBehaviour
{
    public string playerName;
    private float seed;
    private int iSeed;
    private float density = 0.025f; // one item per 4 square units
    private float spawnSize = 100.0f;

    public GameObject Scrap;
    public GameObject Thruster;
    public GameObject Shield;
    public GameObject Gyroscope;
    public GameObject HullExpansion;

    // Start is called before the first frame update
    void Start()
    {
        // generates seed
        seed = 1.0f;
        string charactersLeft = playerName;
        while (charactersLeft.Length >= 1)
        {
            if (charactersLeft.Length >= 2)
            {
                int charInt = charactersLeft[0];
                int charInt2 = charactersLeft[1];
                seed += charInt;
                seed *= charInt2;
                charactersLeft = charactersLeft.Remove(0, 2);
            }
            else
            {
                int charInt = charactersLeft[0];
                seed += charInt;
                charactersLeft = charactersLeft.Remove(0, 1);
            }
        }
        iSeed = Mathf.FloorToInt(seed);
        float totalItemsToSpawn = spawnSize * spawnSize * density;
        for (int i = 0; i < Mathf.CeilToInt(totalItemsToSpawn); i++)
        {

            // if it can be divided by 4, it's an asteroid.
            // unless it can be divided by 5, then it's a thruster.
            // unless it can be divided by 7, then it's a shield.
            // unless it can be divided by 9, then it's a gyroscope.
            // unless it can be divided by 11, then it's a hull expansion.
            // if none are true, it's scrap

            if (i % 4 == 0)
            {
                // Asteroid
                if ((iSeed + i) % 4 == 0)
                {
                    // large asteroid
                }
            }
            else if (i % 5 == 0)
            {
                // Thruster
                Debug.Log("Spawning Thruster with id #" + i);
                GameObject newThruster = GameObject.Instantiate(Thruster);
                newThruster.transform.SetPositionAndRotation(GeneratePosition(i), Quaternion.Euler(0, 0, GenerateOrientation(i)));
            }
            else if (i % 7 == 0)
            {
                // Shield
                Debug.Log("Spawning Shield with id #" + i);
                GameObject newShield = GameObject.Instantiate(Shield);
                newShield.transform.SetPositionAndRotation(GeneratePosition(i), Quaternion.Euler(0, 0, GenerateOrientation(i)));
            }
            else if (i % 9 == 0)
            {
                // Gyroscope
            }
            else if (i % 11 == 0)
            {
                // Hull Expansion
            }
            else
            {
                // Scrap
                Debug.Log("Spawning Scrap with id #" + i);
                GameObject newScrap = GameObject.Instantiate(Scrap);
                newScrap.transform.SetPositionAndRotation(GeneratePosition(i), Quaternion.Euler(0, 0, GenerateOrientation(i)));
            }
        }
    }

    Vector2 GeneratePosition(int item)
    {
        // takes in item number, uses seed to generate position data
        float x = (seed % 100 * Mathf.Pow(item, 3.0f) * 7378 % spawnSize) - (spawnSize * 0.5f);
        float y = (seed % 100 * Mathf.Pow(item, 2.0f) * 29 % spawnSize) - (spawnSize * 0.5f);

        return new Vector2(x, y);
    }

    float GenerateOrientation(int item)
    {
        return seed * Mathf.Pow(item, 3.0f) % 360.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}