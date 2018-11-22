using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FakePhysics;

namespace WeLoveAero
{
    public class Airplane_Altimeter : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Altimeter Properties")]
        public Hub_Input airplane;
        public RectTransform hundredsPointer;
        public RectTransform thousandsPointer;


        private float currentMSL;
        public float CurrentMSL
        {
            get { return currentMSL; }
        }


        private float currentAGL;
        public float CurrentAGL
        {
            get { return currentAGL; }
        }


        #endregion


        #region Constants
        const float metersToFeet = 3.28084f;
        #endregion



        void Update()
        {
            HandleAltitude();
            HandleAirplaneUI();
        }




        #region Interface Methods
        void HandleAltitude()
        {
            currentMSL = airplane.transform.position.y * metersToFeet;

            /*
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.transform.tag == "ground")
                {
                    currentAGL = (transform.position.y - hit.point.y) * metersToFeet;
                }
            }
            */
        }

        public void HandleAirplaneUI()
        {
            //if(airplane)
            //{
                float currentAlt = CurrentMSL;
                float currentThousands = currentAlt / 1000f;
                currentThousands = Mathf.Clamp(currentThousands, 0f, 10f);

                float currentHundreds = currentAlt - (Mathf.Floor(currentThousands) * 1000f);
                currentHundreds = Mathf.Clamp(currentHundreds, 0f, 1000f);

                if(thousandsPointer)
                {
                    float normalizedThousands = Mathf.InverseLerp(0f, 10f, currentThousands);
                    float thousandsRotation = 360f * normalizedThousands;
                    thousandsPointer.rotation = Quaternion.Euler(0f, 0f, -thousandsRotation);
                }

                if(hundredsPointer)
                {
                    float normalizedHundreds = Mathf.InverseLerp(0f, 1000f, currentHundreds);
                    float hundredsRotation = 360f * normalizedHundreds;
                    hundredsPointer.rotation = Quaternion.Euler(0f, 0f, -hundredsRotation);
                }
            //}
        }
        #endregion
    }
}
