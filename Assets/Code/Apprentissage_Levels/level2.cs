using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

    public class level2 : MonoBehaviour
    {
        public Text instructionTxt;

        public GameObject panelInstruction;
        public GameObject panelHiding;

        public Image horizon;

        public GameObject canvas_EndLevel;

        public Image joyStickL;
        public Image joyStickR;
        public GameObject manette;
        public GameObject manette2;
        public GameObject manette3;

        public string[] nameGateCheckpoint;

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
            Time.timeScale = 1;
            //rb = GetComponent<Rigidbody>();
            scriptTrigger = GetComponent<OutTrigger>();

            //rb.angularVelocity = Vector3.zero;

            //rb.velocity = new Vector3(0, 0, 53.0f);
            //rb.velocity = new Vector3(-3.5f, 5.4f, 53.0f);

            stepLearning = 0;
            stop = true;

            apprentissageColor = joyStickL.GetComponent<Image>().color;

            //instructionTxt.text = "<size=60>Do a <color=#ffa500ff>right barrel</color> by moving the controller<color=#ffa500ff> to the right </color></size>";

            joyStickR.GetComponent<Image>().color = apprentissageColor;
            joyStickL.GetComponent<Image>().color = Color.white;

            //joyStickR.transform.GetChild(0).gameObject.SetActive(false);    

        horizon.enabled = false;
            foreach (Transform child in horizon.transform)
            {
                child.gameObject.SetActive(false);
            }

            panelHiding.SetActive(false);
        }

        void Update()
        {

        /*rotationPlane = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotationPlane.x, rotationPlane.y, 0);*/

        if (scriptTrigger.teleportationResetStep)
        {
            if(numberTrigger == 1)
            {
                stepLearning = 0;
                numberCheckpoint = 0;
                //scriptTrigger.decreaseeNumberPointArrow(0);
            }

            if (numberTrigger == 2)
            {
                stepLearning = 4;
                numberCheckpoint = 0;
               // scriptTrigger.decreaseeNumberPointArrow(3);
            }

            if (numberTrigger == 3)
            {
                stepLearning = 9;
                numberCheckpoint = 0;
               // scriptTrigger.decreaseeNumberPointArrow(6);
            }
        }

        if (stepLearning == 0)
            {
                apprentissageColor = joyStickL.GetComponent<Image>().color;

                manette.SetActive(true);
                instructionTxt.enabled = true;

                instructionTxt.text = "<size=60>Do a <color=#ffa500ff>right barrel</color> by moving the controller<color=#ffa500ff> to the right </color></size>";

                joyStickR.GetComponent<Image>().color = apprentissageColor;
                joyStickL.GetComponent<Image>().color = Color.white;

               // joyStickR.transform.GetChild(0).gameObject.SetActive(false);
                //joyStickR.transform.GetChild(1).gameObject.SetActive(false);

            if (Input.GetAxis("Roll_manette") > 0.9 && stop)
                {
                    horizon.enabled = true;
                    foreach (Transform child in horizon.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                    StartCoroutine(waitBeforeStep(0.5f));
                }
            }

            if (stepLearning == 1 && stop)
            {
                manette.SetActive(false);
                instructionTxt.enabled = false;
            }

            /*if (stepLearning == 2 && stop)
            {
                manette.SetActive(false);
                instructionTxt.enabled = false;
            }*/

            if (stepLearning == 2 && stop)
            {
                instructionTxt.enabled = true;
                instructionTxt.text = "Well Done";
                joyStickR.transform.GetChild(0).gameObject.SetActive(false);
                joyStickR.transform.GetChild(1).gameObject.SetActive(false);
                StartCoroutine(waitBeforeStep(1f));
            }

            if (stepLearning == 3 && stop)
            {
                instructionTxt.enabled = true;

                manette2.SetActive(true);

                joyStickL.GetComponent<Image>().color = apprentissageColor;
                joyStickR.GetComponent<Image>().color = Color.white;

                joyStickL.transform.GetChild(2).gameObject.SetActive(true);
                joyStickL.transform.GetChild(3).gameObject.SetActive(true);

                instructionTxt.text = "<size=60>Do a <color=#ffa500ff>left barrel</color> by moving the controller<color=#ffa500ff> to the left </color></size>";
                stepLearning = 4;
            }

            if (stepLearning == 4 && stop)
            {
                if (Input.GetAxis("Roll_manette") <-0.9 && stop)
                {
                    StartCoroutine(waitBeforeStep(0.5f));
                }
            }

            if (stepLearning == 5 && stop)
            {
                manette2.SetActive(false);
                instructionTxt.enabled = false;
            }

            if (stepLearning == 6 && stop)
            {
                instructionTxt.enabled = true;
                instructionTxt.text = "Well Done";
                StartCoroutine(waitBeforeStep(1f));
            }

            if (stepLearning == 7 && stop)
            {
                instructionTxt.enabled = true;
                manette3.SetActive(true);

                joyStickL.GetComponent<Image>().color = apprentissageColor;
                joyStickR.GetComponent<Image>().color = Color.white;

                joyStickL.transform.GetChild(2).gameObject.SetActive(true);
                joyStickL.transform.GetChild(3).gameObject.SetActive(true);

                instructionTxt.text = "<size=60>Do a <color=#ffa500ff>loping</color> by moving the controller<color=#ffa500ff> up </color></size>";
                stepLearning = 8;
            }

            if (stepLearning == 8 && stop)
            {
                if (Input.GetAxis("Pitch_manette") < -0.9 && stop)
                {
                    StartCoroutine(waitBeforeStep(0.5f));
                }
            }

            if (stepLearning == 9 && stop)
            {
                manette3.SetActive(false);
                instructionTxt.enabled = false;
            }

            if (stepLearning == 10 && stop)
            {
                instructionTxt.enabled = true;
                instructionTxt.text = "Level clear";
                StartCoroutine(waitBeforeStep(2.5f));
            }

            if (stepLearning == 11 && stop)
            {
                instructionTxt.enabled = false;
                Time.timeScale = 0;
                canvas_EndLevel.SetActive(true);    
            }

            if (instructionTxt.enabled == false)
            {
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
                stepLearning = 2;
            }

            if (other.name == "NextSequence02" && numberTrigger != 2)
            {
                numberCheckpoint = 0;
                stepLearning = 6;
            }

            if (other.name == "NextSequence03" && numberTrigger != 3)
            {
                numberCheckpoint = 0;
                stepLearning = 10;
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
