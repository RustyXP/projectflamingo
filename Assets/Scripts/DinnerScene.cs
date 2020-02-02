using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinnerScene : MonoBehaviour
{
    public string[] script;

    public int[] speakorder;

    public Text uiDisplay;

    public Sprite[] bubbles;
    public Image bubble;

    public Animator[] anims;
    
    private int _currentState;
    
    public int currentState
    {
        get { return _currentState; }
        set
        {
            switch (value)
            {
                case 0:
                    StartCoroutine(pause(4, true));
                    break;
                case 1:
                    StartCoroutine(speakLines(script[1], true, 1));
                    break;
                case 2:
                    StartCoroutine(pause(3, true));
                    break;
                case 3:
                    StartCoroutine(speakLines(script[2], true, 2));
                    break;
                case 4:
                    StartCoroutine(pause(1, true));
                    break;
                case 5:
                    StartCoroutine(speakLines(script[3], true,3 ));
                    break;
                case 6:
                    StartCoroutine(pause(1, true));
                    break;
                case 7:
                    StartCoroutine(speakLines(script[4], true, 4));
                    break;
                case 8:
                    StartCoroutine(pause(1, true));
                    break;
                case 9:
                    StartCoroutine(speakLines(script[5], true, 5));
                    break;
                case 10:
                    StartCoroutine(pause(1, true));
                    break;
                case 11:
                    StartCoroutine(speakLines(script[6], true,6));
                    break;
                case 12:
                    StartCoroutine(pause(1, true));
                    break;
                case 13:
                    StartCoroutine(speakLines(script[7], true,7 ));
                    break;
                case 14:
                    anims[0].SetBool("TurnHead",true);
                    StartCoroutine(pause(1, true));
                    break;
                case 15:
                    StartCoroutine(speakLines(script[8], true,8));
                    break;
                case 16:
                    StartCoroutine(pause(2, true));
                    break;
                case 17:
                    GameManager.instance.LoadNextLevel();
                    break;
            }

            _currentState = value;
        }
    }
    
    WaitForSeconds wait = new WaitForSeconds(0.05f);
    // Start is called before the first frame update
    void Start()
    {
        bubble.sprite = bubbles[2];
        uiDisplay.text = "";
        currentState = 0;
    }

    IEnumerator speakLines(string line, bool endPause, int ind)
    {
        bubble.sprite = bubbles[speakorder[ind]];
        anims[speakorder[ind]].SetBool("isTalking", true);
        
        string sr = "";
        string[] characters = new string[line.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] += line[i];
        }
        for (int i = 0; i < characters.Length; i++)
        {
            sr += line[i];
            uiDisplay.text = sr;
            yield return wait;
        }

        if (endPause)
        {
            StartCoroutine(pause(2, false));
        }
        
        anims[speakorder[ind]].SetBool("isTalking", false);
        currentState++;
    }

    IEnumerator pause(int seconds, bool increment)
    {
        WaitForSeconds waitSec = new WaitForSeconds(.5f);

        for (int i = 0; i < seconds; i++)
        {
            yield return waitSec;
        }

        if (increment)
        {
            Debug.Log("pause increment");

            currentState++;
        }
    }
}
