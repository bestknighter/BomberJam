using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arcade : MonoBehaviour {
	public bool beingkicked;
	private MoveDirection moveDirection;
	public float limits;
	public float speed;
	public Rigidbody2D rb2d;

	public int damageToPlayer;
	// Use this for initialization
	void Start () {
		beingkicked = false;
	}

	public void StartMoving(MoveDirection direction){
		beingkicked = true;
		moveDirection = direction;
		if (moveDirection == MoveDirection.NOTHING) {
			Debug.Log ("AHAAAAAAAAAAAA PORRA!");
		}
		Debug.Log ("Arcade deslizando");
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (beingkicked) {
			Vector2 newPos = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y);
			if (moveDirection == MoveDirection.LEFT) {
				newPos += new Vector2 (-speed, 0f);
				if (newPos.x < -limits) {
					newPos.x = -limits;
					beingkicked = false;
				}
			}
			if (moveDirection == MoveDirection.RIGHT) {
				newPos += new Vector2 (speed, 0f);
				if (newPos.x > limits) {
					newPos.x = limits;
					beingkicked = false;
				}
			}
			if (moveDirection == MoveDirection.UP) {
				newPos += new Vector2 (0f, speed);
				if (newPos.y > limits) {
					newPos.y = limits;
					beingkicked = false;
				}
			}
			if (moveDirection == MoveDirection.DOWN) {
				newPos += new Vector2 (0f, -speed);
				if (newPos.y < -limits) {
					newPos.y = -limits;
					beingkicked = false;
				}
			}
			rb2d.MovePosition(newPos); 
		}
	}
	public void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player" && beingkicked) {
			coll.gameObject.GetComponent<Vida> ().TakeDamage (damageToPlayer);
		} else if (beingkicked) {
			beingkicked = false;
		}
	}
}
