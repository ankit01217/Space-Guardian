using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VideoPlayer : MonoBehaviour {

	private MovieTexture movTerxture;
	public Image fader;

	bool isVideoComplete = false;
	// Use this for initialization
	void Start () {
		//movTerxture = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
		RawImage rim = GetComponent<RawImage>();
		movTerxture = (MovieTexture)rim.mainTexture;

		movTerxture.Play();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (movTerxture.isPlaying) {
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

	void startInstructions(){
		Application.LoadLevel("Instructions");
	}
}
