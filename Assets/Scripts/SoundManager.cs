using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static List<SoundTrack> soundList = new List<SoundTrack>();

    private static float clipLength;
    private static bool keepFadingIn;
    private static bool keepFadingOut;

    public AudioMixer mainMix;
    public List<AudioClip> soundTrackList;
    public List<AudioClip> sfxList;

    private bool inCalmZone = false;
    private bool inAlertZone = true;
    private bool inDangerZone = false;

    static public void AddTracks(int numberOfTracks, GameObject gameObj)
    {
        if(numberOfTracks != 0)
        {
            for(int i = 0; i < numberOfTracks; i++)
            {
                SoundTrack track = new SoundTrack
                {
                    id = 1,
                    audioSource = gameObj.AddComponent<AudioSource>()
                };
                soundList.Add(track);
            }
        }
    }

    static public void TrackSettings(int track, AudioMixer mainMix, string audioGroup, float trackVolume, bool loop = false)
    {
        soundList[track].audioSource.outputAudioMixerGroup = mainMix.FindMatchingGroups(audioGroup)[0];
        soundList[track].trackVolume = trackVolume;
        soundList[track].loop = loop;
    }

    static public void PlayAudio(int track, AudioClip audioClip = null, List<AudioClip> listAudioClip = null, int min = -2, int max = -2)
    {
        if(audioClip != null && listAudioClip == null && soundList[track].audioSource.isPlaying == false)
        {
            soundList[track].audioSource.PlayOneShot(audioClip, soundList[track].trackVolume);
        }

        if(soundList[track].loop)
        {
            clipLength = audioClip.length;
        }

        if (audioClip == null && listAudioClip != null && soundList[track].audioSource.isPlaying == false)
        {
            int index = Random.Range(min, max);

            if(index == -1)
            {
                Debug.Log("Ahh, silence.");
            }
            else
            {
                soundList[track].audioSource.PlayOneShot(listAudioClip[index], soundList[track].trackVolume);
                clipLength = listAudioClip[index].length;
            }

            if(soundList[track].loop)
            {
                //Keep on looping!
            }
        }
         
    }

    public static void CallFadeIn(int track, float speed, float maxVolume)
    {
        instance.StartCoroutine(FadeIn(track, speed, maxVolume));
    }

    public static void CallFadeOut(int track, float speed, float maxVolume)
    {
        instance.StartCoroutine(FadeOut(track, speed, maxVolume));
    }

    static IEnumerator FadeIn(int track, float speed, float maxVolume)
    {
        keepFadingIn = true;
        keepFadingOut = false;
        soundList[track].audioSource.volume = 0;
        float audioVolume = soundList[track].audioSource.volume;

        while(soundList[track].audioSource.volume < maxVolume && keepFadingIn)
        {
            audioVolume += speed;
            soundList[track].audioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    static IEnumerator FadeOut(int track, float speed, float maxVolume)
    {
        keepFadingIn = false;
        keepFadingOut = true;
        soundList[track].audioSource.volume = 0;
        float audioVolume = soundList[track].audioSource.volume;

        while(soundList[track].audioSource.volume > speed && keepFadingOut)
        {
            audioVolume -= speed;
            soundList[track].audioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

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
        SoundManager.AddTracks(2, gameObject);
        SoundManager.TrackSettings(0, mainMix, "Music", 0.5f, true);
        SoundManager.TrackSettings(1, mainMix, "SFX", 0.5f, true);

    }

    public void PlaySingle(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void PlayLevelSounds()
    {
        Debug.Log("Play Sounds for Level " + GameManager.instance.level);

        switch (GameManager.instance.level)
        {
            case 0: //Smile to start
                {
                    Debug.Log("Sounds for Level 0 loading");
                    SoundManager.CallFadeIn(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    SoundManager.PlayAudio(0, soundTrackList[0]);
                    SoundManager.CallFadeIn(1, 0.01f, SoundManager.soundList[1].trackVolume);
                    SoundManager.PlayAudio(1, soundTrackList[1]);
                    break;
                }
            case 1: //Intro
                {
                    Debug.Log("Sounds for Level 1 loading");
                    SoundManager.CallFadeOut(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    SoundManager.CallFadeOut(1, 0.01f, SoundManager.soundList[1].trackVolume);

                    SoundManager.PlayAudio(0, soundTrackList[3]);
                    SoundManager.CallFadeIn(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    SoundManager.PlayAudio(1, sfxList[0]);
                    SoundManager.CallFadeIn(0, 0.01f, SoundManager.soundList[1].trackVolume);
                    break;
                }
            case 2: //Tutorial
                {
                    Debug.Log("Sounds for Level 2 loading");
                    SoundManager.CallFadeOut(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    SoundManager.CallFadeOut(1, 0.01f, SoundManager.soundList[1].trackVolume);

                    SoundManager.PlayAudio(0, soundTrackList[4]);
                    SoundManager.CallFadeIn(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    break;
                }
            case 3: //Play Scene
                {
                    SoundManager.CallFadeOut(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    SoundManager.CallFadeOut(1, 0.01f, SoundManager.soundList[1].trackVolume);

                    SoundManager.PlayAudio(0, soundTrackList[5]);
                    SoundManager.CallFadeIn(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    break;
                }
            default: //Game Over
                {
                    SoundManager.CallFadeOut(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    SoundManager.CallFadeOut(1, 0.01f, SoundManager.soundList[1].trackVolume);

                    SoundManager.PlayAudio(0, soundTrackList[7]);
                    SoundManager.CallFadeIn(0, 0.01f, SoundManager.soundList[0].trackVolume);
                    SoundManager.PlayAudio(1, sfxList[1]);
                    SoundManager.CallFadeIn(0, 0.01f, SoundManager.soundList[1].trackVolume);
                    break;
                }
        }

    }

    private void Update()
    {
        int opponentState = 3;

        if (opponentState <= 2 && !inDangerZone)
        {
            //Danger
            Debug.Log("Danger music");
            inCalmZone = false;
            inAlertZone = false;
            inDangerZone = true;
            //SoundManager.PlayAudio(gameObject, soundTrackList[0]);
        }
        else if (opponentState > 2 && opponentState <= 5 && !inAlertZone)
        {
            //Alert
            Debug.Log("Alert music");
            inCalmZone = false;
            inAlertZone = true;
            inDangerZone = false;
            //SoundManager.PlayAudio(gameObject, soundTrackList[1]);
        }
        else if (opponentState > 5 && !inCalmZone)
        {
            //Calm
            Debug.Log("Calm music");
            inCalmZone = true;
            inAlertZone = false;
            inDangerZone = false;
            //SoundManager.PlayAudio(gameObject, soundTrackList[3]);
        }
        else
        {
            return;
        }

    }
}