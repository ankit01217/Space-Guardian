using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameTimer : MonoBehaviour
{
	public Camera camera;
	public GameObject inner,outer;
	GameObject gameTimer;
	public float timerSpeed = 0.01f;
	public AudioClip timerAudio, gameEndAudio;
	AudioSource audioSource;
	bool isTimerAudioEnabled = false;
	bool isGameOver = false;

	// Use this for initialization
	void Start ()
	{
		 
		audioSource = GetComponent<AudioSource> ();
		gameTimer = GameObject.FindGameObjectWithTag ("GameTimer");
		Vector3 localScale = gameTimer.transform.localScale;
		Vector3 newScale = new Vector3 (1,localScale.y,localScale.z);
		gameTimer.transform.localScale = newScale;
	
		InvokeRepeating ("updateTimer", 0.01f, 0.01f);
		resizeTimer();
	}

	void resizeTimer(){

		double width = Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height;
		float innerWidth = GetComponent<Collider> ().bounds.size.x;
		outer.transform.localScale = new Vector3((float)width / innerWidth, 1, 1);

	}


	void updateTimer ()
	{

		Vector3 localScale = gameTimer.transform.localScale;
		float newXScale = Mathf.Clamp (localScale.x - Time.deltaTime * timerSpeed, 0, 1);
		Vector3 newScale = new Vector3 (newXScale,localScale.y,localScale.z);
		gameTimer.transform.localScale = newScale;
		inner.transform.localScale = newScale;

		if (newXScale < 0.07f && isTimerAudioEnabled == false) {
			Debug.Log (newXScale);
			isTimerAudioEnabled = true;
			//play timer audio
			audioSource.PlayOneShot (timerAudio);
		}

		if (newXScale == 0 && isGameOver == false) {
			Debug.Log ("Game Over");
			isGameOver = true;
			audioSource.PlayOneShot (gameEndAudio);
			AutoFade.LoadLevel(2,2,1,Color.black);

		}
	}



	// Update is called once per frame
	void Update ()
	{
	
	}
}
