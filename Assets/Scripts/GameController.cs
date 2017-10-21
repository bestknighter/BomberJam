using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Canvas c;

	public PlayerReadier pr1;
	public PlayerReadier pr2;

	public PlayerController pc1;
	public PlayerController pc2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pr1.Ready && pr2.Ready) {
			c.gameObject.SetActive (false);
			pc1.enabled = true;
			pc2.enabled = true;
			if (Input.GetKeyDown (KeyCode.Escape)) {
				c.gameObject.SetActive (true);
				pc1.enabled = false;
				pc2.enabled = false;
				pr1.Ready = false;
				pr2.Ready = false;
			}
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit ();
			#endif
		}
	}
}
