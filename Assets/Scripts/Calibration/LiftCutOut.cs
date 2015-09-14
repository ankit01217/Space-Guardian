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
		if (image) {

			//float newLocalScale = (cam.orthographicSize*2 / image.sprite.bounds.size.y);
			//transform.localScale = new Vector3(newLocalScale, newLocalScale, 1);
		}
	}
}
