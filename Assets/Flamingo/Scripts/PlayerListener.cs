using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Affdex;
using UnityEngine.UI;

public class PlayerListener : ImageResultsListener
{
    public bool enableSearch;

    public float timer = 0.0f;
    public float cutOffTime = 3.0f;
    public bool runTimer = false;
    public string emotion = "";

    public Text uiOutput;

    private bool label;
    public int emotionIndex = -1;

    public Image loadBar;

    public Color grey, midGreen, okGreen;

    // Start is called before the first frame update
    void Start()
    {
        if (uiOutput != null)
        {
            label = true;
        }

        loadBar.type = Image.Type.Filled;
        loadBar.fillMethod = 0;
        loadBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableSearch)
        {
            if (runTimer)
            {
                timer += Time.deltaTime;
                //Debug.Log(loadBar.fillAmount);
                loadBar.fillAmount = timer / cutOffTime;
                loadBar.color = Color.Lerp(grey, midGreen, timer / cutOffTime);
            }
            else
            {
                if (loadBar.fillAmount > 0)
                {
                    loadBar.fillAmount -= 0.1f;
                }
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
                if (label)
                {
                    uiOutput.text = "Happy";
                }

            }
            //Or if they are shocked
            else if (faces[faces.Count - 1].Emotions[Affdex.Emotions.Surprise] > 90.0f && !runTimer)
            {
                timer = 0.0f;
                emotion = "shocked";
                runTimer = true;
                if (label)
                {
                    uiOutput.text = "Shocked";
                }
            }
            //If they do a kissing face they are showing love
            else if (faces[faces.Count - 1].Expressions[Affdex.Expressions.LipPucker] > 85.0f && !runTimer)
            {
                timer = 0.0f;
                emotion = "affection";
                runTimer = true;
                if (label)
                {
                    uiOutput.text = "Affection";
                }
            }
            //Sad is set to be really sensetive
            else if (faces[faces.Count - 1].Expressions[Affdex.Expressions.LipCornerDepressor] > 5.0f && !runTimer)
            {
                timer = 3.0f;
                emotion = "sad";
                runTimer = true;
                if (label)
                {
                    uiOutput.text = "Sad";
                }
            }
            //Or if they are not interested
            else if (faces[faces.Count - 1].Expressions[Affdex.Expressions.Attention] < 50.0f && !runTimer)
            {
                timer = 0.0f;
                runTimer = true;
                emotion = "low interest";
                if (label)
                {
                    uiOutput.text = "Uninterested";
                }
            }
            //Or if they are angry
            else if (faces[faces.Count - 1].Expressions[Affdex.Expressions.BrowFurrow] > 90.0f && faces[faces.Count - 1].Expressions[Affdex.Expressions.Smile] < 20.0f && !runTimer)
            {
                timer = 0.0f;
                emotion = "angry";
                runTimer = true;
                if (label)
                {
                    uiOutput.text = "Angry";
                }
            }
            //If they display the emotion for a fixed time do stuff
            else if (timer >= cutOffTime)
            {
                runTimer = false;
                timer = 0.0f;
                Debug.Log(emotion);
                if (emotion == "happy")
                {
                    emotionIndex = 0;
                }
                else if (emotion == "shocked")
                {
                    emotionIndex = 1;

                }
                else if (emotion == "low interest")
                {
                    emotionIndex = 2;

                }
                else if (emotion == "angry")
                {
                    emotionIndex = 3;
                }
                else if (emotion == "sad")
                {
                    emotionIndex = 4;
                }
                else if (emotion == "affection")
                {
                    emotionIndex = 5;
                }

                loadBar.fillAmount = 0;
                //Do stuff
            }
            //If they aren't showin any emotions we care about reset the timer and emotions
            else if (faces[faces.Count - 1].Emotions[Affdex.Emotions.Joy] < 90.0f
                    && faces[faces.Count - 1].Emotions[Affdex.Emotions.Surprise] < 90.0f
                    && faces[faces.Count - 1].Expressions[Affdex.Expressions.LipPucker] < 90.0f
                    && faces[faces.Count - 1].Expressions[Affdex.Expressions.LipCornerDepressor] < 3.0f
                    && faces[faces.Count - 1].Expressions[Affdex.Expressions.Attention] > 50.0f
                    && faces[faces.Count - 1].Expressions[Affdex.Expressions.BrowFurrow] < 90.0f)
            {
                runTimer = false;
                emotion = "";
                emotionIndex = -1;
                if (label)
                {
                    uiOutput.text = "Unclear";
                }
            }
        }
    }
}
