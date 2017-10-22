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

	public SpriteRenderer over;
	public Sprite ShownOverlay;
	public Sprite HiddenOverlay;

	public FakeTileMap ftm;

	public Animator start;


	private static GameController instance;
	private bool started;

	// Use this for initialization
	void Start () {
		if (null == instance) {
			instance = this;
		}
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (pr1.Ready && pr2.Ready) {
			if (!started) {
				start.SetBool ("StartFight", true);
				started = true;
			}
			if (Input.GetKeyDown (KeyCode.Escape)) {
				ShowStartScreen ();
				started = false;
			}
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			QuitGame ();
		}
	}

	public static void HideStartScreen() {
		instance.c.gameObject.SetActive (false);
		instance.pc1.enabled = true;
		instance.pc2.enabled = true;
		instance.over.sprite = instance.HiddenOverlay;
	}

	public static void ShowStartScreen() {
		instance.c.gameObject.SetActive (true);
		instance.pc1.enabled = false;
		instance.pc2.enabled = false;
		instance.pr1.Ready = false;
		instance.pr2.Ready = false;
		instance.over.sprite = instance.ShownOverlay;
		instance.ftm.Start();
	}

	public void QuitGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}
