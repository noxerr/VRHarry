using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snitchAI : MonoBehaviour {

	public float speed;
	public Camera playerCamera;
	private Vector3 Max = new Vector3(247,21,369);
	private Vector3 Min = new Vector3(-23,-59,228);
	private Vector3 playerPos;

	/*Good v0.1 Min Max positions:
		Max:	247		21		369
		Min:	-23		-59		228
	*/
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
	//Check if snitch is inside the pitch
	bool insideBoundaries(){
		Vector3 snitchPos = transform.position;
		if (snitchPos.x + 1 > Max.x || snitchPos.x - 1 < Min.x)
			return false;
		if (snitchPos.y + 1 > Max.y || snitchPos.y - 1 < Min.y)
			return false;
		if (snitchPos.z + 1> Max.z || snitchPos.z - 1 < Min.z)
			return false;
		return true;
	}
	// Update is called once per frame
	void Update () {
		playerPos = playerCamera.transform.position;
		if (insideBoundaries()) moveAway ();
	}
}
