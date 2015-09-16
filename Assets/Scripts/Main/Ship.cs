using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	private GameObject planet;
	public string spaceShipType;
	public int spaceShipLife;
	public float spaceShipSpeed;
	//public GameObject normalShipPF, fastShipPF, shieldedShipPF, attackerShipPF;
	Vector3 spaceShipPosition;
	bool startRandomDir = false;
	public static string NORMAL = "normal";
	public static string FAST = "fasr";
	public static string SHIELDED = "shielded";
	public static string ATTACKER = "attacker";
	public static string BOSS = "boss";
	private GameObject[] aliens;
	private Renderer rend;
	private Vector3 targetPosition;
	private AlienController alienController;
	private SpaceshipController spaceshipController;
	private AudioSource audioSource;
	public AudioClip[] shipClips;
	private bool isInToScreen = false;
	//public AudioSource a[3];

	bool inScreen (Vector3 pos)
	{
		//Debug.Log (pos);
		if (pos.x > -15 && pos.x < 15 && pos.y > -4 && pos.y < 8)

			return true;
		else
			return false;
		
		
	}
	
	void moveSpaceShip ()
	{
		
		//gameObject.transform.Translate (-Vector3.up * spaceShipSpeed * 0.05f);

		//GameObject randAlien = aliens [Random.Range (0, aliens.Length)];
		//targetPosition = randAlien.transform.position;

		//Vector3 dir =  targetPosition - transform.position;
		//gameObject.transform.Translate(dir * Time.deltaTime, Space.World);
		gameObject.transform.Translate (Vector3.forward * spaceShipSpeed * 0.05f);

		/*
		Vector3 newpoas = new Vector3 ();
		newpoas.x = Mathf.Lerp (transform.position.x, targetPosition.x, spaceShipSpeed * Time.deltaTime);
		newpoas.y = Mathf.Lerp (transform.position.y, targetPosition.y, spaceShipSpeed * Time.deltaTime);
		newpoas.z = Mathf.Lerp (transform.position.z, targetPosition.z, spaceShipSpeed * Time.deltaTime);
		gameObject.transform.position = newpoas;
	 */

		if (startRandomDir == false && spaceShipType == ATTACKER && inScreen (gameObject.transform.position)) {
			startRandomDir = true;
			InvokeRepeating ("randomDirection", 3f, 2f);
		}
		
	}
	
	void asteroidAttackerAttack ()
	{
		GameObject[] asteroid = GameObject.FindGameObjectsWithTag ("Asteroid");
		if (asteroid.Length != 0) {
			asteroid [Random.Range (0, asteroid.Length)].SendMessage ("AsteroidHit");

		}
		
	}
/*	void spaceShipGenerater(){
		GameObject[] arr = new GameObject[]{normalShipPF, fastShipPF,shieldedShipPF, attackerShipPF};
		GameObject newSpaceShip=(GameObject)Instantiate (arr[Random.Range(0,arr.Length)], getRandPosition (), Quaternion.identity);
	}*/
	void functioningSpaceShip ()
	{
		switch (spaceShipType) {
		case "attacker":
			//Invoke("asteroidAttackerAttack",3f);
			InvokeRepeating ("asteroidAttackerAttack", 5f, 3f);
			break;
		/*	case "boss":

			InvokeRepeating("spaceShipGenerater",5f,3f);
			break;
	*/		
		}
	}
	void randomDirection ()
	{
		//Vector2 dir=Random.insideUnitCircle;
		//float distance=Random.Range(0,1);
		//dir=dir*distance;
		Debug.Log ("rotate!");
		if (startRandomDir && spaceShipType == ATTACKER && !inScreen (gameObject.transform.position)) {
			/*gameObject.transform.Rotate (new Vector3 (180, 0, 0));
			
			var lookPos = planet.transform.position - transform.position;
			lookPos.z = 18;
			var rotation = Quaternion.LookRotation (lookPos);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 0.5f);*/
			gameObject.transform.Rotate (new Vector3 (180, 0, 0));

		} else {
			gameObject.transform.Rotate (new Vector3 (Random.Range (0, 360), 0, 0));

		}

		//gameObject.transform.Rotate (new Vector3 (Random.Range(0,360), 0, 0));
		//Vector3.savepos = gameObject.transform.position;

		//gameObject.transform.Translate (Vector3.forward * spaceShipSpeed * 6f);


		/*Vector3 temppos=gameObject.transform.position+new Vector3(dir.x,dir.y,0);
		if(inScreen(temppos)){
			//Debug.Log (ATTACKER);
			gameObject.transform.LookAt (temppos);
			//gameObject.transform.position=temppos;
		}else{
			gameObject.transform.LookAt (targetPosition);
			//gameObject.transform.Translate (Vector3.forward * spaceShipSpeed * 0.05f);
		}*/
	}
	void fixPosition ()
	{
		//gameObject model = gameObject.GetComponent<SpaceshipModel> ();
		gameObject.transform.Rotate (new Vector3 (0, 0, 180));

	}
	void pointAtPlanet ()
	{
		gameObject.transform.LookAt (new Vector3 (planet.transform.position.x, planet.transform.position.y, 18));
		if (gameObject.transform.position.x < 0) {
			fixPosition ();
		}

	}
	void setUpSpaceShip ()
	{

		//gameObject.transform.LookAt (planet.transform.position);
		//gameObject.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
		//gameObject.transform.RotateAround (Vector3.forward, 20*);
/*
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
*/
		pointAtPlanet ();


		/*
		switch (spaceShipType) {
		case "normal":
			spaceShipLife=2;
			spaceShipSpeed=1;
			break;
		case "attacker":
			spaceShipLife=1;
			spaceShipSpeed=0.5f;
			//InvokeRepeating("randomDirection",3f,2f);
			break;
		case "shielded":
			spaceShipLife=3;
			spaceShipSpeed=1;
			break;
		case "fast":
			spaceShipLife=1;
			spaceShipSpeed=2;
			break;
		}*/

	}
	
	void hitSpaceShip ()
	{
			spaceshipController.onShipDestroyed ();
			Destroy (gameObject);
	}
	void hitPlanet ()
	{
		Debug.Log ("hit planet");
		alienController.killRandomAlien (this.transform.position);
		Destroy (this.gameObject);

	}


	// Use this for initialization

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		rend = GetComponentInChildren<Renderer> ();
		aliens = (GameObject[])GameObject.FindGameObjectsWithTag ("Alien");
		alienController = GameObject.FindObjectOfType<AlienController> ();
		spaceshipController = GameObject.FindObjectOfType<SpaceshipController> ();

		//transform.rotation = Quaternion.identity;
		planet = GameObject.FindGameObjectWithTag ("Planet");
		Random.seed = (int)System.DateTime.Now.Ticks;
		setUpSpaceShip ();
		functioningSpaceShip ();


		float curScale = this.transform.localScale.x;
		LeanTween.scale( this.gameObject, new Vector3 (curScale + 0.05f, curScale + 0.05f, curScale + 0.05f), 0.25f).setEase(LeanTweenType.easeOutCirc).setLoopPingPong(-1);

	}
	
	void Update ()
	{

		if (rend && rend.isVisible && !isInToScreen) {

			audioSource.PlayOneShot (shipClips [Random.Range (0, 3)]);
			isInToScreen = true;
			//		AudioSource tmp=Random
		}
		moveSpaceShip ();



		

	}
	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("OnTriggerEnter");

		if (other.gameObject.tag == "Planet") {
			Invoke ("hitPlanet", 0.1f);
		}
	}
}




