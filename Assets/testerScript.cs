using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testerScript : MonoBehaviour
{
    public PlayerEmotions PE;

    //0-happy, 1-sad, 2-angry, 3-surprise,
    public Sprite[] emotions;
    public SpriteRenderer NPCface;

    public int currEmotion = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currEmotion < emotions.Length)
            {
                currEmotion++;
            }
            else
            {
                currEmotion = 0;
            }

            
        }
        NPCface.sprite = emotions[currEmotion];
        //use player face to change curr emotion
        if (Input.GetKey(KeyCode.K))
        {
            updateFace();
        }
        else
        {
            currEmotion = 1;
        }
    }

    void updateFace()
    {
        if (currEmotion == 1)
        {
            Debug.Log(PE.currentJoy);
            if (PE.currentJoy > 50f)
            {
                Debug.Log("change emotions");
                currEmotion = 0;
            }
        }
    }
}
