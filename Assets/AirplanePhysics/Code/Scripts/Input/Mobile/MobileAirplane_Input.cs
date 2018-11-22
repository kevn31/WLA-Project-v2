using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace WeLoveAero
{
    public class MobileAirplane_Input : BaseAirplane_Input
    {
        #region Variables
        [Header("Mobile Input Properties")]
        public Mobile_Thumbstick1 lThumbstick;
        public Mobile_Thumbstick rThumbstick;
        

        //public Slider ValeurSlider;

        public string SceneToLoad;
        #endregion


        #region Custom Methods
        protected override void HandleInput()
        {
            if(lThumbstick && rThumbstick)
            {
                //Process Main Control Input
             


                pitch = -rThumbstick.VerticalAxis;
                roll = rThumbstick.HorizontalAxis;
                yaw = lThumbstick.HorizontalAxis;
                //throttle = -lThumbstick.VerticalAxis;
                //throttle = ValeurSlider.value;

                //Debug.Log("valeur throttle " + throttle);

                //throttle = ValeurSlider.value;
                //Debug.Log("valeur value slider " + ValeurSlider.value);

            }
        }

        public void SetBrake(float aValue)
        {
            brake = aValue;
        }

      
        public void SetCamera(bool aFlag)
        {
            cameraSwitch = true;
        }



        public void ReLoadScene()
        {
            SceneManager.LoadScene(SceneToLoad);
           
        }




        #endregion
    }
}
