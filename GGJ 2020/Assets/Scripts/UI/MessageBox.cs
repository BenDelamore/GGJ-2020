using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageBox : MonoBehaviour
{
    public bool showMessageBox;
    public string message;

    private bool isVisible = true;
    public float messageTimeLeft;

    private CanvasGroup canvas;
    private RectTransform rTransform;
    private Text messageText;
    private CanvasGroup messageCanvas;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        rTransform = GetComponent<RectTransform>();
        messageText = gameObject.transform.Find("MessageText").GetComponent<Text>();
        messageCanvas = gameObject.transform.Find("MessageText").GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (showMessageBox && !isVisible)
        {
            Begin();
            isVisible = true;
        }

        if (!showMessageBox && isVisible)
        {
            End();
            isVisible = false;
        }

        if (messageTimeLeft > 0)
        {
            showMessageBox = true;
            messageTimeLeft -= Time.unscaledDeltaTime;
        }
        else
        {
            showMessageBox = false;
        }
    }

    public void SendMessage(string text, float duration)
    {
        messageText.text = text;
        messageTimeLeft = duration;
    }

    // Messagebox Animations and Transitions
    private void Begin()
    {
        ShowBox();
        ShowMessage();
        Debug.Log("Showing Messagebox");
    }

    private void End()
    {
        HideBox();
        HideMessage();
        Debug.Log("Hiding Messagebox");
    }

    private void ShowBox()
    {
        rTransform.DOKill(true);
        canvas.DOKill(true);
        canvas.DOFade(1f, 0.1f);
        rTransform.DOSizeDelta(new Vector2(670f, 128f), 0.5f).SetEase(Ease.OutQuint);
    }

    private void ShowMessage()
    {

    }

    private void HideBox()
    {
        rTransform.DOKill(true);
        canvas.DOKill(true);
        canvas.DOFade(0f, 0.25f).SetEase(Ease.OutQuad);
        rTransform.DOSizeDelta(new Vector2(150f, 128f), 0.4f).SetEase(Ease.OutQuad);
    }

    private void HideMessage()
    {

    }

}
