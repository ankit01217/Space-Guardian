using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public bool pickedUp = false;
	public bool thrown = false;
	public bool grabable = true;
	public GameObject explosion;
	public bool isVisible = false;
	public AudioClip blastAudio;

	AudioSource audioSource;

	GameObject hand;
	int dmgPoints = 2;	// how much damage the asteroid does
	GameObject generator;
	float animDuration = 0f;
	float timeToWait = 0f;
	Animator anim;
	bool live = false;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		generator = transform.parent.gameObject;
	}

	public void SetParams (int dmg, Vector3 velocity) {
		dmgPoints = dmg;
		gameObject.GetComponent<Rigidbody> ().velocity = velocity;

		// activate respective model based on start dmg points
		transform.GetChild (dmgPoints).gameObject.SetActive (true);
		anim = transform.GetChild (dmgPoints).GetComponent<Animator> ();

//		if (dmgPoints == 2) {
//			GetComponent<CapsuleCollider> ().radius = 1f;
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pickedUp) {
			grabable = false;
			anim.SetBool ("pickedUp", true);
		} else {
			anim.SetBool ("pickedUp", false);
		}
	}

	// Spaceships use trigger collider
	void OnTriggerEnter(Collider other) {
		if (!pickedUp && other.gameObject.tag == "Hand") {
			pickedUp = true;
			hand = other.gameObject;
		} else if (thrown && other.gameObject.tag == "Spaceship") {
			Debug.Log("Spaceship hit!");
			audioSource.PlayOneShot (blastAudio);
			other.gameObject.SendMessage("hitSpaceShip", dmgPoints);
			DestroyAsteroid();
		}
	}

	void AsteroidHit () {
		DestroyAsteroid ();
	}


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
