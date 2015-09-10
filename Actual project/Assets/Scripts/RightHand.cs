using UnityEngine;
using System.Collections;

public class RightHand : MonoBehaviour {

	public GameObject rightHand;

	bool pickedUp = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// Only link it if player hasn't picked up an asteroid
		if (!pickedUp) {
			LinkHandToDetector ();
		}
	}

	// Link the x and y-coords of player's right hand to the collision detector
	void LinkHandToDetector () {
		Vector3 pos = new Vector3(rightHand.transform.position.x,
		                          rightHand.transform.position.y,
		                          transform.position.z);
		transform.position = pos;
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log (other.gameObject);

		// pick up asteroid and hold it in place
		other.transform.position = transform.position;
		pickedUp = true;
	}
}
