using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour {

	private bool stun_flag = false;

	// Use this for initialization
	void Start () {

	}

	[SerializeField]
	public float timeLeft;

	public void StunHit(int damage){
		if (damage > 0) {
			stun_flag = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (stun_flag == true) {
			Debug.Log (gameObject.name);
			if (gameObject.name == "Jogador1PH") {
				((PlayerController)(GameObject.Find ("Jogador1PH").GetComponent<PlayerController>())).isStun = true;
			} else if(gameObject.name == "Jogador2PH") {
				((PlayerController)(GameObject.Find ("Jogador2PH").GetComponent<PlayerController>())).isStun = true;
			}

			Debug.Log (Time.deltaTime);
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				stun_flag = false;
				if (gameObject.name == "Jogador1PH") {
					((PlayerController)(GameObject.Find ("Jogador1PH").GetComponent<PlayerController>())).isStun = false;
				} else if(gameObject.name == "Jogador2PH") {
					((PlayerController)(GameObject.Find ("Jogador2PH").GetComponent<PlayerController>())).isStun = false;
				}
				timeLeft = 2.0f;
			}

		}
	}
}
	