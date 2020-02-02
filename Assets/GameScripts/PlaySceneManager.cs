using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 How it works:
 Manages the general state of the scene. Basically a state machine using CurrentState.
 
 1. Kicks off the scene on Start()
 2. Lets the Opponent talk first by setting currentState to 1
 3. Lets the Player emote by setting currentState to 2
 
Win State: When the Opponent's emotion = 0, Win.
Lose State: When the Opponent's emotion = 6, lose.

On win/lose, displays text that lets player know they won or lost, then switch scene to the dining table scene
 */


public class PlaySceneManager : MonoBehaviour
{
    //0 = nobody is doing anything, 1 = opponent speaking, 2 = player emoting;
    private int _currentState;
    public int currentState
    {
        get { return _currentState; }
        set
        {
            _currentState = value;
            switch (value)
            {
                case 0: 
                    break;
                case 1: 
                    OM.OpponentSpeak();
                    PlayerReactionEnabled = false;
                    PL.enableSearch = false;
                    break;
                case 2:
                    CharTalking = false;
                    PlayerReactionEnabled = true;
                    PL.enableSearch = true;
                    break;
            }
        }
    }

    public bool PlayerReactionEnabled = false;
    public bool CharTalking = false;

    public OpponentManager OM;

    public PlayerListener PL;

    public Text emotionLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == 2)
        {
            if (PL.emotionIndex != -1)
            {
                PlayerReactionEnabled = false;
                PL.enableSearch = false;
                if (PL.emotionIndex == OM.desiredEmotion)
                {
                    OM.currentEmotion++;
                }
                else
                {
                    OM.currentEmotion--;
                }
            }
        }
    }

    public void sceneLost()
    {
        StartCoroutine(changeScene(false));
    }

    public void sceneWon()
    {
        StartCoroutine(changeScene(true));

    }

    public IEnumerator changeScene(bool win)
    {
        WaitForSeconds wait = new WaitForSeconds(2f);

        yield return wait;

        if (win)
        {
            GameManager.instance.YouWin();
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }
    
    
}
