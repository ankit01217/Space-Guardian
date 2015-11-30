using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject pointMan;
	public static bool isGameOver = false;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		DisablePointManCollider ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			ShowPointMan();
		}

		if(Input.GetKeyDown(KeyCode.C) == true){
			Application.LoadLevel("GameIntro");
		}
		else if(Input.GetKeyDown(KeyCode.G) == true){
			Application.LoadLevel("Main");
		}
		else if(Input.GetKeyDown(KeyCode.I) == true){
			Application.LoadLevel("Instructions");
		}
	}

	// toggle visibility for debugging
	void ShowPointMan () {
		bool visible = pointMan.transform.GetChild(0).GetComponent<Renderer>().isVisible;
		foreach (Transform point in pointMan.transform) {
			point.GetComponent<Renderer>() .enabled = !visible;
		}

	}

	void DisablePointManCollider () {
		foreach (Transform point in pointMan.transform) {
			point.GetComponent<Collider>() .enabled = false;
		}
	}

}
