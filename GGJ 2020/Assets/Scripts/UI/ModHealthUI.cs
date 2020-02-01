﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ModHealthUI : MonoBehaviour
{
    public bool showHealth;
    public float healthValue = 100f;
    private bool healthVisible = false;
    private Image healthRing;
    private CanvasGroup canvas;

    void Start()
    {
        healthRing = gameObject.transform.Find("HealthRingFore").GetComponent<Image>();
        canvas = GetComponent<CanvasGroup>();
        Hide();
    }


    void Update()
    {
        healthRing.fillAmount = healthValue / 100f;

        if (showHealth && !healthVisible)
        {
            Show();
            healthVisible = true;
        }

        if (!showHealth && healthVisible)
        {
            Hide();
            healthVisible = false;
        }

    }

    void Show()
    {
        canvas.DOFade(1f, 0.3f);
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

        Debug.Log("Showing Module Health");
    }

    void Hide()
    {
        canvas.DOKill(true);
        canvas.DOFade(0f, 0.3f);
        transform.DOScale(0f, 0.5f).SetEase(Ease.InQuad);

        Debug.Log("Hiding Module Health");
    }
}