using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public PlayerInput playerInput;
	public Rigidbody2D rb2d;
	public float moveSpeed = 2f;
	public Vector3 initialPosition;
	public BoxCollider2D attackCollider;
	public float attackColiderDistance;

	public GameObject colliding;

	[SerializeField]
	private int attackDamage;

	// Use this for initialization
	void Awake () {
		initialPosition = transform.position;
	}

	void OnDisable() {
		OnEnable ();
	}

	void OnEnable() {
		transform.position = initialPosition;
	}

	// Update is called once per frame
	void FixedUpdate () {

		// Descobrir se é possível fazer DesiredDirection
			// Se sim, coloca em ChosenDirection
			// Se não, descobre se é possível fazer OptionalDirection
				// Se sim, coloca em ChosenDirection
				// Se não, coloca Nothing em ChosenDirection

		switch (playerInput.DesiredDirection) {
		case MoveDirection.UP:
			rb2d.velocity = new Vector2 (0f, moveSpeed);
			attackCollider.offset = new Vector2 (0f, attackColiderDistance);
			break;
		case MoveDirection.DOWN:
			rb2d.velocity = new Vector2(0f, -moveSpeed);
			attackCollider.offset = new Vector2 (0f, -attackColiderDistance);
			break;
		case MoveDirection.LEFT:
			rb2d.velocity = new Vector2(-moveSpeed, 0f);
			attackCollider.offset = new Vector2 (-attackColiderDistance, 0f);
			break;
		case MoveDirection.RIGHT:
			rb2d.velocity = new Vector2(moveSpeed, 0f);
			attackCollider.offset = new Vector2 (attackColiderDistance, 0f);
			break;
		case MoveDirection.NOTHING:
			rb2d.velocity = new Vector2(0f, 0f);
			break;
		}

		if (playerInput.BotaoA) {
			//soco
			if(null != colliding){
				if (colliding.tag == "Destrutivel") {
					colliding.GetComponent<Vida> ().TakeDamage (attackDamage);
				} else if (colliding.tag == "Player") {
					colliding.GetComponent<Vida> ().TakeDamage (attackDamage);
				}
			}

		}
	}

	void OnTriggerEnter2D(Collider2D other){
		colliding = other.gameObject;
	}
	void OnTriggerExit2D(){
		colliding = null;
	}

}
