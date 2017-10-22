using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTileMap : MonoBehaviour {

	public GameObject obstaculo;
	public GameObject obstaculoDestrutivel;
	public GameObject arcade;
	public int numObstaculos;
	public int numObstaculosDestrutivel;
	public int numMaxObstaculos;
	public int numArcades;

	public int x_pixels;
	public int y_pixels;
	public int num_horizontalsquares;
	public int num_verticalsquares;
	public int xOffset;

	[SerializeField]
	private int square_width;
	[SerializeField]
	private int square_height;

	private int[] indexes;
	private int[] fixedIndexes = { 10, 28, 46, 65, 31, 49, 61, 43, 34, 11, 14, 16 };

	public List<GameObject> cadeiras;
	public List<GameObject> flippers;
	public List<GameObject> colunas;

	public void Restart(){
		for (int i = 0; i < cadeiras.Count; i++) {
			GameObject.Destroy (cadeiras [i]);
		}
		for (int i = 0; i < flippers.Count; i++) {
			GameObject.Destroy (flippers [i]);
		}
		for (int i = 0; i < colunas.Count; i++) {
			GameObject.Destroy (colunas [i]);
		}
		cadeiras.Clear();
		flippers.Clear();
		colunas.Clear ();

		Start ();
	}

	/*
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

		Vector2 ret = new Vector2 (x * square_width+xOffset + square_width/2, y * square_height + square_height/2);

		return ret;
	}*/

	// Use this for initialization
	public void Start () {
		if (numObstaculos > numMaxObstaculos) {
			Debug.LogError ("Proibido numObstaculos ser maior que numMaxObstaculos");
		}
		indexes = new int[numMaxObstaculos];
		for (int i = 0; i < indexes.Length; i++) {
			indexes [i] = i;
		}
			
		for (int i = 0; i < indexes.Length; i++) {
			int tmp = indexes [i];
			int j = Random.Range (i, numMaxObstaculos);
			indexes [i] = indexes [j];
			indexes [j] = tmp;
		}

		for (int i = 0; i < fixedIndexes.Length; i++) {
			int x = fixedIndexes[i] % (num_verticalsquares);
			int y = fixedIndexes[i] / (num_verticalsquares);
			Vector2 spawnPos = new Vector2 (x * square_width+xOffset + square_width/2, y * square_height + square_height/2);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(spawnPos.x, spawnPos.y, 0f));
			worldPos.z = -2;
			colunas.Add(GameObject.Instantiate (obstaculo, worldPos, Quaternion.identity) );
		}

		SpawnMovable (obstaculoDestrutivel, 0, numObstaculosDestrutivel, cadeiras);
		SpawnMovable (arcade, numObstaculosDestrutivel, numObstaculosDestrutivel+numArcades, flippers);
		Debug.Log ("indexes lenght= " + indexes.Length);
			
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnMovable(GameObject go, int begin, int end, List<GameObject> list){
		int skip = 0; // flag para pular o spawn do obstáculo
		for (int i = begin; i < end; i++) {
			int x = indexes[i] % (num_verticalsquares-2)+1;
			int y = indexes[i] / (num_verticalsquares-2)+1;
			for (int j = 0; j < fixedIndexes.Length; j++) {
				if (  (x*9+y) == fixedIndexes [j] || indexes[i] == fixedIndexes[j]) {
					skip = 1;
				}
			}

			if (skip == 0) {
				Vector2 spawnPos = new Vector2 (x * square_width + xOffset + square_width / 2, y * square_height + square_height / 2);
				Vector3 worldPos = Camera.main.ScreenToWorldPoint (new Vector3 (spawnPos.x, spawnPos.y, 0f));
				worldPos.z = -2;
				list.Add( GameObject.Instantiate (go, worldPos, Quaternion.identity) );
			} else {
				skip = 0;
			}

		}		
	}

}
