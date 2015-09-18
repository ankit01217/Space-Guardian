using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceshipController : MonoBehaviour
{

	public GameObject normalShipPF, vanishedShipPF, attackerShipPF;
	public GameObject[] spipPrefabs;
	public float shipSpawnInterval = 1f;
	public int round1Ship = 5;
	public int round2Ship = 10;
	public float round1Time = 30f;
	public float round2Time = 70f;

	AudioSource audioSource;
	public int totShipsDestroyed = 0;
	public Transform[] spawnPoints;

	float spaceSheepSpeedMultiplier = 1f;
	float spawnTimer = 0f;

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		spawnTimer += Time.deltaTime;
		Debug.Log ("spawnTimer :"+ spawnTimer);
		if (spawnTimer > shipSpawnInterval) {
			spawnTimer = 0;
			SpawnShip();
		}
	}

	public void onShipDestroyed ()
	{
		//curActiveShipCount = Mathf.Clamp (curActiveShipCount - 1, 0, maxActiveShipCount);
		totShipsDestroyed++;
		Debug.Log ("total ships destroyed :" + totShipsDestroyed);
	}

	Vector3 getRandPosition ()
	{
		Transform randTrans = spawnPoints [Random.Range (0, spawnPoints.Length)];
		return randTrans.position;
	}

	void SpawnShip ()
	{

		GameObject newShip;
		Vector3 randShipPos = new Vector3 ();
		Debug.Log ("totShipsDestroyed : " + totShipsDestroyed);
		if (totShipsDestroyed < round1Ship && GameTimer.timer < round1Time) {
			// if < %20 of ships then generate 
			GameObject[] arr = new GameObject[]{normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF};
			newShip = (GameObject)Instantiate (arr [Random.Range (0, arr.Length)], getRandPosition (), Quaternion.identity);
	
		} else if ((totShipsDestroyed >= round1Ship && totShipsDestroyed < round2Ship) || GameTimer.timer >= round1Time) {
			GameObject[] arr = new GameObject[]{normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF,vanishedShipPF,vanishedShipPF,vanishedShipPF,attackerShipPF,attackerShipPF};
			newShip = (GameObject)Instantiate (arr [Random.Range (0, arr.Length)], getRandPosition (), Quaternion.identity);
				
		}else if(totShipsDestroyed >= round2Ship || GameTimer.timer >= round2Time){
			GameObject[] arr = new GameObject[]{normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF,vanishedShipPF,vanishedShipPF,vanishedShipPF,attackerShipPF,attackerShipPF};
			newShip = (GameObject)Instantiate (arr [Random.Range (0, arr.Length)], getRandPosition (), Quaternion.identity);
		}
		else
		{
			GameObject[] arr = new GameObject[]{normalShipPF};
			newShip = (GameObject)Instantiate (arr [Random.Range (0, arr.Length)], getRandPosition (), Quaternion.identity);

		}


		newShip.GetComponent<Ship>().spaceShipSpeed *= spaceSheepSpeedMultiplier;
		newShip.transform.parent = transform;

			
	}

	public void activateLastPhase(){
		//inc speed and spawn rate after one wins the game
		shipSpawnInterval = 0.1f;	
		spaceSheepSpeedMultiplier = 5;
	}

}
