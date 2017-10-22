using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour {
	[SerializeField]
	private int vida;
	public bool isPlayer;
	// Use this for initialization
	void Start () {
		
	}

	public void TakeDamage(int damage){
		vida -= damage;
		if (0 >= vida) {
			vida = 0;
			if (!isPlayer) {
				Destroy (gameObject);
			} else {
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameController> ().GameEnded (gameObject.GetComponent<PlayerInput>().pNumber);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
