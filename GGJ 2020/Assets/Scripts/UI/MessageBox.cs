using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageBox : MonoBehaviour
{
    public bool showMessageBox;
    public string message;

    private bool isVisible;
    private float messageTimeLeft;

    private Text messageText;
    private CanvasGroup messageCanvas;

    void Start()
    {
        messageText = gameObject.transform.Find("MessageText").GetComponent<Text>();
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

    }

    public void SendMessage(string text, float duration)
    {

    }

    // Messagebox Animations and Transitions
    void Begin()
    {
        ShowBox();
        ShowMessage();
    }

    void End()
    {

    }

    void ShowBox()
    {

    }

    void ShowMessage()
    {

    }

    void HideBox()
    {

    }

    void HideMessage()
    {

    }

}
