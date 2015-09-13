using UnityEngine;
using System.Collections;

public class AlienController : MonoBehaviour {

	GameObject[] aliens;
	AudioSource audioSource;
	public AudioClip gameoverClip;
	private GameObject planet;
	private ParticleSystem planetPS;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		planet = GameObject.FindGameObjectWithTag("Planet");
		planetPS = planet.GetComponent<ParticleSystem>();
		planetPS.enableEmission = true;
	}

	public void killRandomAlien(){

		if(planetPS.isPlaying) planetPS.Stop();
		if(!planetPS.isPlaying) planetPS.Play();

		aliens = (GameObject[])GameObject.FindGameObjectsWithTag ("Alien");
		if (aliens != null && aliens.Length > 0) {
			GameObject alien = aliens [Random.Range (0, aliens.Length)];
			Alien alienScript = alien.GetComponent<Alien>();
			alienScript.die();

		}

		if (aliens.Length == 0) {
			Debug.Log("game over");
			audioSource.clip = gameoverClip;
			audioSource.Play();
		}

	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
