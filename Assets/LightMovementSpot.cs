using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovementSpot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int w = Screen.width, h = Screen.height;
		float x = Input.mousePosition.x, y = Input.mousePosition.y;

		float xoffset = x - (w / 2.0f);
		float yoffset = y - (h / 2.0f);
		// float scale = 0.05f;
		float scale = 0.02f;

		transform.position = new Vector3(xoffset * scale, yoffset * scale, transform.position.z);

		// transform.LookAt(Vector3.zero); 

	}
}
