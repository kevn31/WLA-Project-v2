using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace FakePhysics
{
    public class Hub_Input : MonoBehaviour

    {
        #region Variables
        public bool smokeW, smokeR;

        [SerializeField]
        private bool rollUnactive, throttleUnactive;
        private bool calibrationCompleted = false;
        [SerializeField]
        private float f_StickyThrottleValor, f_xSpeed;
        public float f_maxSpeed;
        public float reactivity;
        private Vector3 pastRotationForTheYaw;
        private Vector3 pastRotationForTheRoll;

        public float f_pitch = 0f;
        public float f_actualPitch = 0f;

        public float f_roll = 0f;
        public float f_yaw = 0f;
        protected float f_throttle = 0f;
        public float f_throttleSpeed, f_pitchSpeed = 0.5f;

        public float f_speed = 0f;

        public Slider ValeurSlider;

        public float f_stickyThrottle;
        public float f_StickyThrottle
        {
            get { return f_stickyThrottle; }
        }

        public GameObject T_Thumbstick;

        private float canTurn = 0;

        public float PitchSensitivity;
     [SerializeField]
        public float maxTurn, reactivity_Roll, reactivity_Pitch, maxInclinationAngle, f_minSpeed, maxMultiplicator, minMultiplicator;
        private float accerlerationX, accerlerationY, f_rollTurn, pastRotationRollZ, pastRotationRollY, ReactivityRollTurnZ, ReactivityRollTurnY, turnMultiplicao, f_speedMultiplicator, inclinMinX, inclinMinY;
        private float countDown = 1.0f;

        private Vector3 lastPosition;
        #endregion



        // Use this for initialization
        void Start()
        {
            f_rollTurn = 0.75f;
            f_stickyThrottle = 1f;

            if (throttleUnactive)
            {
                T_Thumbstick.SetActive(false);
            }
            f_speedMultiplicator = 1;
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(f_actualPitch);
            //Debug.Log(f_pitch);
           // Debug.Log(Input.acceleration.y);
          


            if (!calibrationCompleted) calibration();
            else
            {
                setGyroAngleMax();
                StickyThrottleControl();
                HandleInput();
                setSpeed();
                setPitch();
                setYaw();
                setRoll();
                setSpeedMultiplicator();
                ClampInputs();

                transform.Translate(Vector3.forward * Time.deltaTime * f_speed * f_speedMultiplicator);
            }

        }

        public void SetAngleMort()
        {

        }

        //Create a Throttle Value that gradually grows and shrinks
        protected virtual void StickyThrottleControl()
        {
            if (!throttleUnactive)
            {
                f_stickyThrottle = f_stickyThrottle + (-f_throttle * f_throttleSpeed * Time.deltaTime);

                //f_stickyThrottle = ValeurSlider.value;
                ValeurSlider.value = f_stickyThrottle;
            }

        }

        protected virtual void HandleInput()
        {
            //Process Main Control Input

            //f_pitch = Input.GetAxis("Pitch_manette");


            if (Input.acceleration.y < inclinMinY + 0.1 && Input.acceleration.y > inclinMinY - 0.1)
            {
                accerlerationY = 0;
            }
            else
            {
                f_pitch = accerlerationY * reactivity_Pitch;
            }


            if (Input.GetButton("SmokeW_manette"))
            {
                Debug.Log(Input.GetButton("SmokeW_manette"));
                smokeW = true;

                //commencer fx smoke blanc
            }
            else if (!Input.GetButton("SmokeW_manette"))
            {
                smokeW = false;

                //finir fx smoke
            }

            if (Input.GetButton("SmokeR_manette"))
            {
                smokeR = true;

                //commencer fx smoke rouge
            }
            else if (!Input.GetButton("SmokeR_manette"))
            {
                smokeR = false;

                //finir fx smoke
            }


            if (!rollUnactive)
            {
                f_rollTurn = Input.GetAxis("Roll_manette");
                
                if (Input.acceleration.x < 0.05 && Input.acceleration.x > -0.05)
                {
                    accerlerationX = 0;
                }
                else
                {
                    f_roll = -accerlerationX * reactivity_Roll;
                    Debug.Log(f_roll);
                }
                
            }


            if (Input.GetAxis("YawDroite_manette") != 0)
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
            f_pitch = Mathf.Clamp(f_pitch, -0.5f, 1f);
            f_actualPitch = Mathf.Clamp(f_actualPitch, -0.5f, 1f);
            f_roll = Mathf.Clamp(f_roll, -1f, 1f);
            f_yaw = Mathf.Clamp(f_yaw, -1f, 1f);
            f_throttle = Mathf.Clamp(f_throttle, -1f, 1f);
            f_stickyThrottle = Mathf.Clamp(f_stickyThrottle, 0f, 1f);
            f_speed = Mathf.Clamp(f_speed, f_minSpeed, f_maxSpeed);
            //  accerlerationX = Mathf.Clamp(accerlerationX, -1f, 1f);
            // accerlerationY = Mathf.Clamp(accerlerationY, -1f, 1f);
        }


        protected void setSpeed()
        {
            f_xSpeed = f_maxSpeed * f_stickyThrottle;

            if (f_speed < f_xSpeed)
            {
                f_speed += (f_stickyThrottle * reactivity) * Time.deltaTime;
            }

            else if (f_speed > f_xSpeed)
            {
                f_speed -= reactivity * Time.deltaTime;
            }

            // f_speed *= f_speedMultiplicator;

        }

        protected void setSpeedMultiplicator()
        {
            float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
            lastPosition = transform.position;

            f_speedMultiplicator = Mathf.Clamp(f_speedMultiplicator, 0.5f, 2);

            //Debug.Log(speed);

            if (transform.eulerAngles.x > 10 && transform.eulerAngles.x < 180)
            {
                f_speedMultiplicator = ((transform.eulerAngles.x * 2) / 180) + 1;
            }

            else if (transform.eulerAngles.x < 350 && transform.eulerAngles.x > 180)
            {
                f_speedMultiplicator = (transform.eulerAngles.x / 180) - 1;
            }
            else
            {

                f_speedMultiplicator = 1f;
            }
        }


        protected void setPitch()
        {
            if (f_pitch == 0)
            {
                if (f_actualPitch < 0)
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



            transform.Rotate((f_pitch * Time.deltaTime) * PitchSensitivity, 0, 0, Space.Self);
        }



        protected void setYaw()
        {
            if (f_yaw == 0)
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
            float pastRotationRoll = pastRotationForTheRoll.z + ((f_roll * Time.deltaTime) * reactivity_Roll);
            transform.rotation = Quaternion.Euler(pastRotationForTheRoll.x, pastRotationForTheRoll.y, pastRotationRoll);
        }

        protected void setGyroAngleMax()
        {
          //  Debug.Log(Input.acceleration.y);
            // maxInclinationAngle, accerlerationX, accerlerationY;

            if (Input.acceleration.x > -maxInclinationAngle && Input.acceleration.x < maxInclinationAngle)
            {
                accerlerationX = Input.acceleration.x / maxInclinationAngle;
            }

            if (Input.acceleration.y > -maxInclinationAngle && Input.acceleration.y < maxInclinationAngle)
            {
                accerlerationY = (Input.acceleration.y - inclinMinY) / maxInclinationAngle;
            }


        }

        protected void setRollTurn()
        {
            pastRotationForTheRoll = transform.eulerAngles;

            if (f_rollTurn < 0)
            {
                if (pastRotationRollZ > 320 || pastRotationRollZ < 180)
                {
                    ReactivityRollTurnZ = 200;
                    ReactivityRollTurnY = 20;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((-f_rollTurn * Time.deltaTime) * ReactivityRollTurnZ);
                    pastRotationRollY = transform.eulerAngles.y;
                }
                else if (pastRotationRollZ > 270 || pastRotationRollZ < 180)
                {
                    ReactivityRollTurnZ -= 50 * Time.deltaTime;
                    ReactivityRollTurnY += 10 * Time.deltaTime;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((-f_rollTurn * Time.deltaTime) * ReactivityRollTurnZ);
                    pastRotationRollY = pastRotationForTheRoll.y + ((f_rollTurn * Time.deltaTime) * ReactivityRollTurnY);
                }
                else
                {
                    if (ReactivityRollTurnY < 50)
                    {
                        ReactivityRollTurnY += 10 * Time.deltaTime;
                    }
                    pastRotationRollY = pastRotationForTheRoll.y + ((f_rollTurn * Time.deltaTime) * ReactivityRollTurnY);
                }


            }
            else
            {

                if (pastRotationRollZ < 40 || pastRotationRollZ > 180)
                {
                    ReactivityRollTurnZ = 200;
                    ReactivityRollTurnY = 20;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((f_rollTurn * Time.deltaTime) * ReactivityRollTurnZ);
                    pastRotationRollY = transform.eulerAngles.y;
                }

                else if (pastRotationRollZ < 90 || pastRotationRollZ > 180)
                {
                    ReactivityRollTurnZ -= 50 * Time.deltaTime;
                    ReactivityRollTurnY += 10 * Time.deltaTime;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((f_rollTurn * Time.deltaTime) * ReactivityRollTurnZ);
                    pastRotationRollY = pastRotationForTheRoll.y + ((-f_rollTurn * Time.deltaTime) * ReactivityRollTurnY);
                }
                else
                {
                    if (ReactivityRollTurnY < 50)
                    {
                        ReactivityRollTurnY += 10 * Time.deltaTime;
                    }
                    pastRotationRollY = pastRotationForTheRoll.y + ((-f_rollTurn * Time.deltaTime) * ReactivityRollTurnY);
                }

            }

            transform.rotation = Quaternion.Euler(pastRotationForTheRoll.x, pastRotationRollY, pastRotationRollZ);
        }



        public void buttonOnClick(bool isRight)
        {

            pastRotationForTheRoll = transform.eulerAngles;

            if (isRight)
            {
                if (pastRotationRollZ > 320 || pastRotationRollZ < 180)
                {
                    ReactivityRollTurnZ = 200;
                    ReactivityRollTurnY = 20;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((-f_rollTurn * Time.deltaTime) * ReactivityRollTurnZ);
                    pastRotationRollY = transform.eulerAngles.y;
                }
                else if (pastRotationRollZ > 270 || pastRotationRollZ < 180)
                {
                    ReactivityRollTurnZ -= 50 * Time.deltaTime;
                    ReactivityRollTurnY += 10 * Time.deltaTime;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((-f_rollTurn * Time.deltaTime) * ReactivityRollTurnZ);
                    pastRotationRollY = pastRotationForTheRoll.y + ((f_rollTurn * Time.deltaTime) * ReactivityRollTurnY);
                }
                else
                {
                    if (ReactivityRollTurnY < 50)
                    {
                        ReactivityRollTurnY += 10 * Time.deltaTime;
                    }
                    pastRotationRollY = pastRotationForTheRoll.y + ((f_rollTurn * Time.deltaTime) * ReactivityRollTurnY);
                }


            }
            else
            {

                if (pastRotationRollZ < 40 || pastRotationRollZ > 180)
                {
                    ReactivityRollTurnZ = 200;
                    ReactivityRollTurnY = 20;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((f_rollTurn * Time.deltaTime) * ReactivityRollTurnZ);
                    pastRotationRollY = transform.eulerAngles.y;
                }

                else if (pastRotationRollZ < 90 || pastRotationRollZ > 180)
                {
                    ReactivityRollTurnZ -= 50 * Time.deltaTime;
                    ReactivityRollTurnY += 10 * Time.deltaTime;
                    pastRotationRollZ = pastRotationForTheRoll.z + ((f_rollTurn * Time.deltaTime) * ReactivityRollTurnZ);
                    pastRotationRollY = pastRotationForTheRoll.y + ((-f_rollTurn * Time.deltaTime) * ReactivityRollTurnY);
                }
                else
                {
                    if (ReactivityRollTurnY < 50)
                    {
                        ReactivityRollTurnY += 10 * Time.deltaTime;
                    }
                    pastRotationRollY = pastRotationForTheRoll.y + ((-f_rollTurn * Time.deltaTime) * ReactivityRollTurnY);
                }

            }

            transform.rotation = Quaternion.Euler(pastRotationForTheRoll.x, pastRotationRollY, pastRotationRollZ);
        }


        public void buttonNotOnClick(bool isRight)
        {
            pastRotationForTheRoll = transform.eulerAngles;
            pastRotationRollZ = pastRotationForTheRoll.z;
            pastRotationRollY = pastRotationForTheRoll.y;

        }



        void calibration()
        {
            if (countDown >= 0.0f)
            {
                countDown -= Time.deltaTime;

            }
            if (countDown < 0.0f)
            {
                calibrationCompleted = true;
            }

            /*calibrationTxt.text = "Calibration running, stay still on playing position";
            countDownTxt.text = Mathf.Round(countDown).ToString();
            calibrationTxt.enabled = true;
            countDownTxt.enabled = true;*/

            if (countDown >= 0.5f)
            {
                if (Input.acceleration.y > -0.5 && Input.acceleration.y < 0)
                {
                    inclinMinY = Input.acceleration.y;
                }
                if (Input.acceleration.x < inclinMinX)
                {
                    inclinMinX = Input.acceleration.x;
                }

                // if (Input.acceleration.y < inclinMinY)
                
            }


        }



        public void setThrottle(float valor)
        {
            throttleUnactive = true;
            f_StickyThrottleValor = valor;
        }


    }
}
