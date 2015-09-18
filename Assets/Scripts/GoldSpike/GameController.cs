using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour {

	public GameObject ship;
	Animator shipAnimator;
	AudioSource audioSource;

	public GameObject rightHand;
	public int interval = 20;
	public float minDist = 0.8f;
	LinkedList<float> zCoords = new LinkedList<float>();

	// Use this for initialization
	void Start () {
		shipAnimator = ship.gameObject.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource> ();


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Fire1"))
		{
			PlayAnimation();
		}
	}

	void FixedUpdate () {		
		float currCoord = rightHand.transform.localPosition.z;
		zCoords.AddLast (currCoord);
		if (zCoords.Count > interval) {
			zCoords.RemoveFirst();
		}

		float dist = currCoord - zCoords.ElementAt (0);
		//Debug.Log ("curr = " + currCoord + ", dist = " + dist);
		if (dist > minDist) {
			Debug.Log("THROWN!!!");
			PlayAnimation();
		}
	}

	void PlayAnimation () {
		//Debug.Log("on click");
		shipAnimator.SetTrigger("move");
		Invoke("playsound",0.2f);

	}

	void playsound(){
		audioSource.Play();

	}
}
