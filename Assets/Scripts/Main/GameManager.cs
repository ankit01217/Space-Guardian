using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject pointMan;

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
	}

	// toggle visibility for debugging
	void ShowPointMan (){
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
