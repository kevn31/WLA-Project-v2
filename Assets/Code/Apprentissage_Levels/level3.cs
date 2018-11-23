using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FakePhysics;

    public class level3 : MonoBehaviour
    {
        public Text instructionTxt;

        public GameObject panelInstruction;
        public GameObject panelHiding;

        public Image altimeter;

        public GameObject canvas_EndLevel;

        public Image joyStickL;
        public Image joyStickR;
        public GameObject manette;
        public GameObject manette2;
        public GameObject manette3;

        public string[] nameGateCheckpoint;

        public Hub_Input Hub_InputScr;

        public float valor;

        
    //private Rigidbody rb;

        private int stepLearning = 0;
        private int numberCheckpoint = 0;
        private int numberTrigger = 1;
        private int numberGateToPass = 2;

        private bool stop;
        private bool incrementNumberCheckpoint = true;
        private bool playOnce = true;

        private Color apprentissageColor;
        private string test;

        private OutTrigger scriptTrigger;
        [SerializeField]
        private GameObject modelArrow;
        private bool functionArrowAlreadyPlayed = false;

        private Vector3 rotationPlane;

    void Start()
        {
            Hub_InputScr.setThrottle(valor);
            Time.timeScale = 1;

            scriptTrigger = GetComponent<OutTrigger>();
        

            stepLearning = 0;
            stop = true;

            apprentissageColor = joyStickL.GetComponent<Image>().color;

            joyStickR.GetComponent<Image>().color = apprentissageColor;
            joyStickL.GetComponent<Image>().color = Color.white;

            joyStickR.transform.GetChild(0).gameObject.SetActive(true);
            joyStickR.transform.GetChild(1).gameObject.SetActive(true);

            
            panelHiding.SetActive(false);
        }

        void Update()
        {

        rotationPlane = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotationPlane.x, rotationPlane.y, 0);

        if (scriptTrigger.teleportationResetStep)
        {
            if(numberTrigger == 1)
            {
                stepLearning = 0;
                numberCheckpoint = 0;
                scriptTrigger.callArrow(0);
            }

            if (numberTrigger == 2)
            {
                stepLearning = 4;
                numberCheckpoint = 0;
                scriptTrigger.callArrow(3);
            }

            if (numberTrigger == 3)
            {
                stepLearning = 9;
                numberCheckpoint = 0;
                scriptTrigger.callArrow(6);
            }
        }

        if (stepLearning == 0)
            {
                apprentissageColor = joyStickL.GetComponent<Image>().color;

                manette.SetActive(true);
                instructionTxt.enabled = true;

                instructionTxt.text = "Use the <color=#ffa500ff>right joystick</color> to <color=#ffa500ff>turn</color>";

                joyStickR.GetComponent<Image>().color = apprentissageColor;
                joyStickL.GetComponent<Image>().color = Color.white;

                joyStickR.transform.GetChild(0).gameObject.SetActive(false);
                joyStickR.transform.GetChild(1).gameObject.SetActive(false);
                joyStickR.transform.GetChild(2).gameObject.SetActive(true);
                joyStickR.transform.GetChild(3).gameObject.SetActive(true);


            if (Input.GetAxis("Roll_manette") != 0 && stop)
                {
                    StartCoroutine(waitBeforeStep(0.5f));
                }
            }

            if (stepLearning == 1 && stop)
            {
                manette.SetActive(false);
                instructionTxt.text = "Well done";
                StartCoroutine(waitBeforeStep(1f));
            }

            if (stepLearning == 2 && stop)
            {
                instructionTxt.enabled = false;
                StartCoroutine(waitBeforeStep(0.1f));
            }

            if (stepLearning == 3 && stop)
            {
                instructionTxt.enabled = true;
                instructionTxt.text = "Now try follow the hoops";
                StartCoroutine(waitBeforeStep(1f));
            }

            if (stepLearning == 4 && stop)
            {
                instructionTxt.text = "<size=60>You can <color=#ffa500ff>adjust</color> by move the controller <color=#ffa500ff>up and down</color></size>";
                StartCoroutine(waitBeforeStep(1f));
            }

            if (stepLearning == 5 && stop)
            {
                instructionTxt.enabled = false;
            }

            if (stepLearning == 6 && stop)
            {
                instructionTxt.enabled = true;
                instructionTxt.text = "Well Done";

                StartCoroutine(waitBeforeStep(0.5f));
            }


            if (stepLearning == 7 && stop)
            {
                instructionTxt.text = "<size=60>Press the <color=#ffa500ff>up triggers</color> to activate the white or red <color=#ffa500ff>smoke</color></size>";
                manette3.SetActive(false);

                if(Input.GetAxis("SmokeW_manette") != 0 && stop || Input.GetAxis("SmokeR_manette") != 0 && stop)
                {
                    instructionTxt.text = "Well done";
                    StartCoroutine(waitBeforeStep(0.5f));
                }
                
            }

            if (stepLearning == 8 && stop)
            {
                instructionTxt.enabled = true;
                instructionTxt.text = "Let's practice a little !";
                StartCoroutine(waitBeforeStep(2f));
            }

            if (stepLearning == 9 && stop)
            {
                instructionTxt.enabled = true;
                instructionTxt.text = "Finish the <color=#ffa500ff>?</color> rings left";
                StartCoroutine(waitBeforeStep(2f));
            }

            if (stepLearning == 10 && stop)
            {
                instructionTxt.enabled = false;
            }

            if (stepLearning == 11 && stop)
            {
                instructionTxt.enabled = true;
                instructionTxt.text = "Level clear";
                StartCoroutine(waitBeforeStep(2.5f));
            }

            if (stepLearning == 12 && stop)
            {
                instructionTxt.enabled = false;
                Time.timeScale = 0;
                canvas_EndLevel.SetActive(true);    
            }

            if (instructionTxt.enabled == false)
            {
                //Debug.Log("Text et boite de text desactivé");
                panelInstruction.SetActive(false);
            }

            else
            {
                panelInstruction.SetActive(true);
            }

            if (numberCheckpoint == numberGateToPass)
            {
                numberTrigger++;
                scriptTrigger.getValor(false, numberTrigger, false);
                numberCheckpoint = 0;
            }

            if (numberTrigger == 3)
            {
                numberGateToPass = 4;
            }
        }

        IEnumerator waitBeforeStep(float timer)
        {
            stop = false;
            yield return new WaitForSeconds(timer);
            stepLearning += 1;
            stop = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "gate")
            {
                if (incrementNumberCheckpoint)
                {
                    numberCheckpoint += 1;
                    incrementNumberCheckpoint = false;
                }

                if (!functionArrowAlreadyPlayed)
                {
                    functionArrowAlreadyPlayed = true;
                }
            }

            if (other.name == "NextSequence01" && numberTrigger != 1)
            {
                numberCheckpoint = 0;
                stepLearning = 6;
            }

            if (other.name == "NextSequence02" && numberTrigger != 2)
            {
                numberCheckpoint = 0;
                stepLearning = 7;
            }

            if (other.name == "NextSequence03" && numberTrigger != 3)
            {
                numberCheckpoint = 0;
                stepLearning = 11;
            }

            if (numberTrigger == 1)
            {
                if (other.name == "NextSequence01")
                {
                    scriptTrigger.failLevel(false);
                    stepLearning = 0;
                }
            }

            if (numberTrigger == 2)
            {
                if (other.name == "NextSequence02")
                {
                    scriptTrigger.failLevel(false);
                    stepLearning = 4;
                }
            }

            if (numberTrigger == 3)
            {
                if (other.name == "NextSequence03")
                {
                    scriptTrigger.failLevel(false);
                    stepLearning = 9;
                }
            }
    }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "gate")
            {
                 functionArrowAlreadyPlayed = false;
                 incrementNumberCheckpoint = true;
                //Debug.Log(nameGateCheckpoint.Length);

                for (int i = 0; i < nameGateCheckpoint.Length; i++)
                {
                    if (other.name == nameGateCheckpoint[i])
                    {
                        numberCheckpoint = 0;


                        if (other.name == "Gate_006")
                        {
                            numberGateToPass = 4;
                        }

                        //Debug.Log("TRIGER NUMBER = " + numberTrigger);
                        //Debug.Log("number checkpoint = " + numberCheckpoint);
                    }
                }
        }
    }
    }
