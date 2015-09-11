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

		if (asteroid) {
			Debug.Log (asteroid);
		}

		// TODO: WHY DOES THIS GET CALLED WHEN ASTEROID IS NULL?!!
		// throw only if action is big enough, player is not already throwing, and asteroid is in hand
		if (dist > minDist && !throwing && asteroid) {
			Debug.Log(asteroid);
			Debug.Log("Throwing!!!");

			throwing = true;			
			ThrowAsteroid();
			Invoke("ResetCooldown", cooldown);
		}
	}
	
	void ResetCooldown () {
		throwing = false;
	}


	void OnTriggerEnter (Collider other) {
		Debug.Log (other.gameObject);

		// pick up asteroid
		if (other.tag == "Asteroid") {
			asteroid = other.gameObject;
			other.transform.position = transform.position;	// snap asteroid to center of hand
			pickedUp = true;
		}
	}

	void ThrowAsteroid () {
		Debug.Log (crosshair.transform.position);
		asteroid.GetComponent<Rigidbody> ().velocity = crosshair.transform.position - transform.position;

		throwing = false;
		asteroid = null;
	}
}
