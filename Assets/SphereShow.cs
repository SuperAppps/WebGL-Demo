using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereShow : MonoBehaviour {

	public float sphereSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Slowly rotate the object arond its X axis at 1 degree/second.
		transform.Rotate(Vector3.right * Time.deltaTime * sphereSpeed);

		// ... at the same time as spinning relative to the global 
		// Y axis at the same speed.
		transform.Rotate(Vector3.up * Time.deltaTime * sphereSpeed, Space.World);
	}
}
