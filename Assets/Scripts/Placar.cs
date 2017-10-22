using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placar : MonoBehaviour {
	public int p1Point, p2Point;
	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad (gameObject);
		p1Point = 0;
		p2Point = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
