﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTileMap : MonoBehaviour {

	public GameObject obstaculo;
	public int numObstaculos;

	public int x_pixels;
	public int y_pixels;
	public int num_horizontalsquares;
	public int num_verticalsquares;
	public int xOffset;

	[SerializeField]
	private int square_width;
	[SerializeField]
	private int square_height;

	Vector2 SortSquare()
	{
		int sortedValor = Random.Range (0, num_horizontalsquares * num_verticalsquares);
		int x = sortedValor % num_verticalsquares;
		int y = sortedValor / num_verticalsquares;
		Vector2 ret = new Vector2 (x * square_width+xOffset + square_width/2, y * square_height + square_height/2);
		return ret;
	}


	Vector2 SortSInternalquare()
	{
		int x = Random.Range(1, num_verticalsquares-1);
		int y = Random.Range (1, num_horizontalsquares - 1);

		Vector2 ret = new Vector2 (x * square_width+xOffset, y * square_height);

		return ret;
	}

	// Use this for initialization
	void Start () { 
		for (int i = 0; i < numObstaculos; i++) 
		{
			Vector2 spawnPos = SortSquare ();
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(spawnPos.x, spawnPos.y, 0f));
			worldPos.z = -2;
			GameObject.Instantiate (obstaculo, worldPos, Quaternion.identity);
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
