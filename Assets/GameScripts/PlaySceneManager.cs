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
                    break;
                case 2:
                    CharTalking = false;
                    PlayerReactionEnabled = true;
                    break;
            }
        }
    }

    public bool PlayerReactionEnabled = false;
    public bool CharTalking = false;

    public OpponentManager OM;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = 0;
        OM = FindObjectOfType<OpponentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentState = 1;
        }
        
    }

    void sceneEnder()
    {
        
    }

    
    

    
}
