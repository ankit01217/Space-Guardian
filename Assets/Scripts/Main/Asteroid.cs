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
	bool hit = false;
	GameObject hand;

	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName != "Instructions") {
			audioSource = GetComponent<AudioSource> ();
		}
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


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Planet") {
			if (thrown) {
				alienController.killRandomAlien(transform.position);
				DestroyAsteroid();
			} else {
				grabable = false;
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (grabable && other.gameObject.tag == "Hand" && other.GetComponent<AsteroidHandController> ().handIsEmpty) {
			pickedUp = true;
			hand = other.gameObject;
		} else if (thrown && other.gameObject.tag == "Spaceship" && !hit) {
			hit = true;
			if(Application.loadedLevelName == "Main"){
				audioSource.PlayOneShot (blastAudio);
				other.gameObject.SendMessage("hitSpaceShip");
			} else if (Application.loadedLevelName == "Start") {				
				audioSource.PlayOneShot (blastAudio);
				GameObject.Find("GameManager").GetComponent<StartScene>().SendMessage("PrepareMain");
			}
			DestroyAsteroid();
		}
	}

	void OnTriggerExit (Collider other) {
		if (!thrown && other.gameObject.tag == "Planet") {
			grabable = true;
		}
	}

	void AsteroidHit () {
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		Invoke("DestroyAsteroid", 0.3f);
	}


	void OnBecameInvisible () {
		DestroyAsteroid ();
	}

	void DestroyAsteroid () {
		// play destruction animation 
		if (GetComponent<Renderer> ().isVisible) {
			Instantiate(explosion, transform.position, Quaternion.identity);
		}
		Destroy (gameObject);

	}
}
