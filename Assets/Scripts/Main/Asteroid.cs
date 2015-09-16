using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public bool pickedUp = false;
	public bool thrown = false;
	public bool grabable = true;
	public GameObject explosion;
	public AudioClip blastAudio;
	public Animator anim;
	public GameObject glow;

	AlienController alienController;
	AudioSource audioSource;

	GameObject hand;
	GameObject generator;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		generator = transform.parent.gameObject;
		alienController = GameObject.FindObjectOfType<AlienController> ();
	}

	public void SetVelocity (Vector3 velocity) {
		gameObject.GetComponent<Rigidbody> ().velocity = velocity;
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
		if (grabable && other.gameObject.tag == "Hand" && other.GetComponent<AsteroidHandController>().handIsEmpty) {
			pickedUp = true;
			hand = other.gameObject;
		} else if (thrown && other.gameObject.tag == "Spaceship") {
			Debug.Log("Spaceship hit!");
			audioSource.PlayOneShot (blastAudio);
			other.gameObject.SendMessage("hitSpaceShip");
			DestroyAsteroid();
		} else if (other.gameObject.tag == "Planet") {
			if (thrown) {
				alienController.killRandomAlien(transform.position);
				DestroyAsteroid();
			} else {
				grabable = false;
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (!thrown && other.gameObject.tag == "Planet") {
			grabable = true;
		}
	}

	void AsteroidHit () {
		DestroyAsteroid ();
	}


	void OnBecameInvisible () {
		DestroyAsteroid ();
	}

	void DestroyAsteroid () {
		// play destruction animation 
		if (GetComponent<Renderer> ().isVisible) {
			Instantiate(explosion, transform.position, Quaternion.identity);
		}

		generator.GetComponent<AsteroidGenerator> ().asteroidCount--;
		Destroy (gameObject);

	}
}
