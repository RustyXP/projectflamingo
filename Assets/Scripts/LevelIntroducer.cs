using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroScreen : MonoBehaviour
{
    const int SHOW_INTRO_STARTING_AT_LEVEL = 2;

    private Text levelText;
    private GameObject levelImage;
    private bool doingSetup;

    public float levelStartDelay = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Level Intro Screen loaded.");
        ShowLevelIntroScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if(doingSetup == true)
        {
            return;
        }
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }


    void ShowLevelIntroScreen()
    {
        Debug.Log("Hello! Level " + GameManager.instance.level);
        if(GameManager.instance.level > SHOW_INTRO_STARTING_AT_LEVEL)
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        switch(GameManager.instance.level)
        {
            case 0:
                {
                    //Start screen
                    break;
                }
            case 1:
                {
                    break;
                }
            case 2:
                {
                    levelText.text = "Tutorial";
                    levelImage.SetActive(true);
                    Invoke("HideLevelImage", levelStartDelay);
                    break;
                }
            case 3:
                {
                    levelText.text = "Calm your father.";
                    levelImage.SetActive(true);
                    Invoke("HideLevelImage", levelStartDelay);
                    break;
                }
            default:
                {
                    break;
                }
        }


    }

}
