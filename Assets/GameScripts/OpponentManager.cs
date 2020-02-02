using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 How it works: Holds the data about the opponent 
            (body sprite, face sprites, face position (if perspective is used), dialogue, emotional state)
 On Scene Start:
 Needs to have: a body sprite, a name, an array of dialogue, a set of facial expressions, 
 position for the face to be (for perspective purposes), the emotion that is wanted
 value for endStateWin, endStateLose
 
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

    public int desiredEmotion;
    
    public string[] dialogue;

    public int endStateValueWin, endStateValueLose;
    
    public Text OpponentSpeech;
    WaitForSeconds waitDialogue = new WaitForSeconds(0.05f);

    public PlaySceneManager PSM;

    public Image[] EmotionPellets;
    public Sprite filledPellet, emptyPellet;

    public Animator anim;
    
    
    private int _currentEmotion;
    public int currentEmotion
    {
        get { return _currentEmotion; }
        set
        {
            Debug.Log(value + "CE");
            anim.SetInteger("Emotion", value);
            
            if (value == 1)
            {
                faceSR.sprite = emotions[2];
            }else if (value == 3)
            {
                faceSR.sprite = emotions[1];
            }else if (value == 5)
            {
                faceSR.sprite = emotions[0];
            }else if (value == endStateValueLose)
            {
                PSM.sceneLost();
            }
            else if (value == endStateValueWin)
            {
                PSM.sceneWon();
            }
            
            
            for (int i = 0; i < EmotionPellets.Length; i++)
            {
                if (i-1 < currentEmotion)
                {
                    EmotionPellets[i].sprite = filledPellet;
                }

                else
                {
                    EmotionPellets[i].sprite = emptyPellet;
                }
            }

            _currentEmotion = value;
            Debug.Log(name);
            
            PSM.currentState = 1;

        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        if (DebugMode)
        {
            name = "dad";
            dialogue = new string[7];

            desiredEmotion = 0;
            
            dialogue[6] = "Aw shucks kid I love ya";
            dialogue[5] = "I will not disown you";
            dialogue[4] = "Hah I guess thats ok";
            dialogue[3] = "Neutral Feel";
            dialogue[2] = "I am mildly mad";
            dialogue[1] = "You disappoint your ancestors";
            dialogue[0] = "I am going to destroy everything you love";

            endStateValueLose = 0;
            endStateValueWin = 7;
        }
        
        
        currentEmotion = 3;
    }

    public void OpponentSpeak()
    {
        Debug.Log(currentEmotion);
        StartCoroutine(dialogueDisplayer(dialogue[currentEmotion]));
    }

    IEnumerator dialogueDisplayer(string splitDialogue)
    {
        anim.SetBool("IsTalking", true);
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
        anim.SetBool("IsTalking", false);

    }
}
