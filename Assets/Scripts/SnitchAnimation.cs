using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchAnimation : MonoBehaviour {
	public GameObject leftWing;
	public GameObject rightWing;

	public float maxAngle = 30.0f;

	public float animationSpeed = 200.0f;

	private float currentAngle;
	public bool downwards; //motion
	// Use this for initialization
	void Start () {
		currentAngle = 0.0f;
		downwards = true;
	}
	
	// Update is called once per frame
	void Update () {
		float toMove = animationSpeed * Time.deltaTime;
		if (downwards) {
			currentAngle += toMove;
			leftWing.transform.Rotate (new Vector3 (0, 0,toMove));
			rightWing.transform.Rotate(new Vector3 (0, 0,-toMove));
			if (currentAngle > maxAngle)
				downwards = false;
		} else {
			currentAngle -= toMove;
			leftWing.transform.Rotate (new Vector3 (0, 0, -toMove));
			rightWing.transform.Rotate(new Vector3 (0, 0, toMove));
			if (currentAngle < -maxAngle)
				downwards = true;
		}
	}
}
