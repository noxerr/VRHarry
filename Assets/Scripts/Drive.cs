using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {
    private float LowPassFilterFactor = 0.8f;
    private Vector3 lowPassValue = Vector3.zero;
    private Gyroscope gyro; 

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
        gyro = Input.gyro;
	}
	
	// Update is called once per frame
	void Update () {
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
