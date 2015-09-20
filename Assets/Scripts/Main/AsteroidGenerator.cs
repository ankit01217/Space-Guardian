using UnityEngine;
using System.Collections;

public class AsteroidGenerator : MonoBehaviour
{

	public GameObject asteroid;
	public Camera cam;
	public float minSpeed = 3f;
	public float maxSpeed = 5f;
	public float spawnRate = 1f;
	public int initCount = 5;

	float zPlane = 18f;
	float minWorldX;
	float maxWorldX;

	// Use this for initialization
	void Start () {
		minWorldX = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		maxWorldX = cam.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;
		InvokeRepeating ("SpawnAsteroid", 0, spawnRate);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void SpawnAsteroid () {
		Vector3 pos, velocity;
		GetPosAndVelocity (Random.Range (1, 4), out pos, out velocity);

		GameObject newAsteroid = (GameObject)Instantiate (asteroid, pos, Quaternion.identity);
		newAsteroid.transform.SetParent (transform);
		newAsteroid.GetComponent<Asteroid> ().SetVelocity (velocity);
	}

	// Only generate on the left, top and right of screen
	void GetPosAndVelocity (int type, out Vector3 pos, out Vector3 velocity) {
		switch (type) {

		// spawn on top
		case 1: 
			pos = new Vector3 (Random.Range (minWorldX, maxWorldX), cam.orthographicSize + 1, zPlane);
			velocity = new Vector3 (Random.Range (minWorldX, maxWorldX),
			                       	   -cam.orthographicSize,
			                       	   0).normalized * RandSpeed ();
			break;

		// spawn to the left
		case 2:	
			pos = new Vector3 (minWorldX - 1, Random.Range (-cam.orthographicSize, cam.orthographicSize), zPlane);
			velocity = new Vector3 (maxWorldX, Random.Range (-cam.orthographicSize,
			    	                                 		   cam.orthographicSize),
			        	               						   0).normalized * RandSpeed ();
			break;

		// spawn to the right
		case 3:	
			pos = new Vector3 (maxWorldX + 1, Random.Range (-cam.orthographicSize, cam.orthographicSize), zPlane);
			velocity = new Vector3 (minWorldX, Random.Range (-cam.orthographicSize,
			                                        		   cam.orthographicSize),
			                       							   0).normalized * RandSpeed ();
			break;

		// top middle
		default:
			pos = new Vector3 (0, cam.orthographicSize + 1, zPlane);
			velocity = Vector3.one * RandSpeed ();
			break;
		}
	}

	float RandSpeed () {
		return Random.Range (minSpeed, maxSpeed);
	}
}
