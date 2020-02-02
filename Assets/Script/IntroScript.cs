using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {
    public GameObject black;
    public TextMeshProUGUI introText;
    public List<String> texts;
    public bool introOver;

    public int frame;
    private readonly int frameDuration = 3; //In seconds
    private bool frameChanged;
  
    private void Start()
    {
        Debug.Log("Intro start!");
        frame = 0;
        introOver = false;
        frameChanged = true;
    }

    private void Update()
    {
        if(frameChanged)
        {
            frameChanged = false; //Reset the flag
            CallNextFrame();
        }
        if(introOver == true)
        {
            GameManager.instance.LoadNextLevel();
        }
    }

    private void CallNextFrame()
    {
        Debug.Log("Next frame " + frame);
        Debug.Log("text count:" + texts.Count);

        if (frame < texts.Count)
        {
            StartCoroutine(NextFrame());
        }
        else
        {
            introOver = true;
            GameManager.instance.LoadNextLevel();
        }
    }

    IEnumerator NextFrame()
    {
        introText.SetText(texts[frame]);
        Debug.Log("Wait...");
        //yield on a new YieldInstruction that waits for X seconds.
        yield return new WaitForSeconds(frameDuration);
        frame++;
        frameChanged = true;
        Debug.Log("frame: " + frame);
    }

}