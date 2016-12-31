using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarniaDoorRotation : MonoBehaviour {
	private Vector3 hingePoint;
	// Use this for initialization
	void Start () {
		hingePoint = this.transform.GetChild(0).position;
		//rotateDoor(90);
	}
	public void rotateDoor(float angle = 90){
		transform.RotateAround (hingePoint, new Vector3 (0, 1, 0), angle);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
