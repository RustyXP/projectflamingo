using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 Purpose: Holds the data about the opponent 
            (body sprite, face sprites, face position (if perspective is used), dialogue, emotional state)
 
 
 Usage:    
  
 */
public class OpponentManager : MonoBehaviour
{
    public bool DebugMode;
    
    public string name;

    public int id;

    public SpriteRenderer bodySR, faceSR;

    public Sprite bodySprite;

    public Sprite[] emotions;

    private int _currentEmotion;

    public string[] dialogue;

    public int endStateValueWin, endStateValueLose;
    
    public Text OpponentSpeech;
    WaitForSeconds waitDialogue = new WaitForSeconds(0.2f);

    public PlaySceneManager PSM;

    public int currentEmotion
    {
        get { return _currentEmotion; }
        set
        {
            faceSR.sprite = emotions[value];
            _currentEmotion = value;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        faceSR.sprite = emotions[currentEmotion];

        if (DebugMode)
        {
            name = "dad";
            currentEmotion = 3;
            dialogue = new string[5];
            dialogue[0] = "Aw shucks kid I love ya";
            dialogue[1] = "Hah I guess thats ok";
            dialogue[2] = "Neutral Feel";
            dialogue[3] = "I am mildly mad";
            dialogue[4] = "I am going to destroy everything you love";
        }
    }

    public void OpponentSpeak()
    {
        StartCoroutine(dialogueDisplayer(dialogue[currentEmotion]));
    }

    IEnumerator dialogueDisplayer(string splitDialogue)
    {
        string sr = "";
        string[] characters = new string[splitDialogue.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] += splitDialogue[i];
        }
        PSM.CharTalking = true;
        for (int i = 0; i < characters.Length; i++)
        {
            sr += splitDialogue[i];
            OpponentSpeech.text = sr;
            yield return waitDialogue;
        }
        PSM.CharTalking = false;
        PSM.currentState = 2;
    }
}
