using UnityEngine;
using System.Collections;

public class InstructionsScene : MonoBehaviour {

	public Camera cam;
	public LineRenderer line;
	public GameObject asteroid;
	public GameObject spaceship;
	public GameObject hand;
	public GameObject planetPF;

	GameObject planet;
	GameObject topHand;
	float minWorldX;
	float maxWorldX;
	bool objectsSpawned = false;
	bool instructions = true;

	// Use this for initialization
	void Start () {
		minWorldX = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		maxWorldX = cam.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;

		line.SetPosition(0, new Vector3(minWorldX, 0, 0));
		line.SetPosition(1, new Vector3(maxWorldX, 0, 0));

		SetUpHand ();
		Invoke ("SetUpStart", 6f);
	}
	
	// Update is called once per frame
	void Update () {
		if (instructions && topHand && topHand.transform.position.x < 0f && 
		    	topHand.GetComponent<HandMovement>().moveLeft && !objectsSpawned) {
			SpawnObjects();
		}
	}


	void SetUpHand () {
		topHand = (GameObject) Instantiate (hand, new Vector3 (0, cam.orthographicSize/2, -1), Quaternion.identity);
		Instantiate (hand, new Vector3 (0, -cam.orthographicSize/2, -1), Quaternion.identity);
	}


	void SpawnObjects () {
		if (planet) {
			Destroy(planet);
		}

		// TOP
		// Asteroid
		GameObject tempAsteroid = (GameObject) Instantiate (asteroid, 
		                                       new Vector3 (minWorldX/3, cam.orthographicSize/2, 0),
		                                       Quaternion.identity);
		tempAsteroid.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);


		// Spaceship
		Instantiate (spaceship, new Vector3 (maxWorldX/2, cam.orthographicSize/2, 0), Quaternion.identity);

		// BOTTOM
		// Asteroid
		tempAsteroid = (GameObject) Instantiate (asteroid, 
		                                       new Vector3 (minWorldX/3, -cam.orthographicSize/2, 0),
		                                       Quaternion.identity);
		tempAsteroid.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);

		// Planet
		planet = (GameObject) Instantiate (planetPF, 
		                      new Vector3 (maxWorldX / 2, -cam.orthographicSize / 2, 0),
			                  Quaternion.identity);

		objectsSpawned = true;
		Invoke ("ObjectsGone", 1f);
	}

	void ObjectsGone () {
		objectsSpawned = false;
	}

	void SetUpStart () {
//		GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");
//		foreach (GameObject asteroidSingle in asteroids) {
//			Destroy(asteroidSingle);
//		}
//		GameObject[] hands = GameObject.FindGameObjectsWithTag ("Hand");
//		foreach (GameObject hand in hands) {
//			Destroy(hand);
//		}		
//		Destroy(GameObject.FindGameObjectWithTag ("Spaceship"));
//		Destroy(GameObject.FindGameObjectWithTag ("Planet"));
//		line.SetVertexCount (0);
//
//		GameObject tempAsteroid = (GameObject) Instantiate (asteroid,
//		                                                    new Vector3 (minWorldX/2, 0, 0),
//		                                        			Quaternion.identity);
//		tempAsteroid.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
//		Instantiate (spaceship, new Vector3 (maxWorldX/2, 0, 0), Quaternion.identity);
	}
	
}
