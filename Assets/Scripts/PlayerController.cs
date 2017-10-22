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
	public bool isStun = false; 

	public bool beingkicked;
	private float timestampKick;
	public float kickDuration;
	public float kickSpeed;
	private MoveDirection kickDirection;

	private MoveDirection mv;

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
		if (beingkicked) {
			if (MoveDirection.UP == kickDirection) {
//				rb2d.position += new Vector2 (0f, kickSpeed);
				rb2d.velocity= new Vector2(0f, kickSpeed);
			} else if (MoveDirection.DOWN == kickDirection) {
//				rb2d.position += new Vector2 (0f, -kickSpeed);
				rb2d.velocity= new Vector2(0f, -kickSpeed);
			}else if (MoveDirection.LEFT == kickDirection) {
//				rb2d.position += new Vector2 (-kickSpeed, 0f);
				rb2d.velocity= new Vector2(-kickSpeed, 0f);
			}else if (MoveDirection.RIGHT == kickDirection) {
//				rb2d.position += new Vector2 (kickSpeed, 0f);
				rb2d.velocity= new Vector2(kickSpeed, 0f);
			}
			if (timestampKick + kickDuration < Time.time) {
				beingkicked = false;
				rb2d.velocity = new Vector2(0f, 0f);
			}
			return;
		}
		// Descobrir se é possível fazer DesiredDirection
			// Se sim, coloca em ChosenDirection
			// Se não, descobre se é possível fazer OptionalDirection
				// Se sim, coloca em ChosenDirection
				// Se não, coloca Nothing em ChosenDirection
		if (!isStun) {

			switch (playerInput.DesiredDirection) {
			case MoveDirection.UP:
				mv = MoveDirection.UP;
				rb2d.velocity = new Vector2 (0f, moveSpeed);
				attackCollider.offset = new Vector2 (0f, attackColiderDistance);
				break;
			case MoveDirection.DOWN:
				mv = MoveDirection.DOWN;
				rb2d.velocity = new Vector2 (0f, -moveSpeed);
				attackCollider.offset = new Vector2 (0f, -attackColiderDistance);
				break;
			case MoveDirection.LEFT:
				mv = MoveDirection.LEFT;
				rb2d.velocity = new Vector2 (-moveSpeed, 0f);
				attackCollider.offset = new Vector2 (-attackColiderDistance, 0f);
				break;
			case MoveDirection.RIGHT:
				mv = MoveDirection.RIGHT;
				rb2d.velocity = new Vector2 (moveSpeed, 0f);
				attackCollider.offset = new Vector2 (attackColiderDistance, 0f);
				break;
			case MoveDirection.NOTHING:
				rb2d.velocity = new Vector2 (0f, 0f);
				break;
			}

		
			if (playerInput.BotaoA) {
				//soco
				if (null != colliding) {
					if (colliding.tag == "Destrutivel") {
						colliding.GetComponent<Vida> ().TakeDamage (attackDamage);
					} else if (colliding.tag == "Player") {
						colliding.GetComponent<Stun> ().StunHit (attackDamage);
					}
				}

			} else if (playerInput.BotaoB) {
				//chute
				if (null != colliding) {
					((AudioSource) gameObject.GetComponent<AudioSource> ()).Play();
					if (colliding.tag == "Player") {
						colliding.GetComponent<PlayerController>().GetKicked(mv);
					} else if (colliding.tag == "Arcade") {
						colliding.GetComponent<Arcade> ().StartMoving (mv);

					} else if (colliding.tag == "Destrutivel") {
						colliding.GetComponent<Arcade> ().StartMoving (mv);
					}
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

	public void GetKicked(MoveDirection d){
		beingkicked = true;
		timestampKick = Time.time;
		kickDirection = d;
	}

	public void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Arcade" && beingkicked){
			gameObject.GetComponent<Vida> ().TakeDamage (500);
		}
	}

}
