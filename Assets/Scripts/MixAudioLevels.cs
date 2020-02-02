using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixAudioLevels : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetsfxLevel(float sfxLevel)
    {
        masterMixer.SetFloat("sfxVolume", sfxLevel);
    }

    public void SetMusicLevel(float musicLevel)
    {
        masterMixer.SetFloat("musicVolume", musicLevel);
    }

    public void SetMasterLevel(float masterLevel)
    {
        masterMixer.SetFloat("masterVolume", masterLevel);
    }

    public void ClearVolume()
    {
        masterMixer.ClearFloat("musicVolume");
        masterMixer.ClearFloat("sfxVolume");
        masterMixer.ClearFloat("masterVolume");
    }
}
