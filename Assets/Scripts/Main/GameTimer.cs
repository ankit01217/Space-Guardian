﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
	public Text timerText, messageText;
	public float timerSpeed;
	public GameObject shield;
	public float maxAlpha = 0.5f;
	public float flickerPerc = 10f;
	public Image fader;
	public AudioClip timerAudio,gameEndAudio, flickerAudio, shieldCompleteAudio, shieldRefillAudio, shieldBrokenAliensAudio, shieldCompleteAliensAudio;
	public AudioClip flicker_voiceover, fixedshield_voiceover;
	public static float timer = 90f;
	public float endPhasePerc = 90f;

	bool isShieldMessageIsShown = false;
	bool isEndPhaseEnabled = false;
	MeshRenderer shieldRenderer;
	AudioSource audioSource;
	bool isTimerAudioEnabled = false;
	bool isFlickerEnabled = false;
	bool isFlickering = false;
	SpaceshipController spaceshipController;
	bool isFlickerComplete = false;

	void Awake(){

		timer = 0;
		messageText.text = "";
		shieldRenderer = shield.GetComponent<MeshRenderer> ();
		setShieldAlpha(0);
		//shield.SetActive (true);
		fader.GetComponent<Animator>().SetTrigger("FadeOut");

	
	}

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		spaceshipController = GameObject.FindObjectOfType<SpaceshipController> ();
		InvokeRepeating ("updateTimer", 0.01f, 0.01f);


	}



	void updateTimer ()
	{
		if (isFlickering == false) {
			timer = Mathf.Clamp (timer + Time.deltaTime * timerSpeed,0f,100f);
		}

		if (timer >= 80f && isShieldMessageIsShown == false) {
			isShieldMessageIsShown = true;
			messageText.text = "Woohoo! We have almost fixed the shield!";
			audioSource.PlayOneShot(fixedshield_voiceover);
			Invoke("clearMessage",fixedshield_voiceover.length);
			
		
		}
		if (timer >= 100f && isFlickerEnabled == false) {
			isFlickerEnabled = true;
			audioSource.PlayOneShot (shieldCompleteAudio);
			startShieldFlickerAnimation();
			timer = flickerPerc;


			if (isEndPhaseEnabled == false) {
				isEndPhaseEnabled = true;
				spaceshipController.activateLastPhase();
				
			}

		}

		if (GameManager.isGameOver == false && isFlickering == false) {
			timerText.text = "Shield completion: " + (int)timer + "%";
			//setShieldAlpha (timer);
			
		}


		if (timer == 100f && GameManager.isGameOver == false) {
			//game ends here
			//shwo end cinematic and shield completion animaion
			Debug.Log ("Game Over");
			GameManager.isGameOver = true;
			audioSource.PlayOneShot (shieldCompleteAudio);

			shield.SetActive (true);
			LeanTween.alpha (shield, 0, 0.01f);
			//Color newColor = new Color (1, 0.90f, 91/255f, 0.35f);
			//shieldRenderer.material.color = newColor;
			LeanTween.alpha (shield, 0.3f, 0.5f).setEase(LeanTweenType.easeOutCirc);
			audioSource.PlayOneShot(shieldCompleteAliensAudio);

			Invoke("startTransition",5f);

		}


	}


	void setShieldAlpha(float alpha){

		Color curColor = shieldRenderer.material.color;
		float newAlpha = (timer/100) * (maxAlpha);
		Debug.Log (" timer , newAlpha " + timer + ", "+ newAlpha);
		curColor.a = newAlpha;
		shieldRenderer.material.color = curColor;
	}


	void startShieldFlickerAnimation(){
		shield.SetActive (true);
		isFlickering = true;
		setShieldAlpha (1);
		Invoke ("startFlickerTween", 5f);
	
	}

	void startFlickerTween(){
		audioSource.PlayOneShot (flickerAudio);
		LeanTween.alpha (shield, 0, 0.15f).setEase(LeanTweenType.easeOutCirc).setLoopPingPong(2).setOnComplete(endShieldFlickerAnimation);

	}

	void endShieldFlickerAnimation(){
		messageText.text = "Oh no! Shield is still broken! Please give us more time...";
		Invoke("clearMessage",flicker_voiceover.length);

		audioSource.PlayOneShot(flicker_voiceover);
		audioSource.PlayOneShot(shieldBrokenAliensAudio);

		isFlickering = false;
		audioSource.PlayOneShot (shieldRefillAudio);
		LeanTween.alpha (shield, 0, 0.01f);
		isFlickerComplete = true;
		shield.SetActive (false);

	}

	void clearMessage(){
		messageText.text = "";
	}

	void startTransition(){
		fader.GetComponent<Animator>().SetTrigger("FadeIn");
		Invoke("startEndCinematic",1.4f);
	}

	void startEndCinematic(){
		Application.LoadLevel("GameEndSuccess");
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
