using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placar : MonoBehaviour {
	public int p1Point, p2Point;
	public Text p1Score, p2Score;
	// Use this for initialization
	void Start () {
		p1Point = 0;
		p2Point = 0;
	}

	public void P2Victory(){
		p2Point++;
		Debug.Log ("P2 Venceu! p1 " + p1Point + " x " + p2Point + " p2");
		p2Score.text = "Score : " + p2Point;
	}
	public void P1Victory(){
		p1Point++;
		Debug.Log ("P1 Venceu! p1 " + p1Point + " x " + p2Point + " p2");
		p1Score.text = "Score : " + p1Point;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
