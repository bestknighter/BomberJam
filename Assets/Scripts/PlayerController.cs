using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public PlayerInput playerInput;
	public Rigidbody2D rb2d;
	public float moveSpeed = 2f;


	// Use this for initialization
	void Start () {
		
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
			rb2d.velocity = new Vector2(0f, moveSpeed);
			break;
		case MoveDirection.DOWN:
			rb2d.velocity = new Vector2(0f, -moveSpeed);
			break;
		case MoveDirection.LEFT:
			rb2d.velocity = new Vector2(-moveSpeed, 0f);
			break;
		case MoveDirection.RIGHT:
			rb2d.velocity = new Vector2(moveSpeed, 0f);
			break;
		case MoveDirection.NOTHING:
			rb2d.velocity = new Vector2(0f, 0f);
			break;
		}
	}

}
