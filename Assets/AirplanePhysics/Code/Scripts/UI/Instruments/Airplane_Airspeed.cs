using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FakePhysics;


namespace WeLoveAero
{
    public class Airplane_Airspeed : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Airspeed Indicator Properties")]
        public Hub_Input MPH;
        public RectTransform pointer;
        public float maxIndicatedKnots = 160f;





        void Update()
        {

            HandleAirplaneUI();
        }




        #endregion


        public const float mphToKnts = 0.868976f;


        #region Interface Methods
        public void HandleAirplaneUI()
        {
            //if(characteristics && pointer)
            //{
                float currentKnots = ((MPH.f_speed / maxIndicatedKnots) * MPH.f_maxSpeed )* mphToKnts;
                //Debug.Log(currentKnots);

                float normalizedKnots = Mathf.InverseLerp(0f, maxIndicatedKnots, currentKnots);
                float wantedRotation = 360f * normalizedKnots;
                pointer.rotation = Quaternion.Euler(0f, 0f, -wantedRotation);
            //}
        }
        #endregion
    }
}
