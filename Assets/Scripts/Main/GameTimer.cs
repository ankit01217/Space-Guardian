using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
	public Text timerText;
	public float timerSpeed;
	public GameObject shield;
	public float maxAlpha = 0.5f;
	public float flickerPerc = 10f;
	public Image tranTint;
	public AudioClip timerAudio,gameEndAudio, flickerAudio, shieldCompleteAudio, shieldRefillAudio;
	public static float timer = 90f;

	MeshRenderer shieldRenderer;
	AudioSource audioSource;
	bool isTimerAudioEnabled = false;
	bool isFlickerEnabled = false;
	bool isFlickering = false;
	SpaceshipController spaceshipController;

	void Awake(){
		timer = 0;
		shieldRenderer = shield.GetComponent<MeshRenderer> ();
		setShieldAlpha(0);
		shield.SetActive (true);
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

		if (timer >= 100f && isFlickerEnabled == false) {
			isFlickerEnabled = true;
			//do flicker animation of shield and set timer to 80% after that
			startShieldFlickerAnimation();
			timer = flickerPerc;
		}

		if (GameManager.isGameOver == false && isFlickering == false) {
			timerText.text = (int)timer + "% complete";
			setShieldAlpha (timer);
			
		}



		if (timer == 100f && GameManager.isGameOver == false) {
			//game ends here
			//shwo end cinematic and shield completion animaion
			Debug.Log ("Game Over");
			GameManager.isGameOver = true;
			spaceshipController.activateLastPhase();

			LeanTween.alpha (shield, 0, 0.01f);
			audioSource.PlayOneShot (shieldCompleteAudio);
			Color newColor = new Color (1, 0.90f, 91/255f, 0.35f);
			shieldRenderer.material.color = newColor;
			LeanTween.alpha (shield, 0.3f, 0.5f).setEase(LeanTweenType.easeOutCirc);
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
		audioSource.PlayOneShot (flickerAudio);
		isFlickering = true;
		LeanTween.alpha (shield, 0, 0.15f).setEase(LeanTweenType.easeOutCirc).setLoopPingPong(2).setOnComplete(endShieldFlickerAnimation);
	}

	void endShieldFlickerAnimation(){
		isFlickering = false;
		audioSource.PlayOneShot (shieldRefillAudio);

	}


	void startTransition(){
		Invoke("startEndCinematic",0.6f);

	}

	void startEndCinematic(){
		Application.LoadLevel(2);
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
