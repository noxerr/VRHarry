using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePublic : MonoBehaviour {

    //public float AccelerometerUpdateInterval = 1.0f / 100.0f;
    //public float LowPassKernelWidthInSeconds = 0.001f;
    private float LowPassFilterFactor = 0.8f;
    private Vector3 lowPassValue = Vector3.zero;
    private Gyroscope gyro; 

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
        gyro = Input.gyro;
	}


    Vector3 lowpass()
    {
        //float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable
        lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
        return lowPassValue;
    }

	
	// Update is called once per frame
	void Update () {
        AccelerationEvent[] events = Input.accelerationEvents;
        for (int i = 0; i < events.Length; i++)
        {
            Debug.LogError("Accel: " + events[i].acceleration);
        }
        Debug.LogError("Gyro accel: " + gyro.userAcceleration + " - Rotation: " + Quaternion.ToEulerAngles(gyro.attitude));
	}
}
