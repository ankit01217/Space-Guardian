using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceshipController : MonoBehaviour {

	public GameObject normalShipPF, fastShipPF, shieldedShipPF, attackerShipPF;
	public GameObject[] spipPrefabs;
	public float shipSpawnInterval = 3f;

	private int curActiveShipCount = 0;
	public int maxActiveShipCount = 10;

	public static int totShipsDestroyed = 20;
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

	Vector3 getRandPosition(){
		Transform randTrans = spawnPoints[Random.Range(0,spawnPoints.Length)];
		return randTrans.position;
	}

	void SpawnShip(){

		if (curActiveShipCount < maxActiveShipCount) {
			GameObject newShip;
			Vector3 randShipPos = new Vector3();

			Debug.Log("totShipsDestroyed : " + totShipsDestroyed);
			if (totShipsDestroyed < 3) {
				// if < %20 of ships then generate 
				newShip = (GameObject)Instantiate (normalShipPF, getRandPosition (), Quaternion.identity);
			} else if (totShipsDestroyed >= 3 && totShipsDestroyed < 6) {
				GameObject[] arr = new GameObject[]{normalShipPF, fastShipPF};
				newShip = (GameObject)Instantiate (arr[Random.Range(0,arr.Length)], getRandPosition (), Quaternion.identity);
				
			}
			else if (totShipsDestroyed >= 6 && totShipsDestroyed < 10) {
				GameObject[] arr = new GameObject[]{normalShipPF, fastShipPF, shieldedShipPF};
				newShip = (GameObject)Instantiate (arr[Random.Range(0,arr.Length)], getRandPosition (), Quaternion.identity);
				
			}
			else{
				GameObject[] arr = new GameObject[]{normalShipPF, fastShipPF,shieldedShipPF, attackerShipPF};
				newShip = (GameObject)Instantiate (arr[Random.Range(0,arr.Length)], getRandPosition (), Quaternion.identity);

			}


			newShip.transform.parent = transform;
			curActiveShipCount++;
		
		}



	}
}
