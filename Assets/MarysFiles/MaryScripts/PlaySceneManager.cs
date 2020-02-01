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
    private int _currentState;
    public int currentState
    {
        get { return _currentState; }
        set
        {
            _currentState = value;
            
        }
    }

    public Text OpponentSpeech;
    public bool opponentTalking;
    
    WaitForSeconds waitDialogue = new WaitForSeconds(0.2f);
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sceneEnder()
    {
        
    }

    IEnumerator dialogueDisplayer(string splitDialogue)
    {
        string sr = "";
        string[] characters = new string[splitDialogue.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] += splitDialogue[i];
        }
        opponentTalking = true;
        for (int i = 0; i < characters.Length; i++)
        {
            sr += splitDialogue[i];
            OpponentSpeech.text = sr;
            yield return waitDialogue;
        }

        opponentTalking = false;
    }
}
