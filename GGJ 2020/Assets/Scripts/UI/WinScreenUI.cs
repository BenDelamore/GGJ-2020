using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinScreenUI : MonoBehaviour
{
    public bool showScreen;
    private bool isVisible;
    public bool postGame;

    private ShipScript ship;

    private CanvasGroup canvas;

    void Start()
    {
        ship = FindObjectOfType<ShipScript>();
        canvas = GetComponent<CanvasGroup>();
    }


    void Update()
    {
        if (ship.warpCoreCount >= 4 && !postGame)
        {
            showScreen = true;
        }

        if (showScreen && !isVisible)
        {
            Show();
            isVisible = true;
        }

        if (!showScreen && isVisible)
        {
            Hide();
            isVisible = false;
        }
    }

    public void Continue()
    {
        postGame = true;
        //Hide();
        //Hide();
        showScreen = false;
        Debug.Log("Continue");
    }

    void Show()
    {
        canvas.DOKill(true);
        //transform.DOKill(true);
        canvas.DOFade(1f, 0.2f);
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
        //transform.DOScale(0.75f, 0.3f).SetEase(Ease.OutBack);
    }

    void Hide()
    {
        canvas.DOKill(true);
        //transform.DOKill(true);
        canvas.DOFade(0f, 0.2f);
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
        //transform.DOScale(0f, 0.5f).SetEase(Ease.InQuad);
    }
}
