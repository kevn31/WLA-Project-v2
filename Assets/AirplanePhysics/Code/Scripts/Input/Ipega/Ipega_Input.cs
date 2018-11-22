using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WeLoveAero
{
    public class Ipega_Input : BaseAirplane_Input

    {
        #region Variables
        #endregion


        #region Custom Methods
        protected override void HandleInput()
        {
            //process Keyboard
            base.HandleInput();

            //Process Main Control Input
            pitch += Input.GetAxis("Pitch_Ipega");
            roll += Input.GetAxis("Roll_Ipega");
            yaw += Input.GetAxis("YawDroite_Ipega");
            yaw -= Input.GetAxis("YawGauche_Ipega");
            throttle += Input.GetAxis("Throttle_Ipega");


            //Process Brake inputs
            brake += Input.GetAxis("Fire1");

            //Process Smokeinputs
            //smoke += Input.GetAxis("Smoke_Ipega");


            //Camera Switch Button
            cameraSwitch = Input.GetButtonDown("Camera_Ipega") || Input.GetKeyDown(cameraKey);

        }
        #endregion
    }
}

