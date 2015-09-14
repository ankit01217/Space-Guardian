using UnityEngine;
using System.Collections;

public class CrosshairController : MonoBehaviour {

	public GameObject leftHand;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		LinkToHand ();
	}

	// Link the x and y-coords of the crosshair to that of the player's left hand
	void LinkToHand () {
		Vector3 pos = new Vector3(leftHand.transform.position.x,
		                          leftHand.transform.position.y,
		                          transform.position.z);
		transform.position = pos;
	}
	

}
