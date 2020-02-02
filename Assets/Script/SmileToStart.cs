using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class SmileToStart : MonoBehaviour
{
    public bool smiling = false;
    // Start is called before the first frame update

    void Awake()
    {
        smiling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(smiling == true)
        {
            //SceneManager.LoadScene("TextIntroScene", LoadSceneMode.Single);
            GameManager.instance.LoadNextLevel();
        }
    }
}
