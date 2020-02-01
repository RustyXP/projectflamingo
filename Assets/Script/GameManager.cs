using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Make the Game Manager a singleton, so we can access it across
    // all scripts and scenes.
    public static GameManager instance = null;
    public float levelStartDelay = 2f;

    private int level = 0;
    private Text levelText;
    private GameObject levelImage;
    private bool doingSetup;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    private void OnLevelWasLoaded(int level)
    {
        level++;
        InitGame();
    }

    void InitGame()
    {
        Debug.Log("Hello!");
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "Relationship destroyed";
        levelImage.SetActive(true);
        enabled = false;

    }
    
    // Update is called once per frame
    void Update()
    {
        //If we're doing setup, then don't let the player do anything.
        if (doingSetup)
        {
            return;
        }
    }
}
