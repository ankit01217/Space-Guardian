using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	public bool moveUp = true;
	
	Camera cam;
	Vector3 topBound;
	Vector3 botBound;
	float speed = 3f;
	Animator anim;

	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName == "Start") {
			cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
			topBound = new Vector3 (transform.position.x, cam.orthographicSize / 2, 0);
			botBound = new Vector3 (transform.position.x, -cam.orthographicSize / 2, 0);
		}

		anim = GetComponent<Animator> ();
		anim.Play ("ShipMove");
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "Start") {
			if (transform.position == topBound) {
				moveUp = false;
			} else if (transform.position == botBound) {
				moveUp = true;
			}
		
			if (moveUp) {
				transform.position = Vector3.MoveTowards (transform.position, topBound, speed * Time.deltaTime);
			} else {
				transform.position = Vector3.MoveTowards (transform.position, botBound, speed * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		Destroy (gameObject, 0.1f);

		if (Application.loadedLevelName == "Start") {
			Invoke("LoadMainLevel", 3f);
		}
	}

	void LoadMainLevel () {
		Application.LoadLevel ("Main");
	}
}
