using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameTimer : MonoBehaviour {

	Slider gameTimer;
	public float timerSpeed = 0.01f;
	bool isTimerAudioEnabled = false;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
	
		audioSource = GetComponent<AudioSource> ();
		gameTimer = GetComponent<Slider> ();
		gameTimer.value = 1;
	
		InvokeRepeating ("updateTimer", 0.01f, 0.01f);
	}


	void updateTimer(){

		gameTimer.value = Mathf.Clamp (gameTimer.value - Time.deltaTime * timerSpeed, 0, 1);

		if (gameTimer.value < 0.2 && isTimerAudioEnabled == false) {
			isTimerAudioEnabled = true;
			//play timer audio
			//audioSource.Play();
		}

		if(gameTimer.value == 0)
		{
			Debug.Log("Game Over");
			Time.timeScale = 0;

		}
	}



	// Update is called once per frame
	void Update () {
	
	}
}
