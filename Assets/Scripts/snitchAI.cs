using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snitchAI : MonoBehaviour {

	public float speed;
	public Camera playerCamera;

	private Vector3 playerPos;

	// Use this for initialization
	void Start () {
		playerPos = playerCamera.transform.position;
	}

	void moveAway(){
		float step = speed * Time.deltaTime;
		Vector3 heading = playerPos - transform.position;
		// divisions are expensive.
		transform.position += (step * heading) / heading.magnitude;
	}
	// Update is called once per frame
	void Update () {
		playerPos = playerCamera.transform.position;
		moveAway ();
	}
}
