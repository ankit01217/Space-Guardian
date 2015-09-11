using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public bool thrown = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Spaceship") {
			Debug.Log("Spaceship hit!");

			// Play animation


			// Destroy asteroid
			// TODO: remember to remove reference from data structure of asteroids if needed
			Destroy(gameObject);
		}
	}
}
