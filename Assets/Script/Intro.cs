using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public GameObject black;
    public List<GameObject> texts;
    public bool introOver;

    private int frame;
    private readonly int frameDuration = 3; //In seconds
    private bool frameChanged;
  
    private void Awake()
    {
        Debug.Log("Intro awake!");
        frame = 0;
        frameChanged = false;
        introOver = false;
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
        Debug.Log("Switch frame: " + frame);
        switch (frame)
        {
            case 0:
                StartCoroutine(NextFrame());
                NextText();
                frame++;
                break;
            case 1:
                StartCoroutine(NextFrame());
                NextText();
                frame++;
                break;
            case 2:
                StartCoroutine(NextFrame());
                NextText();
                frame++;
                break;
            case 3:
                StartCoroutine(NextFrame());
                NextText();
                frame++;
                break;
            default:
                Debug.Log("Default frame in Intro Scene!");
                GameManager.instance.LoadNextLevel();
                break;
        }
    }
    private void NextText() {
        if(frame < texts.Count) texts[frame].SetActive(true);
        StartCoroutine(FadeIn(frame < texts.Count ? texts[frame] : null, frame >= 1 ? texts[frame - 1] : null));
    }

    private static IEnumerator FadeIn(GameObject newObj, GameObject oldObj) {
        const float time = 3;
        float timeLeft = time;
        var text = newObj?.GetComponent<TextMeshProUGUI>();
        var textOld = oldObj?.GetComponent<TextMeshProUGUI>();
        while(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if(text != null) text.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            if(textOld != null) textOld.color = Color.Lerp(Color.white, Color.clear, (time - timeLeft) / time);
            yield return null;
        }
        oldObj?.SetActive(false);
        if (text == null)
        {
            Debug.Log("Out of text. Let's move on!");
            GameManager.instance.LoadNextLevel();
        }
    }

    private static IEnumerator FadeInHiglight(GameObject newObj, GameObject oldObj) {
        const float time = 1;
        float timeLeft = time;
        var sprite = newObj?.GetComponent<SpriteRenderer>();
        var spriteOld = oldObj?.GetComponent<SpriteRenderer>();
        while(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if(sprite != null) sprite.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            if(spriteOld != null) spriteOld.color = Color.Lerp(Color.white, Color.clear, (time - timeLeft) / time);
            yield return null;
        }
        oldObj?.SetActive(false);
    }


    IEnumerator NextFrame()
    {
        Debug.Log("Wait...");
        //yield on a new YieldInstruction that waits for X seconds.
        yield return new WaitForSeconds(frameDuration);
        frame++;
        frameChanged = true;
        Debug.Log("frame: " + frame);
    }

}