using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LiftCutOut : MonoBehaviour {

	public Camera cam;
	Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		SetSize ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetSize () {
//		if (image) {
//			float worldScreenHeight = (float)(cam.orthographicSize * 2.0);
//			float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
//			//float newLocalScale = (worldScreenWidth / image.sprite.bounds.);
//			transform.localScale = new Vector3(newLocalScale, newLocalScale, 1);
//		}
	}
}
