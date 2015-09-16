using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
	public Text timerText;
	public float timerSpeed;
	public AudioClip timerAudio,gameEndAudio;
	AudioSource audioSource;
	bool isTimerAudioEnabled = false;
	bool isGameOver = false;
	bool isFlickerEnabled = false;
	float timer = 0f;

	// Use this for initialization
	void Start ()
	{
		 
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("updateTimer", 0.01f, 0.01f);
	}



	void updateTimer ()
	{

		timer = Mathf.Clamp (timer + Time.deltaTime * timerSpeed,0f,100f);
		if (timer >= 95f && isFlickerEnabled == false) {
			isFlickerEnabled = true;
			//do flicker animation of shield and set timer to 80% after that
		}

		if (timer == 100f && isGameOver == false) {
			//game ends here
			//shwo end cinematic and shield completion animaion
			Debug.Log ("Game Over");
			isGameOver = true;
			audioSource.PlayOneShot (gameEndAudio);
			AutoFade.LoadLevel(2,2,1,Color.black);

		}


		timerText.text = (int)timer + "% complete";

	}



	// Update is called once per frame
	void Update ()
	{
	
	}
}
