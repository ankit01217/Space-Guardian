using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceshipController : MonoBehaviour {

	public GameObject normalShipPF, fastShipPF, shieldedShipPF, attackerShipPF;
	public GameObject[] spipPrefabs;
	public float shipSpawnInterval = 3f;
	public int round1Ship = 3;
	public int round2Ship = 5;
	public int round3Ship = 8;

	//public int curActiveShipCount = 0;
	//public int maxActiveShipCount = 10;

	public static int totShipsDestroyed = 0;
	public int levelShipCount = 20;

	public Transform[] spawnPoints;


	void Awake(){

		Debug.Log ("Screen size" + Screen.width + "height " + Screen.height);

	}

	// Use this for initialization
	void Start () {


		InvokeRepeating ("SpawnShip", shipSpawnInterval, shipSpawnInterval);

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void onShipDestroyed(){
		//curActiveShipCount = Mathf.Clamp (curActiveShipCount - 1, 0, maxActiveShipCount);
	
	}

	Vector3 getRandPosition(){
		Transform randTrans = spawnPoints[Random.Range(0,spawnPoints.Length)];
		return randTrans.position;
	}

	void SpawnShip(){

			GameObject newShip;
			Vector3 randShipPos = new Vector3();
			Debug.Log("totShipsDestroyed : " + totShipsDestroyed);
			if (totShipsDestroyed < round1Ship) {
				// if < %20 of ships then generate 
				newShip = (GameObject)Instantiate (normalShipPF, getRandPosition (), Quaternion.identity);
			} else if (totShipsDestroyed >= round1Ship && totShipsDestroyed < round2Ship) {
				GameObject[] arr = new GameObject[]{normalShipPF, fastShipPF};
				newShip = (GameObject)Instantiate (arr[Random.Range(0,arr.Length)], getRandPosition (), Quaternion.identity);
				
			}
			else if (totShipsDestroyed >= round2Ship && totShipsDestroyed < round3Ship) {
				GameObject[] arr = new GameObject[]{normalShipPF, fastShipPF, shieldedShipPF};
				newShip = (GameObject)Instantiate (arr[Random.Range(0,arr.Length)], getRandPosition (), Quaternion.identity);
				
			}
			else{
				GameObject[] arr = new GameObject[]{normalShipPF, fastShipPF,shieldedShipPF, attackerShipPF};
				newShip = (GameObject)Instantiate (arr[Random.Range(0,arr.Length)], getRandPosition (), Quaternion.identity);

			}


			newShip.transform.parent = transform;
			//curActiveShipCount++;


	}
}
