using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {
    public GameObject Camera;
    //[TextArea]
    [Header("Driving Settings")]
    [Tooltip("true means you look where you want to go, false works with experienced kind of movements")]
    //public string Notes = "This component shouldn't be removed, it does important stuff.";
    public bool driveConMirada = false;
    public bool hasBroom = false;

    private float LowPassFilterFactor = 0.8f;
    private Vector3 lowPassValue = Vector3.zero;

    private Gyroscope gyro;
    private Rigidbody rb;
    private Vector3 gyroAccel, movingDir;

    private bool moving = false;
    public float movingThresHold = 0.4f;


	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
        gyro = Input.gyro;
        rb = GetComponent<Rigidbody>();
        movingDir = Vector3.zero;
	}


	// Update is called once per frame
	void FixedUpdate () {
        if (hasBroom)
        {
            gyroAccel = gyro.userAcceleration;
            if (moving)
            {
                if (gyroAccel.z < -movingThresHold) moving = false;
                else if (driveConMirada) movingDir = Camera.transform.forward;
            }
            else
            {
                //aceleramos?
                if (gyroAccel.z > movingThresHold) moving = true;

            }

            //aplicar fuerzas

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
