using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public GameObject black;
    public List<GameObject> texts;
    public TMP_InputField textField;

    private int _state = 0;
    private bool _gotToNameScreen;
    private bool _hasNameRun;
    private bool _hasName;
    public TextMeshProUGUI nameText;
    private string _nameTextStart;
    private string _nameTextEnd;
    [HideInInspector] public string playerName;

    //Intro Music
    public AudioClip introMusicPart1;
    public AudioClip introMusicPart2;

    public void OnEdit() {
        nameText.text = _nameTextStart + textField.text + _nameTextEnd;
    }

    IEnumerator waitForIntoMusic(float seconds)
    {
        yield return new WaitForSeconds(seconds); // This statement will make the coroutine wait for the number of seconds you put there, 2 seconds in this case
        SoundManager.instance.musicSource.UnPause();
    }

    public void EndEdit() {
        if(textField.text.Length == 0) return;
        _hasName = true;
        playerName = textField.text;
        textField.gameObject.SetActive(false);
    }

    private void Update() {
        if(Input.anyKeyDown || _hasNameRun) {
            _hasNameRun = false;
            switch(_state) {
                case 0:
                    if(!_gotToNameScreen) {
                        black.SetActive(true);
                        NextText();
                        const string insertNameHere = "[Insert Name Here]";
                        int i = nameText.text.IndexOf(insertNameHere, StringComparison.Ordinal);
                        _nameTextStart = nameText.text.Substring(0, i);
                        _nameTextEnd = nameText.text.Substring(i + insertNameHere.Length,
                                                                nameText.text.Length - i - insertNameHere.Length);
                        _gotToNameScreen = true;
                    }
                    if(_hasName) {
                        _hasNameRun = true;
                        _state++;
                    }
                    break;
                case 1:
                    StartIntroMusic();
                    NextText();
                    _state++;
                    break;
                case 2:
                    NextText();
                    _state++;
                    break;
                case 3:
                    NextText();
                    _state++;
                    break;
                case 4:
                    NextText();
                    _state++;
                    break;
                case 5:
                    NextText();
                    break;
            }
        }
    }

    private void NextText() {
        if(_state < texts.Count) texts[_state].SetActive(true);
        StartCoroutine(FadeIn(_state < texts.Count ? texts[_state] : null, _state >= 1 ? texts[_state - 1] : null));
    }

    private static IEnumerator FadeIn(GameObject newObj, GameObject oldObj) {
        const float time = 1;
        float timeLeft = time;
        var text = newObj?.GetComponent<TextMeshProUGUI>();
        var textOld = oldObj?.GetComponent<TextMeshProUGUI>();
        while(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if(text != null) text.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            if(textOld != null) textOld.color = Color.Lerp(Color.white, Color.clear, (time - timeLeft) / time);
            yield return null;
        }
        oldObj?.SetActive(false);
        if (text == null)
        {
            Destroy(GameObject.Find("SoundManager"));
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }

    private void StartIntroMusic()
    {
        SoundManager.instance.musicSource.Pause();
        SoundManager.instance.sfxSource.PlayOneShot(introMusicPart1);
        waitForIntoMusic(20);
        SoundManager.instance.musicSource.UnPause();
    }

    private static IEnumerator FadeInHiglight(GameObject newObj, GameObject oldObj) {
        const float time = 1;
        float timeLeft = time;
        var sprite = newObj?.GetComponent<SpriteRenderer>();
        var spriteOld = oldObj?.GetComponent<SpriteRenderer>();
        while(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if(sprite != null) sprite.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            if(spriteOld != null) spriteOld.color = Color.Lerp(Color.white, Color.clear, (time - timeLeft) / time);
            yield return null;
        }
        oldObj?.SetActive(false);
    }
}