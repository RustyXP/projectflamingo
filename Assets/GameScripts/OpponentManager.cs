﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 How it works: Holds the data about the opponent 
            (body sprite, face sprites, face position (if perspective is used), dialogue, emotional state)
 On Scene Start:
 Needs to have: a body sprite, a name, an array of dialogue, a set of facial expressions, 
 position for the face to be (for perspective purposes), the emotion that is wanted
 
 Acting:
 1. On Start, the player's emotion will be a neutral 3. 
 2. When called upon by PlaySceneManager, will display the dialogue to the UI.
 3. When the dialogue is completed, tells PlaySceneManager that CharTalking = false. 
  
  Reacting:
  1. After the player "locks in" an emotion, it will increase/decrease the Opponent's emotion by 1 
  (let's discuss if we want it to go through playscenemanger or not)
  2. When the emotion is changed, change the facial expression as well. 
  3. The next time the Opponent speaks, it will use the dialogue associated with this level of emotion.
  
  Emotional States: 3 = neutral, 0 = good, 6 = bad
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
            if (value == 1)
            {
                faceSR.sprite = emotions[0];
            }else if (value == 3)
            {
                faceSR.sprite = emotions[1];
            }else if (value == 5)
            {
                faceSR.sprite = emotions[2];
            }
            _currentEmotion = value;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentEmotion = 3;

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
