﻿using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	private GameObject planet;
	public string spaceShipType;
	public int spaceShipLife;
	public float spaceShipSpeed;

	Vector3 spaceShipPosition;
	bool startRandomDir =false;
	public static string NORMAL = "normal";
	public static string FAST = "fasr";
	public static string SHIELDED = "shielded";
	public static string ATTACKER = "attacker";
	SpaceshipController spaceshipGenerator;

	private GameObject[] aliens;
	private Vector3 targetPosition;

	bool inScreen(Vector3 pos){
		//Debug.Log (pos);
		if (pos.x > -18 && pos.x < 18 && pos.y > -8 && pos.y < 8)
			return true;
		else
			return false;
		
		
	}
	
	void moveSpaceShip (){
		
		//gameObject.transform.Translate (-Vector3.up * spaceShipSpeed * 0.05f);

		GameObject randAlien = aliens [Random.Range (0, aliens.Length)];
		targetPosition = randAlien.transform.position;

		Vector3 dir =  targetPosition - transform.position;
		gameObject.transform.Translate(dir * Time.deltaTime, Space.World);

		/*
		Vector3 newpoas = new Vector3 ();
		newpoas.x = Mathf.Lerp (transform.position.x, targetPosition.x, spaceShipSpeed * Time.deltaTime);
		newpoas.y = Mathf.Lerp (transform.position.y, targetPosition.y, spaceShipSpeed * Time.deltaTime);
		newpoas.z = Mathf.Lerp (transform.position.z, targetPosition.z, spaceShipSpeed * Time.deltaTime);
		gameObject.transform.position = newpoas;
	 */

		if(startRandomDir==false&&spaceShipType==ATTACKER && inScreen(gameObject.transform.position)){
			startRandomDir=true;
			InvokeRepeating("randomDirection",3f,2f);
		}
		
	}
	
	void asteroidAttackerAttack(){
		GameObject[] asteroid = GameObject.FindGameObjectsWithTag("Asteroid");
		if (asteroid.Length!= 0) {
			asteroid[Random.Range(0,asteroid.Length)].SendMessage("AsteroidHit");

		}
		
	}
	
	void functioningSpaceShip (){
		switch (spaceShipType) {
		case "attacker":
			//Invoke("asteroidAttackerAttack",3f);
			InvokeRepeating("asteroidAttackerAttack",5f,3f);
			break;
			
		}
	}
	void randomDirection(){
		Vector2 dir=Random.insideUnitCircle;
		//float distance=Random.Range(0,1);
		//dir=dir*distance;
		Vector3 temppos=gameObject.transform.position+new Vector3(dir.x,dir.y,0);
		if(inScreen(temppos)){
			Debug.Log (ATTACKER);
			gameObject.transform.LookAt (temppos);
			//gameObject.transform.position=temppos;
		}else{
			gameObject.transform.LookAt (targetPosition);
			//gameObject.transform.Translate (Vector3.forward * spaceShipSpeed * 0.05f);
		}
	}
	void setUpSpaceShip(){

		//gameObject.transform.LookAt (planet.transform.position);
		//gameObject.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
		//gameObject.transform.RotateAround (Vector3.forward, 20*);

		if (transform.position.x < -12) {
			transform.Rotate( new Vector3(0,0,1),35);

		}else if (transform.position.x < 0) {
			transform.Rotate( new Vector3(0,0,1),20);
			
		}
		else if (transform.position.x > 12) {
			transform.Rotate( new Vector3(0,0,1),-35);

		}
		else if (transform.position.x > 0) {
			transform.Rotate( new Vector3(0,0,1),-20);
			
		}


	}
	
	void hitSpaceShip(){
		///////
		if (spaceShipLife == 0) {
			
		}
	}
	void hitPlenet(){
		Debug.Log ("hit plenet");

		spaceshipGenerator.onShipDestroyed ();
		Destroy (this.gameObject);


	}

	void hitAlien(GameObject alien){
		Debug.Log("hit alien");
		spaceshipGenerator.onShipDestroyed ();
		Destroy (this.gameObject);
		Alien alienSc = alien.GetComponent<Alien> ();
		alienSc.die ();
	

	}
	// Use this for initialization
	
	void Start () {

		spaceshipGenerator = GameObject.FindObjectOfType<SpaceshipController> ();

		aliens = (GameObject[])GameObject.FindGameObjectsWithTag("Alien");
		transform.rotation = Quaternion.identity;
		planet = GameObject.FindGameObjectWithTag("Planet");
		Random.seed = (int)System.DateTime.Now.Ticks;
		setUpSpaceShip();

	}
	
	void Update () {
		moveSpaceShip ();
		functioningSpaceShip ();


	}

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Collider>().tag == "Asteroid") {
			hitSpaceShip();
		}else if (other.gameObject.tag == "Alien") {
			hitAlien(other.gameObject);
		}
		else if (other.gameObject.tag == "Planet") {
			hitPlenet();
		}
	}
}




