using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReadier : MonoBehaviour {

	public float secondsTimeout;
	public PlayerInput pi;

	public bool Ready {
		get { return m_ready; }
		set { m_ready = value;
			  text.enabled = value;}
	}

	public Text text;

	private bool m_ready;

	private float endTime;

	// Use this for initialization
	void Start () {
		m_ready = false;
		text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (pi.BotaoA || pi.BotaoB) {
			endTime = secondsTimeout + Time.realtimeSinceStartup;
			m_ready = true;
			text.enabled = true;
		}
		if (m_ready && Time.realtimeSinceStartup > endTime) {
			m_ready = false;
			text.enabled = false;
		}
	}
}
