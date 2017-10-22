using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YZSync : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	//	gameObject.transform.position.z = gameObject.transform.position.y;
		gameObject.transform.Translate (new Vector3(0f, 0f, gameObject.transform.position.y - gameObject.transform.position.z) );
	}
}
