using UnityEngine;

public class PlayerInput : MonoBehaviour {

	[SerializeField, Range(1.0f, 2.0f)]
	private int PlayerNumber = 1;

	public int pNumber{
		get{ 
			return PlayerNumber;
		}
	}

	public MoveDirection DesiredDirection {
		get { return m_Dir1; }
		set { m_Dir1 = value; }
	}

	public MoveDirection OptionalDirection {
		get { return m_Dir2; }
	}

	public MoveDirection ChosenDirection {
		get { return m_ChosenDir; }
		set { m_ChosenDir = value; }
	}

	public bool BotaoA {
		get { return m_BtnA; }
	}

	public bool BotaoB {
		get { return m_BtnB; }
	}

	private MoveDirection m_Dir1;
	private MoveDirection m_Dir2;
	private MoveDirection m_ChosenDir;
	private bool m_BtnA;
	private bool m_BtnB;

	void Start () {
		m_Dir1 = MoveDirection.NOTHING;
		m_Dir2 = MoveDirection.NOTHING;
		m_ChosenDir = MoveDirection.NOTHING;
		m_BtnA = false;
		m_BtnB = false;
	}


	void FixedUpdate () {
//		m_BtnA = Input.GetAxis ("BtnAP" + PlayerNumber) > 0.0f;
//		m_BtnB = Input.GetAxis ("BtnBP" + PlayerNumber) > 0.0f;
		if (1 == PlayerNumber) {
			m_BtnA = Input.GetKeyDown ("k");
			m_BtnB = Input.GetKeyDown ("l");
		} else if (2 == PlayerNumber) {
			m_BtnA = Input.GetKeyDown ("f");
			m_BtnB = Input.GetKeyDown ("g");
		}

		MoveDirection opt1;
		MoveDirection opt2;
		float h = Input.GetAxis ("HorizontalP" + PlayerNumber);
		float v = Input.GetAxis ("VerticalP" + PlayerNumber);

		if (h > 0.0f) {
			opt1 = MoveDirection.RIGHT;
		} else if (h < 0.0f) {
			opt1 = MoveDirection.LEFT;
		} else {
			opt1 = MoveDirection.NOTHING;
		}

		if (v > 0.0f) {
			opt2 = MoveDirection.UP;
		} else if (v < 0.0f) {
			opt2 = MoveDirection.DOWN;
		} else {
			opt2 = MoveDirection.NOTHING;
		}

		if (MoveDirection.NOTHING == opt1) {
			m_Dir1 = opt2;
			m_Dir2 = opt1;
		} else if (MoveDirection.NOTHING == opt2) {
			m_Dir1 = opt1;
			m_Dir2 = opt2;
		} else if (m_ChosenDir == opt1) {
			m_Dir1 = opt1;
			m_Dir2 = opt2;
		} else {
			m_Dir1 = opt2;
			m_Dir2 = opt1;
		}
	}

	void Update () {
//		Debug.Log ("Direction:\tDesired: " + m_Dir1 + "\tOptional: " + m_Dir2 + "\tChosen: " + m_ChosenDir + "\nA: " + m_BtnA + "\tB: " + m_BtnB);
	}
}
