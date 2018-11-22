using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FakePhysics;



namespace FakePhysics
{
    public class Rotation_Propeller : MonoBehaviour
    {

        #region Variables
        public Throttle_Percentage VitesseRotation;

        [Header("Propeller Properties")]
        public GameObject mainProp;
        public GameObject blurredProp;
        public GameObject blurredProp1;



        #endregion

        // Use this for initialization
        void Start()
        {

        }



        // Update is called once per frame
        void Update()
        {
            RotationPropeller();
        }



        void RotationPropeller()
        {


            if (VitesseRotation.ValeurThrottle < 50)
            {
                //Rotate the Propeller Group
                transform.Rotate(0, 0, -(VitesseRotation.ValeurThrottle+10));
           
                mainProp.gameObject.SetActive(true);
                blurredProp.gameObject.SetActive(false);
                blurredProp1.gameObject.SetActive(false);
            }


           if (VitesseRotation.ValeurThrottle >= 50 && VitesseRotation.ValeurThrottle < 70)
           {
                transform.Rotate(0, 0, -10);

                mainProp.gameObject.SetActive(false);
                blurredProp.gameObject.SetActive(true);
                blurredProp1.gameObject.SetActive(false);

            }


           if (VitesseRotation.ValeurThrottle >= 70)
           {
                transform.Rotate(0, 0,-1);

                mainProp.gameObject.SetActive(false);
                blurredProp.gameObject.SetActive(false);
                blurredProp1.gameObject.SetActive(true);
            }
        }


    }
}

