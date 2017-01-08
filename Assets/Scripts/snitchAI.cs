using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snitchAI : MonoBehaviour {

	public float speed;
	public Camera playerCamera;
	public GameObject Path;
	public float distanceToPoint = 1f;
    [HideInInspector]
    public bool isActive = false, isAnimating = false;
    [HideInInspector]
    public AllLogics logic;
	private Transform[] points;
	private int currentPos;
	private Vector3 Max = new Vector3(247,21,369);
	private Vector3 Min = new Vector3(-23,-59,228);
	private Vector3 playerPos;
    private float distPlayerSnitch;
    private Vector3 snitchPos;
    private bool animatedWings = false;
    private float step;

	/*Good v0.1 Min Max positions:
		Max:	247		21		369
		Min:	-23		-59		228
	*/
	// Use this for initialization
	void Start () {
		playerPos = playerCamera.transform.position;
		points = Path.GetComponentsInChildren<Transform>();
		currentPos = 1;
	}

	void moveAway(){
		step = speed * Time.deltaTime;
		Vector3 heading = playerPos - transform.position;
		// divisions are expensive.
		transform.position -= (step * heading) / heading.magnitude;
	}
	//Check if snitch is inside the pitch
	bool insideBoundaries(){
		snitchPos = transform.position;
		if (snitchPos.x > Max.x || snitchPos.x < Min.x)
			return false;
		if (snitchPos.y > Max.y || snitchPos.y < Min.y)
			return false;
		if (snitchPos.z > Max.z || snitchPos.z < Min.z)
			return false;
		return true;
	}

	void getNextPoint(){
		++currentPos;
		if (currentPos >= points.Length)
			currentPos = 1;
	}
	void followPath(){
		if (Vector3.Distance (points [currentPos].position, transform.position) < distanceToPoint)
			getNextPoint ();
		//transform.position = Vector3.MoveTowards (transform.position, position, speed * Time.deltaTime);

		step = speed * Time.deltaTime;
        Vector3 heading = points[currentPos].position - transform.position;
		// divisions are expensive.
		transform.position += (step * heading) / heading.magnitude;
	}
	// Update is called once per frame
	void FixedUpdate () {
        if (isActive)
        {
            distPlayerSnitch = Vector3.Distance(playerCamera.transform.position, transform.position);
            if (distPlayerSnitch > 10)
                followPath();
            else if (distPlayerSnitch < 0.6f)
            {
                if (logic != null) logic.snitchCatched = true;
                isActive = false;
            }
            else if (insideBoundaries())
                moveAway();
            //if (insideBoundaries()) moveAway ();
        }
        /*else if (isAnimating)
        {

        }*/
	}
}
