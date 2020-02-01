using UnityEngine;
using UnityEngine.UI;
using Affdex;
using System.Collections.Generic;

public class PlayerEmotions : ImageResultsListener
{
    public float currentAnger;
    public float currentSurprise;
    public float currentJoy;
    public float currentSadness;
    public float currentEyeClosure;
    GameObject redWall;
    GameObject greenWall;
    public Slider joySlider;
    public Slider sadnessSlider;
    public Slider surpriseSlider;
    public Slider angerSlider;

    void Awake()
    {
       // redWall = GameObject.FindGameObjectWithTag ("RedWall");
       // greenWall = GameObject.FindGameObjectWithTag ("GreenWall");
    }

    public override void onFaceFound(float timestamp, int faceId)
    {
        //greenWall.SetActive (true);
        //redWall.SetActive (false);
        if (Debug.isDebugBuild) Debug.Log("Found the face");
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
       // greenWall.SetActive (false);
        //redWall.SetActive (true);
        currentAnger = 0;
        currentSurprise = 0;
        currentJoy = 0;
        currentSadness = 0;
        currentEyeClosure = 0;
        if (Debug.isDebugBuild) Debug.Log("Lost the face");
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        Debug.Log("onimageresults is working");
        if (faces.Count > 0)
        {
            faces[0].Emotions.TryGetValue(Emotions.Anger, out currentAnger);
            faces[0].Emotions.TryGetValue(Emotions.Surprise, out currentSurprise);
            faces[0].Emotions.TryGetValue(Emotions.Sadness, out currentSadness);
            faces[0].Emotions.TryGetValue(Emotions.Joy, out currentJoy);
            faces[0].Expressions.TryGetValue(Expressions.EyeClosure, out currentEyeClosure);

            //joySlider.value = currentJoy;
            //sadnessSlider.value = currentSadness;
            //surpriseSlider.value = currentSurprise;
            //angerSlider.value = currentAnger;
        }
    }
}