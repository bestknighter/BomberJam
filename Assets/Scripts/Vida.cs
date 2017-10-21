using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour {
	[SerializeField]
	private int vida;
	[SerializeField]
	private bool isPlayer;
	// Use this for initialization
	void Start () {
		
	}

	public void TakeDamage(int damage){
		vida -= damage;
		if (0 >= vida) {
			vida = 0;
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
