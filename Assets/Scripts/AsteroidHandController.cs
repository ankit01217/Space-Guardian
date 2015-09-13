using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AsteroidHandController : MonoBehaviour {

	public GameObject rightHand;
	public GameObject crosshair;
	public int interval = 8;
	public float minDist = 0.5f;
	public float maxThrowStrength = 25f;
	public float minThrowStrength = 15f;
	public bool handIsEmpty = true;
	
	LinkedList<float> zCoords = new LinkedList<float>();
	float cooldown = 1f;
	GameObject asteroid;
	bool throwing = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		LinkHandToDetector ();
	}

	// Link the x and y-coords of the asteroid hand to that of the player's right hand
	void LinkHandToDetector () {
		Vector3 pos = new Vector3(rightHand.transform.position.x,
		                          rightHand.transform.position.y,
		                          transform.position.z);
		transform.position = pos;
	}
	
	// Called 30 times a second to match Kinect fps
	void FixedUpdate () {		
		CheckForThrow ();
	}

	void CheckForThrow () {
		float currCoord = rightHand.transform.localPosition.z;
		zCoords.AddLast (currCoord);
		if (zCoords.Count > interval) {
			zCoords.RemoveFirst();
		}

		float dist = currCoord - zCoords.ElementAt (0);

		if (dist > minDist && !throwing) {
			Debug.Log("Throw action detected");

			throwing = true;			
			AttemptThrow();
			Invoke("ResetCooldown", cooldown);
		}
	}
	
	void ResetCooldown () {
		throwing = false;
	}


	void OnTriggerEnter (Collider other) {
		if (handIsEmpty && other.tag == "Asteroid" && other.GetComponent<Asteroid>().grabable) {
			asteroid = other.gameObject;
			other.GetComponent<Asteroid>().pickedUp = true;
			other.GetComponent<Rigidbody>().velocity = Vector3.zero;
			other.transform.position = transform.position;	// snap asteroid to center of hand
			handIsEmpty = false;

			Debug.Log ("Picked up " + other.gameObject);
		}
	}

	// Only throw if there is an asteroid in hand
	void AttemptThrow () {
		if (asteroid) {
			// We only want the direction along the x-y plane
			float strength = Random.Range(minThrowStrength, maxThrowStrength);
			asteroid.GetComponent<Rigidbody> ().velocity = new Vector3(
				crosshair.transform.position.x - transform.position.x,
				crosshair.transform.position.y - transform.position.y,
				0).normalized * strength;
			asteroid.GetComponent<Asteroid>().pickedUp = false;
			asteroid.GetComponent<Asteroid>().thrown = true;

			handIsEmpty = true;
			asteroid = null;

			Debug.Log("Throw successful");
		}
	}
}
