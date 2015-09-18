using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

	public float speed = 5f;
	public float yMoveRange = 5f;

	private float startPosY;
	// Use this for initialization
	void Awake(){
		
	
	}

	void Start () {
		startPosY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {


		transform.Rotate(Vector3.up * Time.deltaTime* speed, Space.World);
		//Vector3 curPos = transform.position;
		//transform.eulerAngles = Vector3.Lerp (new Vector3(curPos.x,curPos.y - 5f,curPos.z), new Vector3(curPos.x,curPos.y + 5f,curPos.z), 20f);


	}
}
