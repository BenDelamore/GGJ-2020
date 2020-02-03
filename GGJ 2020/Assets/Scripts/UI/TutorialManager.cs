using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private MessageBox mBox;

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

    void Start()
    {
        mBox = FindObjectOfType<MessageBox>();
    }


    void Update()
    {
        // Name Tutorial
        if (GlobalData.curScene == "TitleScreen")
        {
            if (nameTutState != NameTutorial.Start && nameTutState != NameTutorial.End)
            {
                nameTutorialTimer += Time.deltaTime;
            }
            if (nameTutorialTimer >= 3.0f && nameTutState == NameTutorial.EnteringName)
            {
                mBox.SendMessage("Your name will be used as a seed for the game's generation!", 5.0f);
                Debug.Log("Testing message");
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
            modTutTimer += Time.deltaTime;
            if (modTutTimer >= 2.5f)
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
                controlTutTimer += Time.deltaTime;
            }
            if (controlTutTimer >= 1.5f)
            {
                StartControlsTutorial();
            }
            if (controlTutState == ControlsTutorial.Trigger)
            {
                mBox.SendMessage("Press W to boost.\nPress Q and E to rotate.", 5.0f);
                controlTutState = ControlsTutorial.ShowingMessage;
            }
            if (controlTutTimer >= 5.5f)
            {
                controlTutState = ControlsTutorial.End;
            }
        }


        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            EndModTutorial();
        }
        */
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
}
