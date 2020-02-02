using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmileToStart : MonoBehaviour
{
    public PlayerListener PL;

    private bool _hasSmiled;
    public bool hasSmiled
    {
        get { return _hasSmiled; }
        set
        {
            _hasSmiled = value;
        }
    }

    void Start()
    {
        hasSmiled = false;
        PL.enableSearch = true;
    }

    // Update is called once per frame
    void Update()
    {
        //make it look for smiling
        if (PL.enableSearch && PL.timer > PL.cutOffTime && PL.emotion == "happy")
        {
            //disable timers
            hasSmiled = true;
            GameManager.instance.LoadNextLevel();
        }
    }
}
