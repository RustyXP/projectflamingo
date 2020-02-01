using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Make the Game Manager a singleton, so we can access it across
    // all scripts and scenes.
    public static GameManager instance = null;
    public int level = 0;

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

    void InitGame()
    {
        Debug.Log("Hello!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
