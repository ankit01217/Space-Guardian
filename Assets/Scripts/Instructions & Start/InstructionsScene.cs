using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionsScene : MonoBehaviour {

	public Camera cam;
	public LineRenderer line;
	public GameObject asteroid;
	public GameObject spaceship;
	public GameObject hand;
	public GameObject planetPF;
	public Text timer;
	public GameObject cross;
	public GameObject tick;
	public int duration = 8;
	public GameObject fader;
	public AudioClip counterAudio;
	AudioSource audioSource;

	GameObject planet;
	GameObject topHand;
	float minWorldX;
	float maxWorldX;
	bool objectsSpawned = false;
	bool instructions = true;

	// Use this for initialization
	void Start () {
		minWorldX = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		maxWorldX = cam.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;
		audioSource = GetComponent<AudioSource> ();

		line.SetPosition(0, new Vector3(minWorldX, 0, 0));
		line.SetPosition(1, new Vector3(maxWorldX, 0, 0));

		SetUpHand ();
		StartCoroutine ("GoToStart");
	}
	
	// Update is called once per frame
	void Update () {
		if (instructions && topHand && topHand.transform.position.x < 0f && 
		    	topHand.GetComponent<HandMovement>().moveLeft && !objectsSpawned) {
			SpawnObjects();
		}
	}


	void SetUpHand () {
		topHand = (GameObject) Instantiate (hand, new Vector3 (0, cam.orthographicSize/2, -1), Quaternion.identity);
		Instantiate (hand, new Vector3 (0, -cam.orthographicSize/2, -1), Quaternion.identity);
	}


	void SpawnObjects () {
		if (planet) {
			Destroy(planet);
		}

		// TOP
		Instantiate (tick, new Vector3 (minWorldX/3*2, cam.orthographicSize/2, 0), Quaternion.identity);
		GameObject tempAsteroid = (GameObject) Instantiate (asteroid, 
		                                       new Vector3 (minWorldX/3, cam.orthographicSize/2, 0),
		                                       Quaternion.identity);
		tempAsteroid.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		Instantiate (spaceship, new Vector3 (maxWorldX/2, cam.orthographicSize/2, 0), Quaternion.identity);

		// BOTTOM
		Instantiate (cross, new Vector3 (minWorldX/3*2, -cam.orthographicSize/2, 0), Quaternion.identity);
		tempAsteroid = (GameObject) Instantiate (asteroid, 
		                                       new Vector3 (minWorldX/3, -cam.orthographicSize/2, 0),
		                                       Quaternion.identity);
		tempAsteroid.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		planet = (GameObject) Instantiate (planetPF, 
		                      new Vector3 (maxWorldX / 2, -cam.orthographicSize / 2, 0),
			                  Quaternion.identity);

		objectsSpawned = true;
		Invoke ("ObjectsGone", 1f);
	}

	void ObjectsGone () {
		objectsSpawned = false;
	}

	IEnumerator GoToStart () {
		for (int i = duration; i > 0; i--) {
			timer.text = i.ToString();
			yield return new WaitForSeconds(1f);
			audioSource.PlayOneShot(counterAudio);

		}
		Animator anim = fader.GetComponent<Animator> ();
		anim.SetTrigger ("FadeIn");
		yield return new WaitForSeconds (anim.GetCurrentAnimatorClipInfo(0).Length);
		Application.LoadLevel ("Start");
	}
	
}
