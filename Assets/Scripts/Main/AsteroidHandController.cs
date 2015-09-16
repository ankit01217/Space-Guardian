using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AsteroidHandController : MonoBehaviour {

	public GameObject hand;
	public float aimTime = 1f;
	public float maxThrowStrength = 30f;
	public float minThrowStrength = 20f;
	public AudioClip throwAudio,pickupAudio;
	public GameObject arrowhead;
	public bool handIsEmpty = true;

	LineRenderer line;
	Vector3[] lineVertices = new Vector3[2];
	AudioSource audioSource;
	GameObject asteroid;
	bool throwing = false;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer> ();

		if (Application.loadedLevelName == "Main") {
			audioSource = GetComponent<AudioSource> ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "Main") {
			LinkHandToDetector ();
		}

		if (asteroid) {
			lineVertices [1] = transform.position;
			line.SetPosition (1, lineVertices [1]);
			Vector3 midpoint = Vector3.Lerp (lineVertices [0], lineVertices [1], 0.5f);
			arrowhead.transform.position = midpoint;
			arrowhead.transform.rotation = Quaternion.FromToRotation (Vector3.up,
			                                                          lineVertices [1] - lineVertices [0]);
		}
	}

	// Link the x and y-coords of the asteroid hand to that of the player's hand
	void LinkHandToDetector () {
		Vector3 pos = new Vector3(hand.transform.position.x,
		                          hand.transform.position.y,
		                          transform.position.z);
		transform.position = pos;
	}


	void OnTriggerEnter (Collider other) {
		if (asteroid == null && other.tag == "Asteroid" && other.GetComponent<Asteroid>().grabable) {
			handIsEmpty = false;

			asteroid = other.gameObject;
			asteroid.GetComponent<Asteroid>().pickedUp = true;
			asteroid.GetComponent<Asteroid>().glow.SetActive(true);
			asteroid.GetComponent<Rigidbody>().velocity = Vector3.zero;
			asteroid.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

			Debug.Log ("Picked up " + other.gameObject);

			if (Application.loadedLevelName == "Main") {
				audioSource.PlayOneShot(pickupAudio);
			}

			line.SetVertexCount(2);
			line.SetPosition(0, asteroid.transform.position);
			lineVertices[0] = (asteroid.transform.position);
			arrowhead.SetActive(true);

			Invoke("ThrowAsteroid", aimTime);
		}
	}

	// Only throw if there is an asteroid in hand
	void ThrowAsteroid () {
		// We only want the direction along the x-y plane
		float strength = Random.Range(minThrowStrength, maxThrowStrength);
		Vector3 oldPos = asteroid.transform.position;
		asteroid.GetComponent<Rigidbody> ().velocity = new Vector3 (transform.position.x - oldPos.x,
		                                                           transform.position.y - oldPos.y,
		                                                           0).normalized * strength;
		asteroid.GetComponent<Asteroid>().pickedUp = false;
		asteroid.GetComponent<Asteroid>().thrown = true;
		asteroid.transform.localScale = new Vector3(1f, 1f, 1f);
		asteroid = null;

		line.SetVertexCount (0);	// remove line		
		arrowhead.SetActive (false);
		handIsEmpty = true;

		Debug.Log("Throw successful");
		if (Application.loadedLevelName == "Main") {
			audioSource.PlayOneShot (throwAudio);
		}
	}
}
