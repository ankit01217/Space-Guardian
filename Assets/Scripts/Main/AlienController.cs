using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlienController : MonoBehaviour {

	GameObject[] aliens;
	AudioSource audioSource;
	public AudioClip gameoverClip;
	public int maxAliens = 20;
	private GameObject planet;
	private float rotationSpeed = 10f;
	private int totAliensKilled = 0;
	public Image fader;


	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName == "Main") {
			audioSource = GetComponent<AudioSource> ();
			planet = GameObject.FindGameObjectWithTag ("Planet");
		}

	}

	public void killRandomAlien(Vector3 objectPosition){

		aliens = (GameObject[])GameObject.FindGameObjectsWithTag ("Alien");
		if (aliens != null && aliens.Length > 0) {

			float minDis = 1000000;
			GameObject alien = null;
			for(int i=0;i<aliens.Length;i++)
			{
				float dis = Vector3.Distance(objectPosition,aliens[i].transform.position);
				if(dis < minDis)
				{
					minDis = dis;
					alien = aliens[i];
				}
			}

			//GameObject alien = aliens [Random.Range (0, aliens.Length)];
			Alien alienScript = alien.GetComponent<Alien>();
			alienScript.die();
			totAliensKilled++;
			
		}

		if (Application.loadedLevelName == "Main") {
			if ((aliens.Length == 0 || totAliensKilled >= maxAliens) && GameManager.isGameOver == false) {
				Debug.Log ("game over");
				GameManager.isGameOver = true;
				audioSource.PlayOneShot (gameoverClip);

				fader.GetComponent<Animator>().SetTrigger("FadeIn");
				Invoke("startEndCinematic",1.4f);

		
			}
		}

	}


	void startEndCinematic(){
		Application.LoadLevel ("GameEndFailure");

	} 

	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "Main") {
			planet.transform.RotateAround (planet.transform.position, new Vector3 (0, 0, 1), 0.3f * Time.deltaTime * rotationSpeed);
		}

	}
}
