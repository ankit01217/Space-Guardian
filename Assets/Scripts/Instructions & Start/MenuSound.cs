using UnityEngine;
using System.Collections;

public class MenuSound : MonoBehaviour {

	static bool AudioBegin = false; 
	AudioSource audio;
	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		audio = GetComponent<AudioSource>();
		if (!AudioBegin) {
			audio.Play ();
			//DontDestroyOnLoad (gameObject);
			AudioBegin = true;
		} 
	}

	void Update () {

	}


}
