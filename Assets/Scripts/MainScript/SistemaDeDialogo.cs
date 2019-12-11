using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //using the API to enable the modifying of the UI

/// <summary>
/// code inspired by this: https://www.youtube.com/watch?v=nnxZVU0qe5I&list=PLGSox0FgA5B7mApF1vhbspLj5NpzKedU6&index=1
/// Dialogue
/// </summary>

public class SistemaDeDialogo : MonoBehaviour
{
    public static SistemaDeDialogo instance; //with this we can use this scripts in other scenes and scripts
    public ELEMENTS elements; //we use the same name as the class, so we can call it over by using its constructor and also know that this store a point of reference in that clas


     void Awake()
    {
        instance = this; //this assure that this variable will be only use in this scene. I learned this keywork on Javascript 
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Di(string speech, string speaker = "") //to show on the screen    //also, we created this variables in the methods so it helped us saving memory (one of the reasons with have to watch that video)
    {
        StopSpeaking();

        speaking = StartCoroutine(Speaking(speech, false, speaker));
    }

    public void DiAdd(string speech, string speaker = "") //this is for switching between additive and normla text
    {
        StopSpeaking();
        dialogueText.text = targetSpeech;
       speaking =  StartCoroutine(Speaking(speech, true, speaker));
    }

    public void StopSpeaking() //this method is for stoping the dialog in Di
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking); //this stops the couritine of the dialog in favor of other
        }
        speaking = null; //with this the the actua dialog is deleted
    }


    //this works a little be different than normal arrays and also this is to know that someone is speaking
    public bool isSpeaking { get { return speaking != null; } } //to delete the previous dialog that Di has
    [HideInInspector] public bool isWaitingForUserInput = false; //this attrubute of Unity allow us to although the code is public, is does not show on the Inspector

    string targetSpeech = "";

    Coroutine speaking = null;  // so that Speaking don't gave the value until it is given

    IEnumerator Speaking(string speech, bool additive, string speaker = "")  //for showing text // also the same reason as on line 22
    {
        dialoguePanel.SetActive(true);
        targetSpeech = speech;
        if (!additive)
            dialogueText.text = "";
        else
            targetSpeech = dialogueText.text + targetSpeech;  //for changing between additive and normal text

        dialogueText.text = "";
        speakerNameText.text = DetermineSpeaker(speaker);
        isWaitingForUserInput = false; //when the text has not finished and don-'t need to be cut out

        while (dialogueText.text != targetSpeech) //this make one charachter for frame to talk
        {
            dialogueText.text += targetSpeech[dialogueText.text.Length];  //to find the new character
            yield return new WaitForEndOfFrame();

        }

        isWaitingForUserInput = true; //for when the text is finished
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopSpeaking(); //if input is detected it will be deleted


    }

     string DetermineSpeaker(string d) //this determine who is going to talk
    {
        string retVal = speakerNameText.text;  //default return  is the current name
        if (d != speakerNameText.text && d != "")
            retVal = (d.ToLower().Contains("narrator")) ? "" : d;

        return retVal;
    }

    public void Close() //close the dialoguePanel Object and stops the dialogue
    {
        StopSpeaking();
        dialoguePanel.SetActive(false);
    }

  
    [System.Serializable] //this is a Unity attribute. Although we prefer using Range, the video that we got our inspiration for recommended this.

    //this class is inspired by the video  Let's make a visual Novel series 
    // here, again, is the link to it https://www.youtube.com/watch?v=nnxZVU0qe5I&list=PLGSox0FgA5B7mApF1vhbspLj5NpzKedU6&index=1
    public class ELEMENTS
    {
        ////this where the screen things will be write over and help keeping it organize
        ///this is for UI use only
        public GameObject dialoguePanel;
        public Text speakerNameText;
        public Text dialogueText;
    }

    //this change and return the dialogs, so we can simply
    public GameObject dialoguePanel { get { return elements.dialoguePanel; } }
    public Text speakerNameText { get { return elements.speakerNameText; } }
    public Text dialogueText { get { return elements.dialogueText; } }




}
