﻿using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

	Animator animator;
	AudioSource audioSource;
	public AudioClip deathClip;

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
		animator.SetTrigger("Die");
		Invoke("playDeathSound", 0.1f);
	}

	void playDeathSound(){
		audioSource.clip = deathClip;
		audioSource.Play();
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
