using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeLoveAero
{
    public class XboxAirplane_Input : BaseAirplane_Input
    {
        #region Variables
        [SerializeField]
        private bool rollUnactive, throttleUnactive;
        [SerializeField]
        private float StickyThrottleValor;
        #endregion


        #region Custom Methods
        protected override void HandleInput()
        {
            //process Keyboard
            base.HandleInput();

            //Process Main Control Input
            pitch += Input.GetAxis("Pitch_Xbox");

            if (!rollUnactive)
            {
                roll += Input.GetAxis("Roll_Xbox");
            }

            yaw += Input.GetAxis("YawDroite_Xbox");
            yaw -= Input.GetAxis("YawGauche_Xbox");

            if (throttleUnactive)
            {
               stickyThrottle = StickyThrottleValor;
            }
            else
            {
                throttle += Input.GetAxis("Throttle_Xbox");
            }
            

            //Process Brake inputs
            brake += Input.GetAxis("Fire1");

            //Process Smokeinputs
            //smoke += Input.GetAxis("Smoke_Xbox");


            //Camera Switch Button
            cameraSwitch = Input.GetButtonDown("Camera_Xbox") || Input.GetKeyDown(cameraKey);


           

        }

        protected override void StickyThrottleControl()
        {
            stickyThrottle = stickyThrottle + (-throttle * throttleSpeed * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);

            //stickyThrottle = ValeurSlider.value;
            //Debug.Log("Sticky" + stickyThrottle);
        }

        #endregion
    }
}
