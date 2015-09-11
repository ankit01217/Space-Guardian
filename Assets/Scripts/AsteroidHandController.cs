using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AsteroidHandController : MonoBehaviour {

	public GameObject rightHand;
	public GameObject crosshair;
	public int interval = 10;
	public float minDist = 0.7f;
	
	LinkedList<float> zCoords = new LinkedList<float>();
	float cooldown = 1f;
	GameObject asteroid;
	bool throwing = false;
	bool pickedUp = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// Only link it if player hasn't picked up an asteroid
		if (!pickedUp) {
			LinkHandToDetector ();
		}
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
		// pick up if it's an asteroid that hasn't been thrown
		if (other.tag == "Asteroid" && !other.GetComponent<Asteroid>().thrown) {
			asteroid = other.gameObject;
			other.transform.position = transform.position;	// snap asteroid to center of hand
			pickedUp = true;
			other.GetComponent<Asteroid>().thrown = true;

			Debug.Log ("Picked up " + other.gameObject);
		}
	}

	// Only throw if there is an asteroid in hand
	void AttemptThrow () {
		if (asteroid) {
			// We only want the direction along the x-y plane
			asteroid.GetComponent<Rigidbody> ().velocity = new Vector3(
				crosshair.transform.position.x - transform.position.x,
				crosshair.transform.position.y - transform.position.y,
				0);

			pickedUp = false;
			asteroid = null;

			Debug.Log("Throw successful");
		}
	}
}
