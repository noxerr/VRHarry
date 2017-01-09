using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialLogic : AllLogics {
    public Animator animator;
    private Text textField;
    public Drive driveScript;
    public float secsForEachSentence = 3.5f;
    public GameObject[] reticleObjects;
    public GameObject reticle, slider, snitch, avatarStanding, avatarMounted, broom;
    private float elapsedTimeSinceLast = 0, extraTime = 0;
    private int stringIndex = 0, frasesLength;
    private bool condition, loadSlider = false;
    private int animateDir = 0; //0 no, 4 forward, 8 backwards
    private float animateTransition = 0; //0 standing on broom, 1 leaning forward
    private Image sliderImage;
    private GameObject wing1, wing2;

    private string[] Frases = new string[]{
        "Welcome to the Tutorial", //0
        "I'm going to show you\nSome basic movements", //1
        "apply a hard force forward", //2
        "the harder the force is,\nthe faster you\'ll go", //3
        "you can apply force twice,\nwith delay, to go faster", //4
        "do the opposite to stop", //5
        "you can also look around,\nlike me", //6
        "you can also look around,\nlike me", //7 -- to stop looping lookAround
        "Finally, your goal is\nto catch the snitch", //8
        "Finally, your goal is\nto catch the snitch", //9
        "Now, look at your broom", //10
    };

	// Use this for initialization
	void Start () {
        textField = GetComponent<Text>();
        textField.text = Frases[stringIndex];
        stringIndex++;
        extraTime = 2.5f;
        frasesLength = Frases.Length;
        condition = true;
        sliderImage = slider.GetComponent<Image>();
        UnityEngine.VR.InputTracking.Recenter();
        GameObject.Find("GvrViewerMain").GetComponent<GvrViewer>().Recenter();
	}


    public void takeBroom(bool enter)
    {
        if (enter)
        {
            loadSlider = true;
            slider.SetActive(true);
        }
        else
        {
            loadSlider = false;
            slider.SetActive(false);
            sliderImage.fillAmount = 0;
        }
    }


	// Update is called once per frame
	void FixedUpdate () {
        if ((animateDir == 4 || animateDir == 8) && elapsedTimeSinceLast < 1.5f && elapsedTimeSinceLast > 1f)
        {
            animateTransition += (6 - animateDir) * Time.deltaTime; //formula propia
            animator.SetFloat("Accelerate", animateTransition);
        }

        if (elapsedTimeSinceLast > secsForEachSentence + extraTime && condition && stringIndex < frasesLength)
        {
            elapsedTimeSinceLast = 0;
            textField.text = Frases[stringIndex];
            interactiveCalls(stringIndex);
            stringIndex++;
        }
        else
        {
            if (stringIndex < frasesLength) elapsedTimeSinceLast += Time.deltaTime;
            else {
                if (loadSlider)
                {
                    sliderImage.fillAmount += Time.deltaTime;//
                    if (sliderImage.fillAmount > 0.95f)
                    {
                        condition = true;
                        for (int i = 0; i < reticleObjects.Length; i++)  reticleObjects[i].SetActive(false);
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GvrPointerPhysicsRaycaster>().enabled = false;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FadeScreen>().FadeOutIn();
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FadeScreen>().logics = this;
                        loadSlider = false;
                    }
                }
                else if (!reticleObjects[0].activeSelf) //SECOND PART OF TUTORIAL
                {
                    if (fadeOutFinished)
                    {
                        fadeOutFinished = false;
                        avatarStanding.SetActive(false);
                        avatarMounted.SetActive(true);
                        broom.transform.parent = avatarMounted.transform;
                        broom.transform.localPosition = new Vector3(-0.19f, 2.37f, 0.8356f);
                        broom.transform.localRotation = Quaternion.Euler(new Vector3(5.71f, 175, 0));
                        snitch.transform.localPosition = new Vector3(116, -57.68f, 302.23f);
                        snitch.transform.localRotation = Quaternion.Euler(new Vector3(0, 94.66f, 0));
                        snitch.GetComponent<SnitchAnimation>().enabled = true;
                        snitch.GetComponent<snitchAI>().logic = this;
                    }
                    if (fadeInFinished)
                    {
                        snitch.GetComponentInChildren<GvrAudioSource>().enabled = true;
                        snitch.GetComponent<snitchAI>().isActive = true;
                        driveScript.hasBroom = true;
                        fadeInFinished = false;
                    }
                }
            }
        }
        
	}

    private void interactiveCalls(int actionNum)
    {
        switch (actionNum)
        {
            case 1: animator.SetBool("mount", true);
                extraTime = 0;
                break;

            case 3: animateDir = 4;
                break;

            case 4: animateDir = 0;
                animateTransition = 1;
                extraTime = 1;
                break;

            case 5: animateDir = 8;
                extraTime = 0;
                break;

            case 6: animateDir = 0;
                animator.SetBool("Look", true);
                break;

            case 7: extraTime = 2f;
                //condition = false; //in case we want the player to look around too
                animator.SetBool("Look", false);
                break;

            case 8: extraTime = 1f;
                animator.SetBool("Catch", true);
                break;

            case 9: extraTime = 0f;
                animator.SetBool("Catch", false);
                break;

            case 10: extraTime = 2f;
                for (int i = 0; i < reticleObjects.Length; i++) reticleObjects[i].SetActive(true);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GvrPointerPhysicsRaycaster>().enabled = true;
                condition = false; //we wait for the player to take the broom
                break;
            default: break;

        }
    }
}
