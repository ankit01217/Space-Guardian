using UnityEngine;
using System.Collections;

public class BossShip : MonoBehaviour {
	private GameObject planet;
	public string spaceShipType="boss";
	public int spaceShipLife;
	public float spaceShipSpeed;
	public GameObject normalShipPF, fastShipPF, shieldedShipPF, attackerShipPF;
	Vector3 spaceShipPosition;
	bool startRandomDir =false;
	public static string NORMAL = "normal";
	public static string FAST = "fasr";
	public static string SHIELDED = "shielded";
	public static string ATTACKER = "attacker";
	public static string BOSS = "boss";
	float movingcount=0;
	bool bossdirRight=true;
	bool freeze=false;
	private GameObject[] aliens;
	private Renderer rend;
	private Vector3 targetPosition;
	private AlienController alienController;
	private SpaceshipController spaceshipController;
	
	
	
	bool inScreen(Vector3 pos){
		//Debug.Log (pos);
		if (pos.x > -15 && pos.x < 15 && pos.y > -4 && pos.y < 8)
			
			return true;
		else
			return false;
		
		
	}
	
	void moveSpaceShip (){
		if (!freeze) {
			if (transform.position.y >= 8f) {
				gameObject.transform.position-=new Vector3(0,0.5f*spaceShipSpeed*Time.deltaTime,0);
				
			} else {
				if(bossdirRight){
					gameObject.transform.position+=new Vector3(spaceShipSpeed*Time.deltaTime,0,0);
					if(transform.position.x>16)bossdirRight=false;
				}else if(!bossdirRight){
					gameObject.transform.position-=new Vector3(spaceShipSpeed*Time.deltaTime,0,0);
					if(transform.position.x<-16)bossdirRight=true;
				}
				
			}


		}
		//gameObject.transform.Translate (-Vector3.up * spaceShipSpeed * 0.05f);
		
		//GameObject randAlien = aliens [Random.Range (0, aliens.Length)];
		//targetPosition = randAlien.transform.position;
		
		//Vector3 dir =  targetPosition - transform.position;
		//gameObject.transform.Translate(dir * Time.deltaTime, Space.World);

		
		/*
		Vector3 newpoas = new Vector3 ();
		newpoas.x = Mathf.Lerp (transform.position.x, targetPosition.x, spaceShipSpeed * Time.deltaTime);
		newpoas.y = Mathf.Lerp (transform.position.y, targetPosition.y, spaceShipSpeed * Time.deltaTime);
		newpoas.z = Mathf.Lerp (transform.position.z, targetPosition.z, spaceShipSpeed * Time.deltaTime);
		gameObject.transform.position = newpoas;
	 */
		
	}
	
	void asteroidAttackerAttack(){

		GameObject[] asteroid = GameObject.FindGameObjectsWithTag("Asteroid");
		if (asteroid.Length!= 0) {
			asteroid[Random.Range(0,asteroid.Length)].SendMessage("AsteroidHit");
			
		}
	
		
	}
	void genSpaceShip(){
		GameObject[] arr = new GameObject[]{normalShipPF, fastShipPF,shieldedShipPF, attackerShipPF};
		GameObject newSpaceShip=(GameObject)Instantiate (arr[Random.Range(0,arr.Length)], transform.position, Quaternion.identity);
		freeze = true;
	}
	void spaceShipGenerater(){
		freeze = true;
		Invoke ("genSpaceShip", 1f);
		Debug.Log("gen spaceship");
	}
	void functioningSpaceShip (){
			Debug.Log("start gen spaceship");
			InvokeRepeating("spaceShipGenerater",10f,3f);
			
	}
	void randomDirection(){
		//Vector2 dir=Random.insideUnitCircle;
		//float distance=Random.Range(0,1);
		//dir=dir*distance;
		Debug.Log("rotate!");
		if (startRandomDir && spaceShipType == ATTACKER && !inScreen (gameObject.transform.position)) {
			/*gameObject.transform.Rotate (new Vector3 (180, 0, 0));
			
			var lookPos = planet.transform.position - transform.position;
			lookPos.z = 18;
			var rotation = Quaternion.LookRotation (lookPos);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 0.5f);*/
			gameObject.transform.Rotate (new Vector3 (180, 0, 0));
			
		} else {
			gameObject.transform.Rotate (new Vector3 (Random.Range(0,360), 0, 0));
			
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
	void fixPosition(){
		//gameObject model = gameObject.GetComponent<SpaceshipModel> ();
		gameObject.transform.Rotate (new Vector3 (0, 0, 180));
		
	}
	void pointAtPlenet(){
		gameObject.transform.LookAt (new Vector3(planet.transform.position.x,planet.transform.position.y,18) );
		if (gameObject.transform.position.x < 0) {
			fixPosition();
		}
		
	}
	void setUpSpaceShip(){
		
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
		//pointAtPlenet ();
		
		
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
	
	void hitSpaceShip(int damage){
		spaceShipLife -= damage;
		if (spaceShipLife <= 0) {
			spaceshipController.onShipDestroyed();
			Destroy(gameObject);
			
		}
	}
	void hitPlenet(){
		Debug.Log ("hit plenet");
		alienController.killRandomAlien(this.transform.position);
		Destroy (this.gameObject);
		
	}
	
	
	// Use this for initialization
	
	void Start () {
		rend = GetComponentInChildren<Renderer> ();
		aliens = (GameObject[])GameObject.FindGameObjectsWithTag("Alien");
		alienController = GameObject.FindObjectOfType<AlienController>();
		spaceshipController = GameObject.FindObjectOfType<SpaceshipController>();
		
		//transform.rotation = Quaternion.identity;
		planet = GameObject.FindGameObjectWithTag("Planet");
		Random.seed = (int)System.DateTime.Now.Ticks;
		setUpSpaceShip();
		functioningSpaceShip ();
		
	}
	
	void Update () {
		moveSpaceShip ();
		
		
		
		
		
	}
	void OnTriggerEnter(Collider other){
		Debug.Log ("OnTriggerEnter");
		
		if (other.gameObject.tag == "Planet") {
			Invoke("hitPlenet",0.1f);
		}
	}
}




