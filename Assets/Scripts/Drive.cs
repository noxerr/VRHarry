﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {
    public GameObject Camera, avatarMounted;
    //[TextArea]
    [Header("Driving Settings")]
    [Tooltip("true means you look where you want to go, false works with experienced kind of movements")]
    //public string Notes = "This component shouldn't be removed, it does important stuff.";
    public bool driveConMirada = false;
    public bool hasBroom = false;
    public float maxSpeed = 8;

    private float LowPassFilterFactor = 0.8f;
    private Vector3 lowPassValue = Vector3.zero;
    private Gyroscope gyro;
    private Rigidbody rb;
    private Vector3 gyroAccel, movingDir;

    private bool moving = false;
    public float movingThresHold = 0.1f;

    private float lastChangeOfMov = 0;


	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
        gyro = Input.gyro;
        rb = Camera.GetComponent<Rigidbody>();
        movingDir = Vector3.zero;
        //startRotation = avatarMounted.transform.rotation.eulerAngles;
        //aimRotation = new Vector3();
	}


	// Update is called once per frame
	void FixedUpdate () {
        if (hasBroom)
        {
            gyroAccel = gyro.userAcceleration;
            if (lastChangeOfMov > 0) lastChangeOfMov -= Time.deltaTime;
            if (moving)
            {
                if (gyroAccel.z < -movingThresHold && lastChangeOfMov <= 0) { moving = false; lastChangeOfMov = 0.3f; movingDir = Vector3.zero; }
                else if (driveConMirada) movingDir = Camera.transform.forward;

                //aplicar fuerzas
                //rb.AddForce(movingDir, ForceMode.VelocityChange);
                rb.velocity = movingDir*maxSpeed;
            }
            else
            {
                //aceleramos?
                if (gyroAccel.z > movingThresHold && lastChangeOfMov <= 0) { moving = true; lastChangeOfMov = 0.3f; }
                rb.velocity = Vector3.zero;
            }
            avatarMounted.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, avatarMounted.transform.rotation, 0.9f);

        }
        
        /*AccelerationEvent[] events = Input.accelerationEvents;
        for (int i = 0; i < events.Length; i++)
        {
            Debug.LogError("Accel: " + events[i].acceleration);
        }
        Debug.LogError("Gyro accel: " + gyro.userAcceleration + " - Rotation: " + Quaternion.ToEulerAngles(gyro.attitude));*/
	}

    Vector3 lowpass()
    {
        //float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable
        lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
        return lowPassValue;
    }
}
