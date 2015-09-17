using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

	Animator animator;
	AudioSource audioSource;
	public AudioClip deathClip, explosionClip;
	public GameObject waterExplosionPF;
	private GameObject waterExplosion;

	// Use this for initialization
	void Awake(){

		audioSource = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		startAlienAnimation ();

	}

	void Start () {
	}


	void startAlienAnimation(){
		animator.SetTrigger("Move");
	
	}

	public void die(){

		//animator.SetTrigger("Die");
		Invoke("removeAlien", 0.7f);

		audioSource.PlayOneShot (explosionClip);
		waterExplosion = (GameObject)Instantiate (waterExplosionPF, transform.position, Quaternion.identity);
		//waterExplosion.transform.parent = this.gameObject.transform;
		Animator anim = waterExplosion.GetComponent<Animator> ();


		audioSource.PlayOneShot (deathClip);
		Vector3 pos = transform.position;
		pos.z = -18;
		this.transform.position = pos;
	}


	public void OnDeathAnimComplete(){
		Debug.Log ("OnDeathAnimComplete");
		Invoke("removeAlien", 0.3f);

	}

	void removeAlien(){
		Destroy(waterExplosion);
		Destroy (this.gameObject);	
	
	}


	// Update is called once per frame
	void Update () {
	
	}
}
