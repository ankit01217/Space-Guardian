using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

	public Camera cam;
	public GameObject ship;
	public GameObject asteroid;
	public GameObject pointMan;
	public Text text;
	public GameObject blackScreen;

	float minWorldX;
	float maxWorldX;
	GameObject asteroidInstance;
	GameObject shipInstance;

	// Use this for initialization
	void Start () {
		minWorldX = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		maxWorldX = cam.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;

		Instantiate (asteroid, new Vector3 (minWorldX / 2, 0, 0), Quaternion.identity);
		Instantiate (ship, new Vector3 (maxWorldX / 2, 0, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void PrepareMain () {
		StartCoroutine ("StartGame");
	}


	IEnumerator StartGame () {
		text.text = "Good job!";
		text.fontSize = 150;
		yield return new WaitForSeconds(1f);
		
		blackScreen.GetComponent<Animator> ().SetTrigger ("FadeIn");
		yield return new WaitForSeconds(1.5f);

		Application.LoadLevel ("Main");
	}
}
