using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawnScript : MonoBehaviour
{
    public string playerName;
    private float seed;
    private int iSeed = 0;
    private float density = 0.025f; // one item per 8 square units
    private float spawnSize = 100.0f;

    public GameObject Scrap;
    public GameObject Thruster;
    public GameObject Shield;
    public GameObject Gyroscope;
    public GameObject WarpCore;

    // Start is called before the first frame update
    void Start()
    {
        // generates seed
        seed = 1.0f;
        string charactersLeft = GlobalData.seed;
        while (charactersLeft.Length >= 1)
        {
            int charInt = charactersLeft[0];
            seed *= charInt;
            charactersLeft = charactersLeft.Remove(0, 1);
        }
        iSeed = Mathf.RoundToInt(seed % 100000);
        Random.InitState(iSeed);
        float totalItemsToSpawn = spawnSize * spawnSize * density;
        for (int i = 0; i < Mathf.CeilToInt(totalItemsToSpawn); i++)
        {
            if (i % 5 == 0)
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
                Debug.Log("Spawning Gyroscope with id #" + i);
                GameObject newGyroscope = GameObject.Instantiate(Gyroscope);
                newGyroscope.transform.SetPositionAndRotation(GeneratePosition(i), Quaternion.Euler(0, 0, GenerateOrientation(i)));
            }
            else if (i % 11 == 0)
            {
                // Warp Core
                Debug.Log("Spawning Warp Core with id #" + i);
                GameObject newWarpCore = GameObject.Instantiate(WarpCore);
                newWarpCore.transform.SetPositionAndRotation(GeneratePosition(i), Quaternion.Euler(0, 0, GenerateOrientation(i)));
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
        float min = spawnSize * -0.5f;
        float max = spawnSize * 0.5f;
        return new Vector2(Random.Range(min, max), Random.Range(min, max));
    }

    float GenerateOrientation(int item)
    {
        return Random.Range(0.0f, 360.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
