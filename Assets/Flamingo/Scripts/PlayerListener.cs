using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Affdex;

public class PlayerListener : ImageResultsListener
{
    public bool enableSearch;
    
    public float timer = 0.0f;
    public float cutOffTime = 3.0f;
    public bool runTimer = false;
    public string emotion = "";
    
    

    public int emotionIndex = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enableSearch)
        {
            if (runTimer)
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            enableSearch = false;
        }
    }

    public override void onFaceFound(float timestamp, int faceId)
    {
        Debug.Log("Found player face");
    }
    public override void onFaceLost(float timestamp, int faceId)
    {
        Debug.Log("Lost player face");
    }
    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (faces.Count > 0)
        {
            //If the player is happy start the timer
            if (faces[faces.Count - 1].Emotions[Affdex.Emotions.Joy] > 90.0f && !runTimer)
            {
                timer = 0.0f;
                emotion = "happy";
                runTimer = true;
            }
            //Or if they are shocked
            else if(faces[faces.Count - 1].Emotions[Affdex.Emotions.Surprise] > 90.0f && !runTimer)
            {
                timer = 0.0f;
                emotion = "shocked";
                runTimer = true;
            }
            //Or if they are not interested
            else if(faces[faces.Count - 1].Expressions[Affdex.Expressions.Attention] < 50.0f && !runTimer)
            {
                timer = 0.0f;
                runTimer = true;
                emotion = "low interest";
            }
            //Or if they are angry
            else if(faces[faces.Count - 1].Expressions[Affdex.Expressions.BrowFurrow] > 90.0f && faces[faces.Count - 1].Expressions[Affdex.Expressions.Smile] < 20.0f && !runTimer)
            {
                timer = 0.0f;
                emotion = "angry";
                runTimer = true;
            }
            //If they display the emotion for a fixed time do stuff
            else if(timer >= cutOffTime)
            {
                runTimer = false;
                timer = 0.0f;
                Debug.Log(emotion);
                if (emotion == "happy")
                {
                    emotionIndex = 0;
                }else if (emotion == "shocked")
                {
                    emotionIndex = 1;

                }else if (emotion == "low interest")
                {
                    emotionIndex = 2;

                }else if (emotion == "angry")
                {
                    emotionIndex = 3;

                }
                //Do stuff
            }
            //If they aren't showin any emotions we care about reset the timer and emotions
            else if(faces[faces.Count - 1].Emotions[Affdex.Emotions.Joy] < 
                    90.0f && faces[faces.Count - 1].Emotions[Affdex.Emotions.Surprise] < 
                    90.0f && faces[faces.Count - 1].Expressions[Affdex.Expressions.Attention] > 
                    50.0f && faces[faces.Count - 1].Expressions[Affdex.Expressions.BrowFurrow] < 90.0f)
            {
                runTimer = false;
                emotion = "";
                emotionIndex = -1;
            }
        }
    }
}
