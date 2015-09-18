using UnityEngine;
using System.Collections;

public class HandMovement : MonoBehaviour {

	public bool moveLeft = true;

	Camera cam;
	Vector3 leftBound;
	Vector3 rightBound;
	float speed = 8f;


	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		leftBound = new Vector3 (cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x / 3,
		                         transform.position.y, transform.position.z);
		rightBound = new Vector3 (cam.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x / 3,
		                          transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position == leftBound) {
			moveLeft = false;
		} else if (transform.position == rightBound) {
			moveLeft = true;
		}

		if (moveLeft) {
			transform.position = Vector3.MoveTowards (transform.position, leftBound, speed * Time.deltaTime);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, rightBound, speed * Time.deltaTime);
		}
	}
}
