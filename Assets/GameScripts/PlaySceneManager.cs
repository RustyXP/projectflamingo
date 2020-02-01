using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 Purpose:
 
 Usage: 
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
                    break;
                case 2: 
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
        
    }

    void sceneEnder()
    {
        
    }

    
    

    
}
