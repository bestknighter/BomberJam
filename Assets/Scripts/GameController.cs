using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Vector3 p1InitialPos, p2InitialPos;
	public GameObject player1, player2;

	public Canvas c;

	public PlayerReadier pr1;
	public PlayerReadier pr2;

	public PlayerController pc1;
	public PlayerController pc2;

	public FakeTileMap ftm;

	public Animator start;

	public SpriteRenderer inGameOverlay;

	private static GameController instance;
	private bool started;

	public float cooldownBetwweenGames;
	private float timeWhenGameEnded;

	public FakeTileMap fakeTM;

	// Use this for initialization
	void Start () {
		if (null == instance) {
			instance = this;
		}
		started = false;
		p1InitialPos= new Vector3(-4.48f, -4.48f, -1.15f);
		p2InitialPos= new Vector3(4.48f, 4.44f, -1.05f);
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
		} else if (timeWhenGameEnded + cooldownBetwweenGames < Time.time) {
//			SceneManager.LoadScene (0);
		}
	}

	public static void HideStartScreen() {
		instance.c.gameObject.SetActive (false);
		instance.pc1.enabled = true;
		instance.pc2.enabled = true;
		instance.inGameOverlay.enabled = true;
	}

	public static void ShowStartScreen() {
		instance.c.gameObject.SetActive (true);
		instance.pc1.enabled = false;
		instance.pc2.enabled = false;
		instance.pr1.Ready = false;
		instance.pr2.Ready = false;
		instance.ftm.Start();
		instance.inGameOverlay.enabled = false;
	}

	public void QuitGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}

	public void GameEnded (int playerLost){
		timeWhenGameEnded = Time.time;
		player1.transform.position = p1InitialPos;
		player2.transform.position = p2InitialPos;

	}
}
