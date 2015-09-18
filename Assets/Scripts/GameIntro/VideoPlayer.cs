using UnityEngine;
using System.Collections;

public class VideoPlayer : MonoBehaviour {

	private MovieTexture movTerxture;
	bool isVideoComplete = false;
	// Use this for initialization
	void Start () {
		movTerxture = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
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
				Application.LoadLevel(1);
			}

		}
	}
}
