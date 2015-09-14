using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameTimer : MonoBehaviour
{

	Slider gameTimer;
	public float timerSpeed = 0.01f;
	public AudioClip timerAudio, gameEndAudio;
	AudioSource audioSource;
	bool isTimerAudioEnabled = false;
	bool isGameOver = false;
	// Use this for initialization
	void Start ()
	{
	
		audioSource = GetComponent<AudioSource> ();
		gameTimer = GetComponent<Slider> ();
		gameTimer.value = 1;
	
		InvokeRepeating ("updateTimer", 0.01f, 0.01f);
	}


	void updateTimer ()
	{

		gameTimer.value = Mathf.Clamp (gameTimer.value - Time.deltaTime * timerSpeed, 0, 1);

		if (gameTimer.value < 0.01f && isTimerAudioEnabled == false) {
			Debug.Log (gameTimer.value);
			isTimerAudioEnabled = true;
			//play timer audio
			audioSource.PlayOneShot (timerAudio);
		}

		if (gameTimer.value == 0 && isGameOver == false) {
			Debug.Log ("Game Over");
			isGameOver = true;
			audioSource.PlayOneShot (gameEndAudio);

		}
	}



	// Update is called once per frame
	void Update ()
	{
	
	}
}
