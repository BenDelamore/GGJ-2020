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
    private float nameTutorialTimer = 0f;


    void Start()
    {
        mBox = FindObjectOfType<MessageBox>();
    }


    void Update()
    {
        // Name Tutorial
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

    public void StartNameTutorial()
    {
        if (nameTutState == NameTutorial.Start)
        {
            nameTutState = NameTutorial.EnteringName;
            Debug.Log("Starting name tutorial");
        }
    }
}
