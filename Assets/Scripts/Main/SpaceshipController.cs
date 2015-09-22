using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceshipController : MonoBehaviour
{

	public GameObject normalShipPF, vanishedShipPF, attackerShipPF;
	public GameObject[] spipPrefabs;
	public float shipSpawnInterval;
	public int round1Ship = 5;
	public int round2Ship = 10;
	public float round1Time;
	public float round2Time;

	public float shipSpawnIntervalSpeed;
	public float spaceShipSpeedMultiplierSpeed;
	public AudioClip shipBlastAudio;

	AudioSource audioSource;
	public int totShipsDestroyed = 0;
	public Transform[] spawnPoints;
	bool isLastPhaseActivated = false;
	float spaceShipSpeedMultiplier = 1f;
	float spawnTimer = 0f;

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		SpawnShip();
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

		if (isLastPhaseActivated == true) {
			shipSpawnInterval = Mathf.Clamp(shipSpawnInterval - shipSpawnIntervalSpeed*Time.deltaTime,0.5f,5f);	
			spaceShipSpeedMultiplier = Mathf.Clamp(spaceShipSpeedMultiplier + spaceShipSpeedMultiplierSpeed * Time.deltaTime,1f,4f);
		}
	}

	public void onShipDestroyed ()
	{
		//curActiveShipCount = Mathf.Clamp (curActiveShipCount - 1, 0, maxActiveShipCount);
		totShipsDestroyed++;
		audioSource.PlayOneShot(shipBlastAudio);
		
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
		if (GameTimer.timer > round1Time) {
			GameObject[] arr = new GameObject[]{normalShipPF,normalShipPF,normalShipPF,normalShipPF,vanishedShipPF,attackerShipPF,vanishedShipPF,vanishedShipPF,vanishedShipPF,attackerShipPF};
			newShip = (GameObject)Instantiate (arr [Random.Range (0, arr.Length)], getRandPosition (), Quaternion.identity);

		}
		else if (GameTimer.timer > round1Time) 
		{
			GameObject[] arr = new GameObject[]{normalShipPF,normalShipPF,normalShipPF,normalShipPF,normalShipPF,vanishedShipPF,vanishedShipPF,attackerShipPF,normalShipPF,attackerShipPF};
			newShip = (GameObject)Instantiate (arr[Random.Range (0, arr.Length)], getRandPosition (), Quaternion.identity);


		}
		else{
			newShip = (GameObject)Instantiate (normalShipPF, getRandPosition (), Quaternion.identity);

		}


		newShip.GetComponent<Ship>().spaceShipSpeed *= spaceShipSpeedMultiplier;
		newShip.transform.parent = transform;

			
	}

	public void activateLastPhase(){
		//inc speed and spawn rate after one wins the game
		isLastPhaseActivated = true;
	}

}
