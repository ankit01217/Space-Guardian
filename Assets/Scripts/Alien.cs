using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

	Animator animator;
	AudioSource audioSource;
	public AudioClip deathClip, explosionClip;
	public ParticleSystem planetPS;

	// Use this for initialization
	void Awake(){

		audioSource = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		Invoke("sartAlienAnimation", Random.Range(0,3));


	}

	void Start () {
	
	}


	void sartAlienAnimation(){
		animator.SetTrigger("Move");
	
	}

	public void die(){

		//animator.SetTrigger("Die");
		Invoke("removeAlien", 2f);

		audioSource.PlayOneShot (explosionClip);
		ParticleSystem ps = (ParticleSystem)Instantiate (planetPS, transform.position, Quaternion.identity);
		ps.enableEmission = true;
		ps.Play();

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
		Destroy (this.gameObject);	
	
	}


	// Update is called once per frame
	void Update () {
	
	}
}
