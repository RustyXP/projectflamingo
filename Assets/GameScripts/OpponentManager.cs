using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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

    //0 = Dad, 1 = mom
    public int id;

    public SpriteRenderer bodySR, faceSR;

    public Sprite[] bodySprite;

    public Sprite[] emotions;

    public int desiredEmotion;
    
    public string[] dialogue;

    public int endStateValueWin, endStateValueLose;
    
    public Text OpponentSpeech;
    WaitForSeconds waitDialogue = new WaitForSeconds(0.05f);

    public PlaySceneManager PSM;

    public Image[] EmotionPellets;
    public Sprite filledPellet, emptyPellet;

    public AnimatorController[] anims;
    public Animator anim;

    public GameObject greyOut;
    

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
                if (i < currentEmotion)
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
        
        if (id == 0)
        {
            name = "dad";
            dialogue = new string[7];

            //anim = anims[0];

            //happiness
            desiredEmotion = 0;
            
            dialogue[6] = "I appreciate you being so… present. You see, things have been incredibly difficult.";
            dialogue[5] = "We're really trying to work together, though… I believe we can do this.";
            dialogue[4] = "I appreciate you being so… present. You see, things have been incredibly difficult.";
            dialogue[3] = "You know your mother and I are having some… disagreements, right?";
            dialogue[2] = "I'm trying to talk to you. Can't you see that?";
            dialogue[1] = "Enough. Listen to me while I'm talking to you.";
            dialogue[0] = "I am through with not being listened to in my own house!";

            endStateValueLose = 0;
            endStateValueWin = 7;
        }else if (id == 1)
        {
            name = "mom";
            dialogue = new string[7];

            //anim = anims[1];

            //Change emotion
            desiredEmotion = 0;
            
            dialogue[6] = "I appreciate you being so… present. You see, things have been incredibly difficult.";
            dialogue[5] = "We're really trying to work together, though… I believe we can do this.";
            dialogue[4] = "I appreciate you being so… present. You see, things have been incredibly difficult.";
            dialogue[3] = "You know your mother and I are having some… disagreements, right?";
            dialogue[2] = "I'm trying to talk to you. Can't you see that?";
            dialogue[1] = "Enough. Listen to me while I'm talking to you.";
            dialogue[0] = "I am through with not being listened to in my own house!";

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
        greyOut.SetActive(true);
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
        anim.SetBool("IsTalking", false);

        int bufferTime = 2;
        for (int i = 0; i < bufferTime; i++)
        {
            yield return new WaitForSeconds(1.2f);
        }
        PSM.CharTalking = false;
        PSM.currentState = 2;
        greyOut.SetActive(false);
    }
}
