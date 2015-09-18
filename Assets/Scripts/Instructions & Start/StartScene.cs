using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

	public Camera cam;
	public GameObject ship;
	public GameObject asteroid;
	public GameObject pointMan;
	public Text text;
	public Image blackScreen;

	float minWorldX;
	float maxWorldX;
	GameObject asteroidInstance;
	GameObject shipInstance;

	// Use this for initialization
	void Start () {
		FreezeScene ();
		minWorldX = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		maxWorldX = cam.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;

		Instantiate (asteroid, new Vector3 (minWorldX / 2, 0, 0), Quaternion.identity);
		Instantiate (ship, new Vector3 (maxWorldX / 2, 0, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartGame () {		
		// Deactivate everything
		Invoke ("FreezeScene", 0.5f);
		pointMan.SetActive (false);
//		GameObject controller = GameObject.Find ("Controllers");
//		controller.transform.GetChild(0).GetComponent<AsteroidHandController>().enabled = false;
//		controller.transform.GetChild(1).GetComponent<AsteroidHandController>().enabled = false;
//		GameObject.Find ("Asteroid(Clone)").GetComponent<Asteroid>().enabled = false;

		// wait for 3s and load main
		Debug.Log ("test");
	}

	void FreezeScene () {
		text.text = "Good job!";
		//Time.timeScale = 0;


	}

	void FadeToBlack () {

	}
}
