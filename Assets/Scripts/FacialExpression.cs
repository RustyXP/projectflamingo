using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpression : MonoBehaviour {
	ImageResultsParser  userEmotions;
	Transform  player;
	public Renderer FaceRenderer;
	private Animator animator;

	// Use this for initialization
	void Start () {
		player =  GameObject.FindGameObjectWithTag("MyCharacter").transform;
		userEmotions = player.GetComponent<ImageResultsParser> ();
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


		//TODO brow frown

		//TODO brow raise

		if (userEmotions.currMouthOpen > 40) {
			animator.SetTrigger ("MouthOpened");
		} else {
			animator.SetTrigger ("MouthClosed");
		}
	}
}
