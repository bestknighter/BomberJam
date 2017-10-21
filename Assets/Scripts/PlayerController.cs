using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField, Range(1.0f, 2.0f)]
	private int PlayerNumber = 1;

	public MoveDirection DesiredDirection {
		get { return m_Act1; }
	}

	public bool BotaoA {
		get { return m_BtnA; }
	}

	public bool BotaoB {
		get { return m_BtnB; }
	}

	private MoveDirection m_Act1;
	private MoveDirection m_Act2;
	private MoveDirection m_ChosenAct;
	private bool m_BtnA;
	private bool m_BtnB;

	void Start () {
		m_Act1 = MoveDirection.NOTHING;
		m_Act2 = MoveDirection.NOTHING;
		m_ChosenAct = MoveDirection.NOTHING;
		m_BtnA = false;
		m_BtnB = false;
	}

	void FixedUpdate () {
		m_BtnA = Input.GetAxis ("BtnAP" + PlayerNumber) > 0.0f;
		m_BtnB = Input.GetAxis ("BtnBP" + PlayerNumber) > 0.0f;

		MoveDirection opt1;
		MoveDirection opt2;
		float h = Input.GetAxis ("HorizontalP" + PlayerNumber);
		float v = Input.GetAxis ("VerticalP" + PlayerNumber);

		if (h > 0.0f) {
			m_Act1 = MoveDirection.RIGHT;
		} else if (h < 0.0f) {
			m_Act1 = MoveDirection.LEFT;
		} else if (v > 0.0f) {
			m_Act1 = MoveDirection.UP;
		} else if (v < 0.0f) {
			m_Act1 = MoveDirection.DOWN;
		} else {
			m_Act1 = MoveDirection.NOTHING;
		}

//		if (v > 0.0f) {
//			opt2 = MoveDirection.UP;
//		} else if (v < 0.0f) {
//			opt2 = MoveDirection.DOWN;
//		} else {
//			opt2 = MoveDirection.NOTHING;
//		}

//		if (opt1 == m_ChosenAct) {
//			m_Act1 = opt1;
//			m_Act2 = opt2;
//		} else if (opt2 == m_ChosenAct) {
//			m_Act1 = opt2;
//			m_Act2 = opt1;
//		}
	}

	void Update () {
//		Debug.Log ("ChosenInput: " + m_Act1 + "\nA: " + m_BtnA + "\tB: " + m_BtnB);
	}
}
