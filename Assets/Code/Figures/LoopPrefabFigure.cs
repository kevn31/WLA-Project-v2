using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeLoveAero;

namespace WeLoveAero
{
    public class LoopPrefabFigure : MonoBehaviour
    {

        private GameObject objectOnCollider;
        public int numberOnLoop;
        private string numberOnLoopString;
        private string numberOnLoopStringMoreOne;

        public bool alreadyIncrease;

       // public Text CheckpointSuccess;     //a rajouter
        public Text scoreTxt;

        public int checkPointPassageSuccess;

        public string figureName;

        public GameObject nextCheckpoint;
        public GameObject CurrentCheckpoint;
        public GameObject CurrentFigure;

        private int nbrPerfect;
        private int nbrGood;
        private int nbrBad;

       
        // private FigureManager managerScript;
        //private GameObject manager;

        FigureManager managerScript;
        feedBackCheckPoint feedBackCheckPointScript;
        CalculScore scriptScore;

        private Vector3 FeedBackCheckPointPosition;
        private Quaternion FeedBackCheckPointRotation;
        public int checkPointActual;
        public int checkPointNext;
        public int checkPointEnd;     //determine la fin de chaque figure et doit etre asigné a son instantiation
        public GameObject ActualCheckPointGameObject;
        public GameObject NextCheckPointGameObject;
        public GameObject FourArrow;//le feedBack a deplacer 


        void Start()
        {
            scriptScore = GameObject.Find("_manager").GetComponent<CalculScore>();
            managerScript = GameObject.Find("_manager").GetComponent<FigureManager>();
            //feedBackCheckPointScript = GameObject.Find("1").GetComponent<feedBackCheckPoint>();

            
            checkPointPassageSuccess = 0;
          //  CheckpointSuccess.enabled = false;

            alreadyIncrease = false;
            numberOnLoop = 1;
            //manager = GameObject.FindWithTag("manager");
            // managerScript = manager.GetComponent<FigureManager>();



            checkPointActual = 0;
            checkPointNext = 1;

        }


        void Update()
        {
          //  Debug.Log("passage: " + checkPointPassageSuccess);
         
        //         Debug.Log("   numberOnLoop: " + numberOnLoop);
        }

        public void setScript()
        {
            Debug.Log("set script");
            Getcheckpoint(true);

            CurrentFigure = GameObject.FindWithTag("onGoingFigure");
            //scriptScore = CurrentFigure.GetComponent<CalculScore>();
        }


        void Getcheckpoint(bool callOnstart)
        {

            numberOnLoopString = numberOnLoop.ToString();
            CurrentCheckpoint = GameObject.Find(numberOnLoopString);


            if (callOnstart)
            {

                if (CurrentCheckpoint != null)
                {
                    if (CurrentCheckpoint.transform.root != transform)
                    {
                        CurrentCheckpoint = CurrentCheckpoint.transform.parent.gameObject;
                    }

                    //feedBackCheckPoint sn = CurrentCheckpoint.GetComponent<feedBackCheckPoint>();
                   // feedBackCheckPointScript.setActiveArrows();

                }
            }


            if (!callOnstart)
            {
                Debug.Log("JE SUIS LA");
                numberOnLoopStringMoreOne = (numberOnLoop + 1).ToString();
                nextCheckpoint = GameObject.Find(numberOnLoopStringMoreOne);

                if (nextCheckpoint == null || nextCheckpoint.name == "1")
                {
                    nextCheckpoint = GameObject.Find("endFigure");
                }


                if (nextCheckpoint.transform.root != transform && nextCheckpoint.transform.parent.name == nextCheckpoint.name)
                {
                    nextCheckpoint = nextCheckpoint.transform.parent.gameObject;
                }

                // feedBackCheckPoint snNext = nextCheckpoint.GetComponent<feedBackCheckPoint>();
               // feedBackCheckPointScript.setActiveArrows();


                if (CurrentCheckpoint != null)
                {
                   // feedBackCheckPoint snCurrent = CurrentCheckpoint.GetComponent<feedBackCheckPoint>();

                   /* if (feedBackCheckPointScript != null)
                    {
                        feedBackCheckPointScript.setUnactiveArrows();
                    }  */
                }

            }

        }


        void OnTriggerEnter(Collider other)
        {
          
          //  feedBackCheckPointScript = GameObject.Find(checkPointNext.ToString()).GetComponent<feedBackCheckPoint>();
            // checkPointActual = 
            Renderer m_Renderer = other.gameObject.GetComponent<Renderer>();
            numberOnLoopString = numberOnLoop.ToString();
           // checkPointNext = checkPointNext.ToString();
            //Debug.Log("coolide");

            Debug.Log(numberOnLoopString);

            if (m_Renderer != null)
            {
                m_Renderer.enabled = false;
            }

            //if (other.gameObject.name == numberOnLoopString)
            if (other.gameObject.name == checkPointNext.ToString())
            {
                if (other.gameObject.name == "1")
                {
                    scriptScore.SetCheckPointEnd();
                }
                   
                    checkPointActual ++;
                checkPointNext = checkPointActual + 1;
                VisualCheckPoint();
                Debug.Log("coolide " + checkPointNext);
                Getcheckpoint(false);

              
                numberOnLoop++;
                if (other.gameObject.tag == "small")
                {
                    checkPointPassageSuccess = 1;
                }
                else if (other.gameObject.tag == "medium")
                {
                    checkPointPassageSuccess = 2;

                }
                else if (other.gameObject.tag == "large")
                {

                    checkPointPassageSuccess = 3;
                }
               

            }

            else if (other.gameObject.name == "endFigure")
            {
                Getcheckpoint(false);
                Debug.Log("fin de figure");

                if (other.gameObject.tag == "small")
                {
                    checkPointPassageSuccess = 5;

                    managerScript.allowToPlace();
                    variableReset();
                }
                else if (other.gameObject.tag == "medium")
                {
                    checkPointPassageSuccess = 6;
                    managerScript.allowToPlace();
                    variableReset();

                }
                else if (other.gameObject.tag == "large")
                {
                    checkPointPassageSuccess = 7;
                    managerScript.allowToPlace();
                    variableReset();
                }

                numberOnLoop = 0;
            }

            else
            {

                checkPointPassageSuccess = 4;

            }

            alreadyIncrease = false;
        }

        void OnTriggerExit(Collider other)
        {
           

            if (other.gameObject.tag == "large")
            {
               // CheckpointSuccess.enabled = true;


                if (checkPointPassageSuccess == 1)
                {
                    Debug.Log("Perfect !!");
                    nbrPerfect++;



                }

                else if (checkPointPassageSuccess == 2)
                {
                    Debug.Log("Good !");
                    nbrGood++;
                }

                else if (checkPointPassageSuccess == 3)
                {
                    Debug.Log("Bad.");
                    nbrBad++;
                }

                else if (checkPointPassageSuccess == 4)
                {
                    Debug.Log("Miss...");
                   // CheckpointSuccess.enabled = true;
                   // CheckpointSuccess.text = "Miss...";

                    //////                  \\\\\\
                    /////                    \\\\\       
                    ////                      \\\\
                    ///                        \\\
                    //                          \\

                    while (other.gameObject.name != numberOnLoopString)
                    {
                      //  Debug.Log("nom du checkpoint = " + other.gameObject.name);
                        numberOnLoop++;
                        numberOnLoopString = numberOnLoop.ToString();
                        //Debug.Log("nombre loop = " + numberOnLoop);
                        return;
                    }

                }

                else if (checkPointPassageSuccess == 5)
                {
                    Debug.Log("passe vers allow place " + checkPointPassageSuccess);
                    nbrPerfect++;
                    Handheld.Vibrate();

                    StartCoroutine(WaitAndDisable());
                    scriptScore.scoreTotalFigure(nbrPerfect, nbrGood, nbrBad);
                    
                    managerScript.allowToPlace();
                    variableReset();
                }

                else if (checkPointPassageSuccess == 6)
                {
                    Debug.Log("passe vers allow place " + checkPointPassageSuccess);
                    nbrGood++;
                    Handheld.Vibrate();

                    StartCoroutine(WaitAndDisable());
                    scriptScore.scoreTotalFigure(nbrPerfect, nbrGood, nbrBad);
                   
                    managerScript.allowToPlace();
                    variableReset();
                }

                else if (checkPointPassageSuccess == 7)
                {
                    Debug.Log("passe vers allow place " + checkPointPassageSuccess);
                    nbrBad++;
                    Handheld.Vibrate();

                    StartCoroutine(WaitAndDisable());
                    scriptScore.scoreTotalFigure(nbrPerfect, nbrGood, nbrBad);
                   
                    managerScript.allowToPlace();
                    variableReset();
                }

                if (!alreadyIncrease)
                {
                    numberOnLoop++;
                    alreadyIncrease = true;
                }

                checkPointPassageSuccess = 0;



            }

            else if (other.gameObject.tag == "limiteFigure")
            {
                Handheld.Vibrate();

                StartCoroutine(WaitAndDisable());
                // scriptScore.scoreTotalFigure(0, 0, 0);
                // managerScript.allowToPlace();
                variableReset();
            }



        }


        public IEnumerator WaitAndDisable()
        {
            yield return new WaitForSeconds(3);
           // CheckpointSuccess.enabled = false;

        }
         
        private void variableReset()
        {
            nbrPerfect = 0;
            nbrGood = 0;
            nbrBad = 0;
        }

        public void VisualCheckPoint()
        {
            FourArrow = GameObject.Find("FourArrow");
            if (checkPointActual != checkPointEnd )
            { 
            Debug.Log(" checkPointActuel" + checkPointActual);
             ActualCheckPointGameObject = GameObject.Find(checkPointActual.ToString());
           // ActualCheckPointGameObject = GameObject.Find("2");
            //NextCheckPointGameObject = GameObject.Find("3");
             NextCheckPointGameObject = GameObject.Find(checkPointNext.ToString());
            // FeedBackCheckPointPosition = plane.transform.position;
            // FeedBackCheckPointRotation = Quaternion.Euler(new Vector3(0, plane.transform.rotation.eulerAngles.y, 0));
            FeedBackCheckPointPosition = NextCheckPointGameObject.transform.position;
            FeedBackCheckPointRotation = Quaternion.Euler(new Vector3(0, NextCheckPointGameObject.transform.rotation.eulerAngles.y, 0));

            FourArrow.transform.position = FeedBackCheckPointPosition;
            FourArrow.transform.rotation = FeedBackCheckPointRotation;
                // ActualCheckPointGameObject.SetActive(false);
                // NextCheckPointGameObject.SetActive(true);                                                         s
            }
            else
            {
                Debug.Log(" fin");
                checkPointActual = 0;
                checkPointNext = 1;//la figure suivante doit etre instantiate voir peut etre un peu avant
                managerScript.placePlane();
            }
        }
    }
}
