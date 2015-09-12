using UnityEngine;
using System.Collections;

public class AlienController : MonoBehaviour {

	GameObject[] aliens;
	AudioSource audioSource;
	public AudioClip deathClip, gameoverClip;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void killRandomAlien(){
		aliens = (GameObject[])GameObject.FindGameObjectsWithTag ("Alien");
		if (aliens != null && aliens.Length > 0) {
			GameObject alien = aliens [Random.Range (0, aliens.Length)];
			Alien alienScript = alien.GetComponent<Alien>();
			audioSource.clip = deathClip;
			audioSource.Play();
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
