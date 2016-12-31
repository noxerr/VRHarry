using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarniaDoorRotation : MonoBehaviour {
	private Vector3 hingePoint;
	// Use this for initialization
	void Start () {
		hingePoint = this.transform.GetChild(0).position;
		rotateDoor();
	}
	void rotateDoor(){
		transform.RotateAround (hingePoint, new Vector3 (0, 1, 0), 90);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
