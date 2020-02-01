using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Affdex;

public class PlayerFaceOutput : ImageResultsListener
{
    public float anger;
    public float surprise;
    public float joy;
    public float disgust;
    public float sad;
    public float eye_closure;

    private float highest;
    public float Highest;
    public float[] emotions = new float[5];

    private void Update()
    {
        

        for(int i = 0 ; i < emotions.Length; i++)
        {
            if (emotions[i] > highest && emotions[i] > 20)
            {
                highest = i;
            }
        }

        Debug.Log("Highest: " + highest);

    }

    public override void onFaceFound(float timestamp, int faceId)
    {
        for (int i = 0; i < 5; i++)
        {
            emotions[i] = 0;
        }
        Debug.Log("found face");
    }
    
    public override void onFaceLost(float timestamp, int faceId)
    {
        anger = 0;
        surprise = 0;
        joy = 0;
        disgust = 0;
        sad = 0;
        eye_closure = 0;
        if (Debug.isDebugBuild) Debug.Log("Lost the face");
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (faces.Count > 0)
        {
            faces[0].Emotions.TryGetValue(Emotions.Anger, out emotions[0]);
            faces[0].Emotions.TryGetValue(Emotions.Surprise, out emotions[1]);
            faces[0].Emotions.TryGetValue(Emotions.Sadness, out emotions[2]);
            faces[0].Emotions.TryGetValue(Emotions.Joy, out emotions[3]);
            faces[0].Emotions.TryGetValue(Emotions.Disgust, out emotions[4]);
            faces[0].Expressions.TryGetValue(Expressions.EyeClosure, out eye_closure);
        }
    }
}
