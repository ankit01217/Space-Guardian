using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AsteroidHandController : MonoBehaviour {

	public GameObject hand;
	public int interval = 30;
	public float maxThrowStrength = 25f;
	public float minThrowStrength = 15f;
	public AudioClip throwAudio,pickupAudio;
	AudioSource audioSource;

	LinkedList<Vector3> zCoords = new LinkedList<Vector3>();
	bool storing = false;
	GameObject asteroid;
	bool throwing = false;

	// Use this for initialization
	void Start () {
//		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		LinkHandToDetector ();
	}

	// Link the x and y-coords of the asteroid hand to that of the player's hand
	void LinkHandToDetector () {
		Vector3 pos = new Vector3(hand.transform.position.x,
		                          hand.transform.position.y,
		                          transform.position.z);
		transform.position = pos;
	}
	
	// Called 30 times a second to match Kinect fps
	void FixedUpdate () {		
		StoreCoords ();
	}

	void StoreCoords () {
		zCoords.AddLast (hand.transform.position);
		if (zCoords.Count > interval) {
			zCoords.RemoveFirst();
		}
	}


	void OnTriggerEnter (Collider other) {
		//TODO: can add sound here when asteroid bounces off hand

		if (asteroid == null && other.tag == "Asteroid" && other.GetComponent<Asteroid>().grabable) {
			asteroid = other.gameObject;
			other.GetComponent<Asteroid>().pickedUp = true;
			other.GetComponent<Rigidbody>().velocity = Vector3.zero;
			//other.transform.position = transform.position;	// snap asteroid to center of hand

			Debug.Log ("Picked up " + other.gameObject);
//			audioSource.PlayOneShot(pickupAudio);

			Invoke("ThrowAsteroid", interval/30);
		}
	}

	// Only throw if there is an asteroid in hand
	void ThrowAsteroid () {
		// We only want the direction along the x-y plane
		float strength = Random.Range(minThrowStrength, maxThrowStrength);
		Vector3 oldPos = zCoords.First ();
		asteroid.GetComponent<Rigidbody> ().velocity = new Vector3(transform.position.x - oldPos.x,
		                                                           transform.position.y - oldPos.y,
																   0).normalized * strength;
		asteroid.GetComponent<Asteroid>().pickedUp = false;
		asteroid.GetComponent<Asteroid>().thrown = true;

		asteroid = null;

		Debug.Log("Throw successful");

//		audioSource.PlayOneShot(throwAudio);
	}
}
