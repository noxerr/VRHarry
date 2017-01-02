using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarniaDoorRotation : MonoBehaviour {
    public float secsForEachAngle = 0.008f;
    private float secsToLoadScene = 1.5f;
    public GameObject slider;
	private Vector3 hingePoint;
    private bool moveDoor;
    private float timeMoving;
    private float angleStep;
    private float angleFinal;
    private bool loadSlider = false;
    private float secsToLoad;
    private Image sliderImage;
	// Use this for initialization
	void Start () {
        moveDoor = false;
		hingePoint = this.transform.GetChild(0).position;
        angleFinal = 0;
        angleStep = 0;
        secsToLoad = secsToLoadScene;
        sliderImage = slider.GetComponent<Image>();
		//rotateDoor(90);
	}
	public void rotateDoor(float angle = 90){
		//transform.RotateAround (hingePoint, new Vector3 (0, 1, 0), angle);
        angleStep = angle - transform.localEulerAngles.y;
        //if (transform.localEulerAngles.y > 90 && transform.localEulerAngles.y != 0
        timeMoving = Mathf.Abs(angleStep * secsForEachAngle);
        angleFinal = angle;
        if (angle == 90)
        {
            loadSlider = true;
            slider.SetActive(true);
        }
        else
        {
            loadSlider = false;
            secsToLoad = secsToLoadScene;
            slider.SetActive(false);
            sliderImage.fillAmount = 0;
        }
        moveDoor = true;
	}
	// Update is called once per frame
	void FixedUpdate ()
    {
        #region moveDoor
        if (moveDoor)
        {
            if (timeMoving > 0)
            {
                if (angleStep < 0) { //se cierra la puerta 
                    if (transform.localEulerAngles.y > 0) { //aun no se ha cerado
                        transform.RotateAround(hingePoint, new Vector3(0, 1, 0), Time.deltaTime / -secsForEachAngle);
                        if (transform.localEulerAngles.y < 0 || transform.localEulerAngles.y > 90)
                            transform.RotateAround(hingePoint, new Vector3(0, 1, 0), 0.03f - transform.localEulerAngles.y);
                    }
                }
                else { //se abre
                    if (transform.localEulerAngles.y < 90) { //aun no se ha abierto
                        transform.RotateAround(hingePoint, new Vector3(0, 1, 0), Time.deltaTime / secsForEachAngle);
                        if (transform.localEulerAngles.y > 90)
                            transform.RotateAround(hingePoint, new Vector3(0, 1, 0), 89.93f - transform.localEulerAngles.y);
                    }
                }
                timeMoving -= Time.deltaTime;
            }
            else moveDoor = false;
        }
        #endregion

        #region slider
        if (loadSlider)
        {
            if (secsToLoad > 0) secsToLoad -= Time.deltaTime;
            sliderImage.fillAmount += 1.0f / secsToLoadScene * Time.deltaTime;// *secsToLoad;
        }
        #endregion
    }
}
