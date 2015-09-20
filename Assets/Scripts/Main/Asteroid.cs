using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public bool pickedUp = false;
	public bool thrown = false;
	public bool grabable = true;
	public GameObject explosion;
	public AudioClip blastAudio, asteroidTimerAudio;
	public Animator anim;
	public GameObject glow;
	public SpriteRenderer asteroidTimer;

	AlienController alienController;
	AudioSource audioSource;
	bool hit = false;
	GameObject hand;
	Sprite[] asteroidSprites; 

	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName != "Instructions") {
			audioSource = GetComponent<AudioSource> ();
		}
		alienController = GameObject.FindObjectOfType<AlienController> ();
		asteroidSprites = Resources.LoadAll<Sprite>("asteroid timer");
	}

	public void SetVelocity (Vector3 velocity) {
		gameObject.GetComponent<Rigidbody> ().velocity = velocity;
	}
	
	// Update is called once per frame
	void Update () {
		if (pickedUp && grabable) {
			StartCoroutine("AsteroidTimer");
			grabable = false;
			anim.SetBool ("pickedUp", true);
			audioSource.PlayOneShot(asteroidTimerAudio);

		} else if (!pickedUp) {
			anim.SetBool ("pickedUp", false);
		}
	}

	IEnumerator AsteroidTimer () {
		asteroidTimer.enabled = true;
		for (int i = 0; i < 30; i++) {
			asteroidTimer.sprite = asteroidSprites[i];
			yield return new WaitForSeconds(1f/50f);
		}
		asteroidTimer.enabled = false;
	}


	void OnTriggerEnter(Collider other) {
		if (grabable && other.gameObject.tag == "Hand" && other.GetComponent<AsteroidHandController> ().handIsEmpty) {
			pickedUp = true;
			hand = other.gameObject;
		} else if (other.gameObject.tag == "Planet") {
			if (thrown) {
				alienController.killRandomAlien(transform.position);
				DestroyAsteroid();
			} else {
				grabable = false;
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (thrown && other.gameObject.tag == "Spaceship" && !hit) {
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
