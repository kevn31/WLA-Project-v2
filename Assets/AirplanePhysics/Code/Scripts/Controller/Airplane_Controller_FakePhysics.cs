using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FakePhysics;



namespace FakePhysics
{
    public enum AirplaneState
    {
        LANDED,
        GROUNDED,
        FLYING,
        CRASHED
    }



    //[RequireComponent(typeof(Airplane_Characteristics))]
    public class Airplane_Controller_FakePhysics : MonoBehaviour
    {

        #region Variables
        //[Header("Base Airplane Properties")]
        //public Airplane_Preset airplanePreset;
        public Hub_Input input;
        //public Airplane_Characteristics charactistics;
        //public Transform centerOfGravity;

        //[Tooltip("Weight is in LBS")]
        //public float airplaneWeight = 800f;

        //[Header("Engines")]
        //public List<Airplane_Engine> engines = new List<Airplane_Engine>();

        //[Header("Wheels")]
        //public List<Airplane_Wheel> wheels = new List<Airplane_Wheel>();

        [Header("Control Surfaces")]
        public List<Airplane_ControlSurface_FakePhysics> controlSurfaces = new List<Airplane_ControlSurface_FakePhysics>();
        #endregion


        #region Properties
        private float currentMSL;
        public float CurrentMSL
        {
            get{return currentMSL;}
        }

        private float currentAGL;
        public float CurrentAGL
        {
            get{return currentAGL;}
        }

        [SerializeField] private AirplaneState airplaneState = AirplaneState.LANDED;
        public AirplaneState State
        {
            get { return airplaneState; }
        }

        private bool isGrounded = true;
        public bool IsGrounded
        {
            get { return isGrounded; }
        }

        private bool isLanded = true;
        public bool IsLanded
        {
            get { return isLanded; }
        }

        private bool isFlying = false;
        public bool IsFlying
        {
            get { return isFlying; }
        }
        #endregion


        #region Constants
        const float poundsToKilos = 0.453592f;
        const float metersToFeet = 3.28084f;
        #endregion





        #region Builtin Methods
        void Start()
        {
    
        }
        #endregion


        // Update is called once per frame
        void Update()
        {
            HandleControlSurfaces();
        }




        #region Custom Methods

        void HandleControlSurfaces()
        {
            if(controlSurfaces.Count > 0)
            {
                foreach(Airplane_ControlSurface_FakePhysics controlSurface in controlSurfaces)
                {
                    controlSurface.HandleControlSurface(input);
                }
            }
        }

    

   

 
        
        #endregion
    }
}
