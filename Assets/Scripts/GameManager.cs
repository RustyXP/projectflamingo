using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Make the Game Manager a singleton, so we can access it across
    // all scripts and scenes.
    public static GameManager instance = null;

    public int level = 0;
    public int playerTime = 0; //Measure how long it takes the player top play through.

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
        LoadNextLevel();
    }
        
    public void GameOver()
    {
        Debug.Log("Legit game over!");
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        enabled = false;
        level = 0;

    }

    public void YouWin()
    {
        Debug.Log("You won!");
        SceneManager.LoadScene("YouWin", LoadSceneMode.Single);
        enabled = false;
        level = 0;

    }

    public void LoadNextLevel()
    {
        Debug.Log("Load new level: " + level);
        switch (level)
        {
            case 0:
                {
                    level++;
                    Debug.Log("Loading Smile to Start Scene...");
                    SceneManager.LoadScene("SmileToStart", LoadSceneMode.Single);
                    SoundManager.instance.PlayLevelSounds();
                    break;
                }
            case 1:
                {
                    level++;
                    Debug.Log("Loading Text Intro Scene...");
                    SceneManager.LoadScene("TextIntroScene", LoadSceneMode.Single);
                    SoundManager.instance.PlayLevelSounds();
                    break;
                }
            case 2:
                {
                    level++;
                    Debug.Log("Loading Tutorial Scene...");
                    SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
                    SoundManager.instance.PlayLevelSounds();
                    break;
                }
            case 3:
                {
                    level++;
                    Debug.Log("Loading Play Scene...");
                    SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
                    SoundManager.instance.PlayLevelSounds();
                    break;
                }
            default:
                {
                    Debug.Log("You broke it. Are you happy now?");
                    SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
                    SoundManager.instance.PlayLevelSounds();
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
