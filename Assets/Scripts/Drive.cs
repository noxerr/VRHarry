using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour {
    public GameObject Camera, avatarMounted;
    //[TextArea]
    [Header("Driving Settings")]
    [Tooltip("true means you look where you want to go, false works with experienced kind of movements")]
    //public string Notes = "This component shouldn't be removed, it does important stuff.";
    public bool driveConMirada = false;
    public bool hasBroom = false;
    public float maxSpeed = 8;
    public Text testDebug;

    public Rigidbody rb;

    

    private float LowPassFilterFactor = 0.8f;
    private Vector3 lowPassValue = Vector3.zero;
    private Gyroscope gyro;
    private Vector3 gyroAccel, accel2;

    private bool moving = false;
    [Space(10)]
    public float movingThresHold = 0.2f;
    public float timeColddownLastmove = 0.5f;

    private float lastChangeOfMov = 0;


	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
        gyro = Input.gyro;
	}


	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.D)) moving = !moving;
        if (hasBroom)
        {
            avatarMounted.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, avatarMounted.transform.rotation, 0.9f);
            gyroAccel = gyro.userAcceleration;
            accel2 = Input.acceleration;
            if (lastChangeOfMov > 0) lastChangeOfMov -= Time.deltaTime;
            if (moving)
            {
                if (gyroAccel.z < -movingThresHold && lastChangeOfMov <= 0)
                {
                    moving = false;
                    lastChangeOfMov = timeColddownLastmove;
                    rb.velocity = Vector3.zero;
                }
                else if (driveConMirada) rb.velocity = avatarMounted.transform.forward * maxSpeed; //rb.velocity = Camera.transform.forward * maxSpeed;
                testDebug.text = "gyrAccel: " + accel2 + " --grav: " + gyro.gravity;

            }
            else //speed up?
            {
                if (gyroAccel.z > movingThresHold && lastChangeOfMov <= 0)
                {
                    moving = true;
                    lastChangeOfMov = timeColddownLastmove;
                    rb.velocity = avatarMounted.transform.forward * maxSpeed; //rb.velocity = Camera.transform.forward
                } 
                testDebug.text = "gyrAccel: " + accel2 + " --grav: " + gyro.gravity;
            }
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
