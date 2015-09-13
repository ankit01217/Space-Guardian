using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

//	public bool pickedUp = false;
//	public bool thrown = false;
//	public bool grabable = true;
	public GameObject explosion;
	public bool isVisible = false;
	public AudioClip blastAudio,crumbleAudio;

	AudioSource audioSource;

//	GameObject hand;
	int dmgPoints = 2;	// how much damage the asteroid does
	GameObject generator;
	float animDuration = 0f;
	float timeToWait = 0f;
	Animator anim;
	bool live = false;

	// Use this for initialization
	void Start () {
		generator = transform.parent.gameObject;
//		hand = GameObject.Find ("Hand");
	}

	public void SetParams (int dmg, Vector3 velocity) {
		dmgPoints = dmg;
		gameObject.GetComponent<Rigidbody> ().velocity = velocity;

		// activate respective model based on start dmg points
		transform.GetChild (dmgPoints).gameObject.SetActive (true);

		// TODO: Clean this up once fractured asteroid is added
		if (dmgPoints != 0) {
			anim = transform.GetChild (dmgPoints).GetComponent<Animator> ();
		}
		if (dmgPoints == 2) {
			GetComponent<CapsuleCollider> ().radius = 1f;
		}

		// TODO: set animDuration

	}
	
	// Update is called once per frame
	void Update () {
//		if (pickedUp == true || dmgPoints == 0) {
//			grabable = false;
//		}
//
//		// TODO: add fractured asteroid in
//		// make this a function and send message to it instead
//		if (pickedUp == true && dmgPoints != 0) {
//			anim.SetBool ("pickedUp", true);
//			transform.position = hand.transform.position;
//		} else if (dmgPoints != 0) {
//			anim.SetBool ("pickedUp", false);
//		}
	}

	void OnTriggerEnter(Collider other) {
		if (dmgPoints == 0 && other.gameObject.tag == "Hand") {
			Debug.Log ("Asteroid crumbled");
			audioSource.PlayOneShot (crumbleAudio);
			DestroyAsteroid ();
		} else if (!live && other.gameObject.tag == "Hand") {
			live = true;
		} else if (live && other.gameObject.tag == "Spaceship") {
			Debug.Log("Spaceship hit!");
			audioSource.PlayOneShot (blastAudio);
			other.gameObject.SendMessage("hitSpaceShip", dmgPoints);
			//dmgPoints--;

			// Play animation/change state


			// Destroy asteroid
			//if(dmgPoints == 0) {
			DestroyAsteroid();
			//}
		}
	}

	void AsteroidHit () {
		DestroyAsteroid ();
	}


	//TODO: NOT DESTROYING PROPERLY!
	void OnBecameInvisible () {
		isVisible = false;
		Invoke ("DestroyAsteroid", timeToWait);		// destroy asteroid if it's out of screen for more than 3s
	}

	void OnBecameVisible () {
		isVisible = true;
		CancelInvoke ();
	}

	void DestroyAsteroid () {
		// play destruction animation 
		if (GetComponent<Renderer> ().isVisible) {
			Instantiate(explosion, transform.position, Quaternion.identity);
		}

		generator.GetComponent<AsteroidGenerator> ().asteroidCount--;
		Destroy (gameObject, animDuration);


	}
}
