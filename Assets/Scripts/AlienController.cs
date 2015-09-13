using UnityEngine;
using System.Collections;

public class AlienController : MonoBehaviour {

	GameObject[] aliens;
	AudioSource audioSource;
	public AudioClip gameoverClip;
	private GameObject planet;
	private ParticleSystem planetPS;
	bool isGameOver = false;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		planet = GameObject.FindGameObjectWithTag("Planet");
		//planetPS = planet.GetComponent<ParticleSystem>();
		//planetPS.enableEmission = true;
	}

	public void killRandomAlien(Vector3 shipPosition){

		//if(planetPS.isPlaying) planetPS.Stop();
		//if(!planetPS.isPlaying) planetPS.Play();
		aliens = (GameObject[])GameObject.FindGameObjectsWithTag ("Alien");
		if (aliens != null && aliens.Length > 0) {

			float minDis = 1000000;
			GameObject alien = null;
			for(int i=0;i<aliens.Length;i++)
			{
				float dis = Vector3.Distance(shipPosition,aliens[i].transform.position);
				if(dis < minDis)
				{
					minDis = dis;
					alien = aliens[i];
				}
			}

			//GameObject alien = aliens [Random.Range (0, aliens.Length)];
			Alien alienScript = alien.GetComponent<Alien>();
			alienScript.die();
			
		}


		

		if (aliens.Length == 0 && isGameOver == false) {
			Debug.Log("game over");
			isGameOver = true;
			audioSource.PlayOneShot(gameoverClip);
		}

	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
