using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Affdex;


public class ImageResultsParser : ImageResultsListener {

	public float currMouthOpen;
	public float currBrowRaise;
	public float currBrowFrown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void onFaceFound (float timestamp, int faceId) {
	}

	public override void onFaceLost(float timestamp, int faceId){
		currBrowFrown = 0;
		currBrowRaise = 0;
		currMouthOpen = 0;
	}

	public override void onImageResults(Dictionary<int, Face> faces){
		if (faces.Count >  0 ){
			//TODO
		} 
	}
}
