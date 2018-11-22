using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using WeLoveAero;

public class OutTrigger : MonoBehaviour {

    private bool onHoops = false;
    public LookAtArrow arrowScript;



    #region replace plane after going out trigger
    //public Image fadePanel;
    public GameObject[] gateSequence;
    public GameObject[] CheckPointDummy;

    public bool teleportationResetStep;

    //private Rigidbody rb;


    private Vector3 checkpointPosition;
    private Quaternion checkpointRotation;

    private int verification = 0;

    private bool fadeFinish = true;
    private bool unfadeFinish = true;
    private bool onTrigger = false;
    private bool teleportToNextTrigger = false;
    private int numberTrigger = 1;
    private int numberTriggerArray = 0;
    private float time = 0;
   

    private bool inside = false;
    private bool invincibility = false;
    private bool onTriggerStay = true;
    private bool stop;

    private Vector3 zeroVelocityVect = new Vector3(0, 0, 0);
    private Vector3 planeVelocity = new Vector3(0, 0, 53.0f);
    private Vector3 actualVelocity;
    private bool zeroVelocity;

    [SerializeField]
    private int endLevelNumber;

    public BaseAirplane_Input inputScript; //ligne OK


    #endregion



    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        teleportationResetStep = false;
        stop = true;
        actualVelocity = planeVelocity;
        inputScript = GetComponent<XboxAirplane_Input>(); //ligne OK

    }


    void Update()
    {
      


        verification = numberTrigger + 1;

        #region fadeInAndOut(commentaire)
        /* if (!fadeFinish)
         {
             time += Time.deltaTime * 100;

            // Debug.Log("fade Begining");
             var tempColor = fadePanel.color;
             tempColor.a += time / 100;
             fadePanel.color = tempColor;


             if (time >= 100)
             {
                 fadeFinish = true;
                 unfadeFinish = false;
             }

             else
             {
                 fadeFinish = false;
             }
         }


         if (!unfadeFinish)
         {
            //Debug.Log(time);
             time += Time.deltaTime * 100;

            //Debug.Log(fadePanel.color);
             var tempColor = fadePanel.color;
             tempColor.a -= time / 100;
             fadePanel.color = tempColor; ;


             if (time <= 0)
             {

                 unfadeFinish = true;
             }

             else
             {
                 unfadeFinish = false;
             }
         }*/
        #endregion


        if (teleportToNextTrigger)
        {
                
                    getPositionDummyCheckpoint();

                    fadeFinish = false;

                    transform.position = checkpointPosition;
                    transform.rotation = checkpointRotation;

                    //rb.angularVelocity = Vector3.zero;
                    //rb.velocity = actualVelocity;

                    teleportToNextTrigger = false;
                    teleportationResetStep = true;

                    reActiveRings();   
        }
        else
        {
            teleportationResetStep = false;
        }

        if (!onTriggerStay)
        {
            Debug.Log("en dehors du trigger");
            teleportationResetStep = true;
            teleportToNextTrigger = true;
            
        }
        else
        {
            teleportToNextTrigger = false;
        }

    }


            void FixedUpdate()
            {
                onTriggerStay = false;
        
            }


            void OnTriggerEnter(Collider other)
            {
                if (other.tag == "gate")
                {
                    arrowScript.getTheNextGate(other);
                }
            }




            public void callArrow (int number)
            {
                arrowScript.decreaseeNumberPointArrow(number);
            }



            void OnTriggerStay(Collider other)
            {

                if (other.tag == "triggerCheckpoint" && !onHoops)
                {
                    onTrigger = true;
                }

                if (other.tag == "triggerCheckpoint")
                {

                    onTriggerStay = true;
                }

            }



    void getPositionDummyCheckpoint()
    {
        if (numberTriggerArray< endLevelNumber)
        {
            checkpointPosition = CheckPointDummy[numberTriggerArray].transform.position;
            checkpointRotation = CheckPointDummy[numberTriggerArray].transform.rotation;
        }
       

        //Debug.Log("checkpointPosition = " + checkpointPosition);
        //Debug.Log("checkpointRotation = " + checkpointRotation);
    }

    void reActiveRings()
    {
        if (numberTriggerArray < endLevelNumber)
        {

            foreach (Transform child in gateSequence[numberTriggerArray].transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }



    public void getValor(bool sequenceOnGoing, int value, bool noVelocity)
    {

        zeroVelocity = noVelocity;

        if (zeroVelocity)
        {
            actualVelocity = zeroVelocityVect;
        }
        else
        {
            actualVelocity = planeVelocity;
        }
        

        onHoops = sequenceOnGoing;
        numberTrigger = value;
        numberTriggerArray = numberTrigger - 1;
        Debug.Log("numberTriggerArray = " + numberTriggerArray);

        
    }

    public void failLevel(bool noVelocity)
    {
        zeroVelocity = noVelocity;

        if (zeroVelocity)
        {
            actualVelocity = zeroVelocityVect;
            inputScript.stickyThrottle = 0f;
        }
        else
        {
            actualVelocity = planeVelocity;
        }

        teleportationResetStep = true;
        teleportToNextTrigger = true;
    }
}
