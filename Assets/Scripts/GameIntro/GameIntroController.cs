using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameIntroController : MonoBehaviour {

	public Image fader;
	public RawImage titlePage;
	public RawImage videoPlayer;
	public AudioClip titleAudio, introAudio;
	AudioSource audioSource;
	private MovieTexture movTexture;
	bool isVideoComplete = false;


	// Use this for initialization
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.PlayOneShot (titleAudio);

		Invoke("startFadingAnim",3f);

	}

	void startFadingAnim(){
		fader.GetComponent<Animator>().SetTrigger("FadeIn");
		Invoke("startIntroVideo",1.5f);

	}

	void startIntroVideo(){
		Destroy (titlePage.gameObject);

		RawImage rim = videoPlayer.GetComponent<RawImage>();
		movTexture = (MovieTexture)rim.mainTexture;
		movTexture.Play();
		audioSource.PlayOneShot (introAudio);

	}
	
	void Update () {
		
		if (movTexture.isPlaying) {
			Debug.Log ("isPlaying");
		} else {
			Debug.Log ("Video Complete");
			if(isVideoComplete == false)
			{
				isVideoComplete = true;
				fader.GetComponent<Animator>().SetTrigger("FadeIn");
				Invoke("startInstructions",1.5f);
				
			}
			
		}
	}

	void onSkip(){
		movTexture.Stop();
		fader.GetComponent<Animator>().SetTrigger("FadeIn");
		Invoke("startInstructions",1.5f);

	}

	void startInstructions(){
		Application.LoadLevel("Instructions");
	}
}
