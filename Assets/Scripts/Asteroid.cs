using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public bool thrown = false;
	public int dmgPoints = 2;	// how much damage the asteroid does

	// Use this for initialization
	void Start () {

	}

	void SetParams (int dmg) {
		dmgPoints = dmg;

		// Set model based on start dmg points

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Spaceship") {
			Debug.Log("Spaceship hit!");

			//dmgPoints--;

			// Play animation/change state


			// Destroy asteroid
			//if(dmgPoints == 0) {
				Destroy(gameObject);
			//}
		}
	}

	void AsteroidHit () {
		Destroy(gameObject);
	}
}
