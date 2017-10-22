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
    public const int KICK = 0;
    public const int PUNCH = 1;
    public const int WALK = 2;
    public const int EXPLODE = 3;
    public const int MISS = 4;
    public float audioTimer;

    public bool beingkicked;
	private float timestampKick;
	public float kickDuration;
	public float kickSpeed;
	private MoveDirection kickDirection;

	private MoveDirection mv;

	public GameObject colliding;

	public Animator anim;
	public GameObject filho;

	[SerializeField]
	private int attackDamage;

    [SerializeField]
    public AudioClip[] aClip;// = new AudioClip[4];

	// Use this for initialization
	void Awake () {
		initialPosition = transform.position;
        Debug.Log(aClip.Length);
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
				anim.SetBool ("Up", true);
				anim.SetBool ("Down", false);
				anim.SetBool ("Walk", true);
				break;
			case MoveDirection.DOWN:
				mv = MoveDirection.DOWN;
				rb2d.velocity = new Vector2 (0f, -moveSpeed);
				attackCollider.offset = new Vector2 (0f, -attackColiderDistance);
				anim.SetBool ("Up", false);
				anim.SetBool ("Down", true);
				anim.SetBool ("Walk", true);
				break;
			case MoveDirection.LEFT:
				mv = MoveDirection.LEFT;
				rb2d.velocity = new Vector2 (-moveSpeed, 0f);
				attackCollider.offset = new Vector2 (-attackColiderDistance, 0f);
				//Linha de Reflexão do personagem
				filho.transform.localScale = (new Vector3(0.055f, 0.055f, 0.055f));
				/////
				anim.SetBool ("Walk", true);
				break;
			case MoveDirection.RIGHT:
				mv = MoveDirection.RIGHT;
				rb2d.velocity = new Vector2 (moveSpeed, 0f);
				attackCollider.offset = new Vector2 (attackColiderDistance, 0f);
				//Linha de Reflexão do personagem
				filho.transform.localScale = (new Vector3(-0.055f, 0.055f, 0.055f));
				/////
				anim.SetBool ("Walk", true);
				break;
			case MoveDirection.NOTHING:
				rb2d.velocity = new Vector2 (0f, 0f);
				anim.SetBool ("Up", false);
				anim.SetBool ("Down", false);
				anim.SetBool ("Walk", false);
//				anim.SetTrigger ("ForceIdle");
				break;
			}

            AudioSource temp = ((AudioSource)gameObject.GetComponent<AudioSource>());
            if (anim.GetBool("Walk") && colliding == null)
            {
                if (!temp.isPlaying)
                {
                    temp.clip = aClip[WALK];
                    Debug.Log(temp.clip.name);
                    temp.Play();
                }
                else
                {
                    Debug.Log("Camla fi, ta tocando a ultima");
                }
            }else if(!anim.GetBool("Walk") && temp.isPlaying && temp.clip.name == aClip[WALK].name)
            {
                temp.Stop();
            }

		
			if (playerInput.BotaoA) {
				//soco
				anim.SetTrigger("Punch");
				if (null != colliding) {
                    if (!temp.isPlaying || temp.clip.name != aClip[PUNCH].name)
                    {
                        temp.clip = aClip[PUNCH];
                        temp.Play();
                    }
                    if (colliding.tag == "Destrutivel") {
						colliding.GetComponent<Vida> ().TakeDamage (attackDamage);
					} else if (colliding.tag == "Player") {
						colliding.GetComponent<Stun> ().StunHit (attackDamage);
						colliding.GetComponent<PlayerController> ().anim.SetTrigger ("Hit");
					}
                }
                else
                {
                    temp.clip = aClip[MISS];
                    temp.Play();

                }

			} else if (playerInput.BotaoB) {
				//chute
				anim.SetTrigger("Kick");
				if (null != colliding) {
                    if (!temp.isPlaying || temp.clip.name != aClip[KICK].name)
                    {
                        temp.clip = aClip[KICK];
                        temp.Play();
                    }
                    if (colliding.tag == "Player") {
						colliding.GetComponent<PlayerController>().GetKicked(mv);
                        colliding.GetComponent<PlayerController>().anim.SetTrigger("Hit");
                    } else if (colliding.tag == "Arcade") {
						colliding.GetComponent<Arcade> ().StartMoving (mv);
					} else if (colliding.tag == "Destrutivel") {
						colliding.GetComponent<Arcade> ().StartMoving (mv);
					}
                }
                else
                {
                    if (!temp.isPlaying || temp.clip.name != aClip[MISS].name)
                    {
                        temp.clip = aClip[MISS];
                        temp.Play();
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
