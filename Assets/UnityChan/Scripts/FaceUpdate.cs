using UnityEngine;
using System.Collections;

public class FaceUpdate : MonoBehaviour
{
	public AnimationClip[] animations;

	Animator anim;

    Transform player;
    PlayerEmotions playerEmotions;

	public float delayWeight;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerEmotions = player.GetComponent<PlayerEmotions> ();
    }

	void Start ()
	{
		anim = GetComponent<Animator> ();
	}

	void OnGUI ()
	{
	}

	float current = 0;


	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			current = 1;
		} else {
			current = Mathf.Lerp (current, 0, delayWeight);
		}

        anim.SetLayerWeight (1, 1);
        float dominantEmotion = Mathf.Max (playerEmotions.currentAnger, playerEmotions.currentSadness, playerEmotions.currentEyeClosure,
                                    playerEmotions.currentJoy, playerEmotions.currentSurprise);

        if (dominantEmotion <= 5) {
            anim.SetLayerWeight (1, current);
        } else if (playerEmotions.currentJoy == dominantEmotion) {
            if (playerEmotions.currentJoy > 60) {
                Debug.Log ("smile1@unitychan");
                anim.CrossFade ("smile1@unitychan", 0.1f);
            } else {
                Debug.Log ("smile2@unitychan");
                anim.CrossFade ("smile2@unitychan", 0.1f);
            }
        } else if (playerEmotions.currentAnger == dominantEmotion) {
            if (playerEmotions.currentAnger > 40) {
                Debug.Log ("angry2@unitychan");
                anim.CrossFade ("angry2@unitychan", 0.1f);
            } else {
                Debug.Log ("angry1@unitychan");
                anim.CrossFade ("angry1@unitychan", 0.1f);
            }
        } else if (playerEmotions.currentSurprise == dominantEmotion) {
            Debug.Log ("sap@unitychan");
            anim.CrossFade ("sap@unitychan", 0.1f);
        } else if (playerEmotions.currentSadness == dominantEmotion) {
            if (playerEmotions.currentSadness > 50) {
                Debug.Log ("disstract1@unitychan");
                anim.CrossFade ("disstract1@unitychan", 0.1f);
            } else {
                Debug.Log ("disstract2@unitychan");
                anim.CrossFade ("disstract2@unitychan", 0.1f);
            }
        } else {
            Debug.Log ("eye_close@unitychan");
            anim.CrossFade ("eye_close@unitychan", 0.1f);
        }
	}
}
