using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace FakePhysics
{
    public class Hub_Input : MonoBehaviour

    {
        #region Variables

        [SerializeField]
        private bool rollUnactive, throttleUnactive;
        [SerializeField]
        private float f_StickyThrottleValor, f_xSpeed;
        public float f_maxSpeed;
        public float reactivity;
        private Vector3 pastRotationForTheYaw;
        private Vector3 pastRotationForTheRoll;

        protected float f_pitch = 0f;
        protected float f_actualPitch = 0f;

        protected float f_roll = 0f;
        protected float f_yaw = 0f;
        protected float f_throttle = 0f;
        public float f_throttleSpeed, f_pitchSpeed = 0.5f;

        public float f_speed = 0f;

        public Slider ValeurSlider;

        public float f_stickyThrottle;
        public float f_StickyThrottle
        {
            get { return f_stickyThrottle; }
        }

        private float canTurn = 0;
        [SerializeField]
        private float maxTurn, reactivity_Roll, reactivity_Pitch, maxInclinationAngle;
        private float accerlerationX, accerlerationY, f_rollTurn, pastRotationRollZ, pastRotationRollY, reactivityNow, turnMultiplicao;
        #endregion



        // Use this for initialization
        void Start()
        {
            f_rollTurn = 0.75f;
        }

        // Update is called once per frame
        void Update()
        {
            setGyroAngleMax();
            StickyThrottleControl();
            HandleInput();
            setSpeed();
            setPitch();
            setYaw();
            setRoll();
            ClampInputs();

            transform.Translate(Vector3.forward*Time.deltaTime * f_speed);
        }

        //Create a Throttle Value that gradually grows and shrinks
        protected virtual void StickyThrottleControl()
        {
            f_stickyThrottle = f_stickyThrottle + (-f_throttle * f_throttleSpeed * Time.deltaTime);

            f_stickyThrottle = ValeurSlider.value;

            //ValeurSlider.value = f_stickyThrottle;
        }

        protected virtual void HandleInput()
        {
            //Process Main Control Input

            //f_pitch = Input.GetAxis("Pitch_manette");


            if (Input.acceleration.y<0.02 && Input.acceleration.y > -0.02)
            {
                accerlerationY = 0;
            }
            else
            {
                f_pitch = accerlerationY * reactivity_Pitch;
            }




            if (!rollUnactive)
            {
                //f_roll = Input.GetAxis("Roll_manette");
                if (Input.acceleration.x < 0.05 && Input.acceleration.x > -0.05)
                {
                    accerlerationX = 0;
                }
                else
                {
                    f_roll = -accerlerationX * reactivity_Roll;
                    //Debug.Log(f_roll);
                }
            }

           
            if(Input.GetAxis("YawDroite_manette")!=0)
            {
                f_yaw = Input.GetAxis("YawDroite_manette");
            }
            else if (Input.GetAxis("YawGauche_manette") != 0)
            {

                f_yaw = -Input.GetAxis("YawGauche_manette");
            }
            else
            {
                f_yaw = 0;
            }



            if (throttleUnactive)
            {
                f_stickyThrottle = f_StickyThrottleValor;
            }
            else
            {
                f_throttle = Input.GetAxis("Throttle_manette");
            }

        }
        

        protected void ClampInputs()
        {
            f_pitch = Mathf.Clamp(f_pitch, -1f, 1f);
            f_actualPitch = Mathf.Clamp(f_actualPitch, -1f, 1f);
            f_roll = Mathf.Clamp(f_roll, -1f, 1f);
            f_yaw = Mathf.Clamp(f_yaw, -1f, 1f);
            f_throttle = Mathf.Clamp(f_throttle, -1f, 1f);
            f_stickyThrottle = Mathf.Clamp(f_stickyThrottle, 0f, 1f);
            f_speed = Mathf.Clamp(f_speed, 0f, f_maxSpeed);
          //  accerlerationX = Mathf.Clamp(accerlerationX, -1f, 1f);
           // accerlerationY = Mathf.Clamp(accerlerationY, -1f, 1f);
        }


        protected void setSpeed()
        {
            f_xSpeed = f_maxSpeed* f_stickyThrottle;

            if (f_speed < f_xSpeed)
            {
                f_speed += (f_stickyThrottle * reactivity) * Time.deltaTime;
            }

            else if (f_speed > f_xSpeed)
            {
                f_speed -= reactivity * Time.deltaTime;
            }
            
        }

        
        protected void setPitch()
        {
            if (f_pitch == 0)
            {
                if(f_actualPitch < 0)
                {
                    f_actualPitch += 1 * Time.deltaTime * f_pitchSpeed;
                }

                else if (f_actualPitch > 0)
                {
                    f_actualPitch -= 1 * Time.deltaTime * f_pitchSpeed;
                }

            }
            else
            {
                f_actualPitch += f_pitch * Time.deltaTime * f_pitchSpeed;
            }
           
            

            transform.Rotate((f_pitch * Time.deltaTime)*reactivity, 0, 0, Space.Self);
        }



        protected void setYaw()
        {
            if(f_yaw == 0)
            {   //maxTurn
                canTurn = 0f;
            }

            else if (f_yaw != 0)
            {
                if (canTurn <= maxTurn)
                {

                    //transform.Rotate(0, (f_yaw * Time.deltaTime) * reactivity, 0, Space.Self);

                    pastRotationForTheYaw = transform.eulerAngles;
                    float pastRotationYaw = pastRotationForTheYaw.y + ((f_yaw * Time.deltaTime) * reactivity);
                    transform.rotation = Quaternion.Euler(pastRotationForTheYaw.x, pastRotationYaw, pastRotationForTheYaw.z);

                    canTurn += 1.5f * Time.deltaTime;
                }
            }
        }


        protected void setRoll()
        {
            pastRotationForTheRoll = transform.eulerAngles;
            float pastRotationRoll = pastRotationForTheRoll.z + ((f_roll * Time.deltaTime) * reactivity);
            transform.rotation = Quaternion.Euler(pastRotationForTheRoll.x, pastRotationForTheRoll.y, pastRotationRoll);
        }

        protected void setGyroAngleMax()
        {
            // maxInclinationAngle, accerlerationX, accerlerationY;

            if (Input.acceleration.x > -maxInclinationAngle && Input.acceleration.x < maxInclinationAngle)
            {
                accerlerationX = Input.acceleration.x / maxInclinationAngle; 
            }

            if (Input.acceleration.y > -maxInclinationAngle && Input.acceleration.y < maxInclinationAngle)
            {
                accerlerationY = Input.acceleration.y / maxInclinationAngle;
            }


        }

        protected void setRollTurn()
        {
            pastRotationForTheRoll = transform.eulerAngles;

            //Debug.Log(pastRotationRollZ);
            if (f_rollTurn != 0)
                {
                    if (pastRotationRollZ > 315 || pastRotationRollZ < 180)
                    {
                        pastRotationRollZ = pastRotationForTheRoll.z + ((f_rollTurn * Time.deltaTime) * 50);
                        pastRotationRollY = pastRotationForTheRoll.y + ((f_rollTurn * Time.deltaTime) * reactivity);
                    }
                }

                if(f_rollTurn != 0)
                {
                    if (pastRotationRollZ < 45 || pastRotationRollZ > 180)
                    {
                        pastRotationRollZ = pastRotationForTheRoll.z + ((f_rollTurn * Time.deltaTime) * 50);
                        pastRotationRollY = pastRotationForTheRoll.y + ((f_rollTurn * Time.deltaTime) * reactivity);
                    
                    }
                }
                
            pastRotationRollY = pastRotationForTheRoll.y + ((f_roll * Time.deltaTime) * reactivity);


            transform.rotation = Quaternion.Euler(pastRotationForTheRoll.x, pastRotationRollY, pastRotationRollZ);
        }



        public void buttonOnClick(bool isRight)
        {

            pastRotationForTheRoll = transform.eulerAngles;

            if (isRight)
            {
                if (pastRotationRollZ > 315|| pastRotationRollZ < 180)
                {
                    reactivityNow = 50;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((-f_rollTurn * Time.deltaTime) * reactivityNow);
                }
                else if(pastRotationRollZ > 300 || pastRotationRollZ < 180)
                {
                    reactivityNow--;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((-f_rollTurn * Time.deltaTime) * reactivityNow);
                    pastRotationRollY = pastRotationForTheRoll.y + ((f_rollTurn * Time.deltaTime) * 50);
                }
       

            }
            else
            {

                if (pastRotationRollZ < 45 || pastRotationRollZ > 180)
                {
                    pastRotationRollZ = pastRotationForTheRoll.z + ((f_rollTurn * Time.deltaTime) * 50);
                    pastRotationRollY = pastRotationForTheRoll.y + ((-f_rollTurn * Time.deltaTime) *100);
                }
            }

            transform.rotation = Quaternion.Euler(pastRotationForTheRoll.x, pastRotationRollY, pastRotationRollZ);
        }


        public void buttonNotOnClick(bool isRight)
        {
            if (isRight)
            {
                f_rollTurn = 0;
            }
            else
            {
                f_rollTurn = 0;
            }
        }


    }
}
