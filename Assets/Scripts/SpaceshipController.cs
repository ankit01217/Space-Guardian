using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceshipController : MonoBehaviour {

	public GameObject normalShipPF, fastShipPF, shieldedShipPF, attackerShipPF;
	public GameObject[] spipPrefabs;
	public Dictionary<string, int> ships;

	public float shipSpawnInterval = 3f;
	private int curActiveShipCount = 0;
	public int maxActiveShipCount = 10;

	public static int totShipsDestroyed = 0;
	public int levelShipCount = 20;
	public float randPointCircleRadius = 5f;
	public float maxX = 12;
	public float minX = 12;
	public float maxY = 8;
	public float minY = 0;



	void Awake(){

		//Initiate ships frequencies by type
		ships = new Dictionary<string,int> ();
		ships [Ship.NORMAL] = 5;
		ships [Ship.FAST] = 3;
		ships [Ship.SHIELDED] = 2;
		ships [Ship.ATTACKER] = 2;


		Debug.Log ("Screen size" + Screen.width + "height " + Screen.height);

	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnShip", shipSpawnInterval, shipSpawnInterval);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Vector3 getRandPosition(){
	
		float xRange = 25f;
		float yRange = -25f;

		Vector3 pt = new Vector3 ();
		/*
		Random random = new Random();
		int angle = random.Next(360);

		float centerX,centerY = 0;
		pt.x = centerX + randPointCircleRadius * cos (angle);
		pt.y = centerY + randPointCircleRadius * sin(angle);
		pt.z = 0;
		*/

		return pt;
	}

	void SpawnShip(){

		GameObject newShip;


		Vector3 randShipPos = new Vector3();
		if (totShipsDestroyed < 0.2f * levelShipCount) {
			// if < %20 of ships then generate 
			newShip = (GameObject)Instantiate(normalShipPF, getRandPosition(), Quaternion.identity);
		}

	}
}
