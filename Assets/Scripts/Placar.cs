using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placar : MonoBehaviour {
	public int p1Point, p2Point;
	// Use this for initialization
	void Start () {
		p1Point = 0;
		p2Point = 0;
	}

	public void P2Victory(){
		p2Point++;
		Debug.Log ("P2 Venceu! p1 " + p1Point + " x " + p2Point + " p2");
	}
	public void P1Victory(){
		p1Point++;
		Debug.Log ("P1 Venceu! p1 " + p1Point + " x " + p2Point + " p2");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
