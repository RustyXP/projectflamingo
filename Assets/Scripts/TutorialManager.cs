using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text UIText;

    public Animator anim;

    public GameObject tear;

    private bool _hasSmiled;
    public bool hasSmiled
    {
        get { return _hasSmiled; }
        set
        {
            if (value)
            {
                StartCoroutine(tutorialTextDisplay(
                    "Hurray! Your encouraging smile has brought him some semblance of joy to his life!", true));
            }

            _hasSmiled = value;
        }
    }


    public PlayerListener PL;
    
    void Start()
    {
        hasSmiled = false;
        //PL.enableSearch = true;
        StartCoroutine(
            tutorialTextDisplay("Your brother is feeling down. Maybe you should smile at him to encourage him!", false));
    }

    void Update()
    {
        //make it look for smiling
        if (PL.emotionIndex == 0)
        {
            //disable timers
            hasSmiled = true;
            anim.SetBool("HasSmiled", true);

            tear.SetActive(false);
        }
    }
    
    IEnumerator tutorialTextDisplay(string textOutput, bool isEnd)
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        PL.enableSearch = false; 
        
        string sr = "";
        string[] characters = new string[textOutput.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] += textOutput[i];
        }
        for (int i = 0; i < characters.Length; i++)
        {
            sr += textOutput[i];
            UIText.text = sr;
            yield return wait;
        }

        PL.enableSearch = true;
        
        if (isEnd)
        {
            PL.enableSearch = false;
            //do something before changing to the next screen
            for (int i = 0; i < 3; i++)
            {
                yield return wait;
            }
            
            GameManager.instance.LoadNextLevel();
        }
    }
    
    
}
