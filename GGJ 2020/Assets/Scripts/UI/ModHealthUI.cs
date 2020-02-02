using System.Collections;
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
    public GameObject Module;

    void Start()
    {
        healthRing = gameObject.transform.Find("HealthRingFore").GetComponent<Image>();
        canvas = GetComponent<CanvasGroup>();
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        Hide();
    }


    void Update()
    {
        if (Module.GetComponent<moduleBehaviour>())
        {
            healthValue = Module.GetComponent<moduleBehaviour>().health;
        }
        healthRing.fillAmount = healthValue * 0.01f;

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
        canvas.DOKill(true);
        transform.DOKill(true);
        canvas.DOFade(1f, 0.3f);
        transform.DOScale(0.75f, 0.3f).SetEase(Ease.OutBack);

        Debug.Log("Showing Module Health");
    }

    void Hide()
    {
        canvas.DOKill(true);
        transform.DOKill(true);
        canvas.DOFade(0f, 0.3f);
        transform.DOScale(0f, 0.5f).SetEase(Ease.InQuad);

        Debug.Log("Hiding Module Health");
    }
}
