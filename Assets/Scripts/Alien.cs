using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

	Animator animator;
	// Use this for initialization
	void Awake(){
		animator = GetComponent<Animator> ();
		Invoke("sartAlienAnimation", Random.Range(0,3));


	}

	void Start () {
	
	}


	void sartAlienAnimation(){
		animator.SetTrigger("Move");
	
	}

	public void die(){

		animator.SetTrigger("Die");
		//Invoke("removeAlien", 3);

	}

	public void OnDeathAnimComplete(){
		Debug.Log ("OnDeathAnimComplete");
		removeAlien();
	}

	void removeAlien(){
		Destroy (this.gameObject);	
	
	}


	// Update is called once per frame
	void Update () {
	
	}
}
