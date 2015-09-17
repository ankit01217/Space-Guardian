using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.Play ("ShipMove");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		Destroy (gameObject, 0.1f);
	}
}
