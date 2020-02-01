using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressBar : MonoBehaviour
{
    public int maxBits = 4;
    public int collectedBits;
    public GameObject bitIndicatorPrefab;

    private GameObject[] bitIndicator;
    private Image[] bitSprite;
    public Sprite spriteOn;
    public Sprite spriteOff;

    void Start()
    {
        bitIndicator = new GameObject[maxBits];
        bitSprite = new Image[maxBits];

        for (int i = 0; i < maxBits; i++)
        {
            bitIndicator[i] = Instantiate(bitIndicatorPrefab);
            bitIndicator[i].transform.SetParent(this.transform);
            bitIndicator[i].transform.localPosition = new Vector3(0.0f + (i * 52f), 0.0f, 0.0f);
            bitIndicator[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            bitSprite[i] = bitIndicator[i].GetComponent<Image>();
        }
    }


    void Update()
    {
        collectedBits = GameObject.Find("Core").GetComponent<ShipScript>().warpCoreCount;
        collectedBits = Mathf.Clamp(collectedBits, 0, maxBits);

        for (int i = 0; i < maxBits; i++)
        {
            if (i < collectedBits)
            {
                bitSprite[i].sprite = spriteOn;
            }
            else
            {
                bitSprite[i].sprite = spriteOff;
            }
        }
    }
}
