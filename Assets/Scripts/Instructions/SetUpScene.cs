using UnityEngine;
using System.Collections;

public class SetUpScene : MonoBehaviour {

	public Camera cam;
	public LineRenderer line;
	public GameObject asteroid;

	float minWorldX;
	float maxWorldX;

	// Use this for initialization
	void Start () {
		minWorldX = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		maxWorldX = cam.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;

		line.SetPosition(0, new Vector3(minWorldX, 0, 0));
		line.SetPosition(1, new Vector3(maxWorldX, 0, 0));

		SetUpTop ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetUpTop () {
		GameObject tempAsteroid = (GameObject) Instantiate (asteroid, 
		                                       new Vector3 (minWorldX / 3, cam.orthographicSize / 2, 0),
		                                       Quaternion.identity);
	}

	void SetUpBot () {

	}

}
