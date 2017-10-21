using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public GameObject Camera;
    private Vector3 diffPos;
	// Use this for initialization
	void Start () {
        diffPos = Camera.transform.localPosition - transform.localPosition;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.localPosition = Camera.transform.localPosition - diffPos;
	}
}
