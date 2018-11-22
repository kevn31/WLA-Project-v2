using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FakePhysics;




namespace WeLoveAero
{
    public class Airplane_Horizon : MonoBehaviour, IAirplaneUI
    {

        #region Variables
        [Header("HorizonProperties")]
        public Hub_Input airplane;
        //public Airplane_Characteristics rollCharac;
        public RectTransform horizonArtificial;
        #endregion



        void Update()
        {
           
            HandleAirplaneUI();
        }



        #region Interface Methods
        public void HandleAirplaneUI()
        {
            float horizonRotation = - airplane.transform.eulerAngles.z;
         

            if (airplane)
            {
                if (horizonArtificial)
                {
                        horizonArtificial.rotation =  Quaternion.Euler(0f, 0f, horizonRotation);
                }
            }
        }
        #endregion
    }
}
