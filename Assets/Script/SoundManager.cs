using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

}
