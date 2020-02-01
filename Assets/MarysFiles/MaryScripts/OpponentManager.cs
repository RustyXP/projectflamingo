using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Purpose: Holds the data about the opponent 
            (body sprite, face sprites, face position (if perspective is used), dialogue, emotional state)
 
 
 Usage:    
  
 */
public class OpponentManager : MonoBehaviour
{
    
    public string name;

    public int id;

    public SpriteRenderer bodySR, faceSR;

    public Sprite bodySprite;

    public Sprite[] emotions;

    private int _currentEmotion;

    public string[] dialogue;
    public ArrayList splitDialogue;

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
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
