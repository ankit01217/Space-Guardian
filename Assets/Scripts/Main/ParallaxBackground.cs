using UnityEngine;
using System.Collections;

public class ParallaxBackground : MonoBehaviour {

	public GameObject bg1,bg2;
	private Renderer r1,r2;
	public float speed = 15f;
	// Use this for initialization
	void Start () {
		r1 = bg1.GetComponent<Renderer>();
		r2 = bg2.GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {
	
		bg1.transform.Translate (Vector3.left*Time.deltaTime*speed);
		bg2.transform.Translate (Vector3.left*Time.deltaTime*speed);


		if (bg1.transform.position.x <= -57) {

			Vector3 curPos = bg1.transform.position;
			curPos.x = 57;
			bg1.transform.position  = curPos;
		
		}

		if (bg2.transform.position.x <= -57) {
			
			Vector3 curPos = bg2.transform.position;
			curPos.x = 57;
			bg2.transform.position  = curPos;
			
		}
	}


}
