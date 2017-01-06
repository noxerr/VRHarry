using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialLogic : MonoBehaviour {
    public Animator animator;
    private Text textField;
    public Drive driveScript;
    public float secsForEachSentence = 3.5f;
    public GameObject[] reticleObjects;
    public GameObject reticle, slider;
    private float elapsedTimeSinceLast = 0, extraTime = 0;
    private int stringIndex = 0, frasesLength;
    private bool condition, loadSlider = false;
    private int animateDir = 0; //0 no, 4 forward, 8 backwards
    private float animateTransition = 0; //0 standing on broom, 1 leaning forward
    private Image sliderImage;

    private string[] Frases = new string[]{
        "Welcome to the Tutorial", //0
        "I'm going to show you\nSome basic movements", //1
        "apply a hard force forward", //2
        "the harder the force is, the faster you\'ll go", //3
        "you can apply force twice, with delay,\nto go faster", //4
        "to slow down, do the opposite", //5
        "you can also look around,\nlike me", //6
        "you can also look around,\nlike me", //7 -- to stop looping lookAround
        "Finally, your goal is\to catch the snitch", //8
        "Finally, your goal is\to catch the snitch", //9
        "Look at your broom to get it", //10
    };

	// Use this for initialization
	void Start () {
        textField = GetComponent<Text>();
        textField.text = Frases[stringIndex];
        stringIndex = 1;
        extraTime = 2.5f;
        frasesLength = Frases.Length;
        condition = true;
        sliderImage = slider.GetComponent<Image>();
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
	void Update () {
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
            if (stringIndex >= frasesLength) {
                if (loadSlider)
                {
                    sliderImage.fillAmount += Time.deltaTime;//
                    if (sliderImage.fillAmount > 0.95f)
                    {
                        driveScript.hasBroom = true;
                        condition = true;
                        for (int i = 0; i < reticleObjects.Length; i++)  reticleObjects[i].SetActive(false);
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GvrPointerPhysicsRaycaster>().enabled = false;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FadeScreen>().FadeOutIn();
                    }
                }
            }//check if last test is finished, i.e catching snitch
            else elapsedTimeSinceLast += Time.deltaTime;
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
