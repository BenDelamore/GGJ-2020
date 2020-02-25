using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    private MessageBox mBox;
    private CanvasGroup hudCanvas;

    public enum NameTutorial
    {
        Start,
        EnteringName,
        ShowingMessage,
        End
    }
    public NameTutorial nameTutState;
    private float nameTutorialTimer = 0.0f;

    public enum ModTutorial
    {
        Start,
        Trigger,
        ShowingMessage,
        Connect,
        End
    }
    public ModTutorial modTutState;
    private float modTutTimer = 0.0f;

    public enum ControlsTutorial
    {
        Start,
        Trigger,
        ShowingMessage,
        End
    }
    public ControlsTutorial controlTutState;
    private float controlTutTimer;

    public enum WinTutorial
    {
        Start,
        Trigger,
        ShowingMessage,
        End
    }
    public WinTutorial winTutState;
    private float winTutTimer = 0.0f;

    void Start()
    {
        mBox = FindObjectOfType<MessageBox>();
        //controlTutState = ControlsTutorial.End;

        if (GlobalData.curScene != "TitleScreen")
        {
            hudCanvas = GameObject.Find("HUD").GetComponent<CanvasGroup>();
        }
    }


    void Update()
    {
        // Name Tutorial
        if (GlobalData.curScene == "TitleScreen")
        {
            if (nameTutState != NameTutorial.Start && nameTutState != NameTutorial.End)
            {
                nameTutorialTimer += Time.unscaledDeltaTime;
            }
            if (nameTutorialTimer >= 0.5f && nameTutState == NameTutorial.EnteringName)
            {
                mBox.SendMessage("Your name will be used as a seed for the game's generation!", 5.0f);
                nameTutState = NameTutorial.ShowingMessage;
            }
            if (nameTutorialTimer >= 8.0f && nameTutState == NameTutorial.ShowingMessage)
            {
                nameTutState = NameTutorial.End;
            }
        }


        if (GlobalData.curScene != "TitleScreen")
        {
            // Drag Object Tutorial
            modTutTimer += Time.unscaledDeltaTime;
            if (modTutTimer >= 2.0f)
            {
                StartModTutorial();
            }
            if (modTutState == ModTutorial.Trigger)
            {
                mBox.SendMessage("Drag and drop components onto your ship to connect them.", 30f);
                modTutState = ModTutorial.ShowingMessage;
            }
            if (modTutState == ModTutorial.Connect)
            {
                mBox.SendMessage("Drag and drop components onto your ship to connect them.", 0.0f);
                modTutState = ModTutorial.End;
            }

            // Controls Tutorial
            if (modTutState == ModTutorial.End)
            {
                controlTutTimer += Time.unscaledDeltaTime;
            }
            if (controlTutTimer >= 1.5f)
            {
                //StartControlsTutorial();
            }
            if (controlTutState == ControlsTutorial.Trigger)
            {
                mBox.SendMessage("Press W to boost.\nPress Q or E to rotate.", 4.0f);
                controlTutState = ControlsTutorial.ShowingMessage;
            }
            if (controlTutTimer >= 5.5f)
            {
                controlTutState = ControlsTutorial.End;
            }

            // Win Tutorial
            if (modTutState == ModTutorial.End)
            {
                winTutTimer += Time.unscaledDeltaTime;
            }
            if (winTutTimer >= 1.5f)
            {
                StartWinTutorial();
            }
            if (winTutState == WinTutorial.Trigger)
            {
                string wordNum = GlobalData.NumberToWords(FindObjectOfType<ProgressBar>().maxBits);
                mBox.SendMessage("Power up your ship by collecting " + wordNum + " gold Warp Cores.", 4.0f);
                Invoke("ShowHud", 1.0f);
                winTutState = WinTutorial.ShowingMessage;
            }
            if (controlTutTimer >= 5.5f)
            {
                winTutState = WinTutorial.End;
            }
        }

    }

    public void StartNameTutorial()
    {
        if (nameTutState == NameTutorial.Start)
        {
            nameTutState = NameTutorial.EnteringName;
            Debug.Log("Starting name tutorial");
        }
    }

    public void StartModTutorial()
    {
        if (modTutState == ModTutorial.Start)
        {
            modTutState = ModTutorial.Trigger;
            Debug.Log("Starting Modding Tutorial");
        }
    }
    public void EndModTutorial()
    {
        if (modTutState != ModTutorial.End)
        {
            modTutState = ModTutorial.Connect;
        }
    }

    public void StartControlsTutorial()
    {
        if (controlTutState == ControlsTutorial.Start)
        {
            controlTutState = ControlsTutorial.Trigger;
        }
    }

    public void StartWinTutorial()
    {
        if (winTutState == WinTutorial.Start)
        {
            winTutState = WinTutorial.Trigger;
        }
    }

    private void ShowHud()
    {
        hudCanvas.DOFade(1.0f, 1.0f);
    }
}
