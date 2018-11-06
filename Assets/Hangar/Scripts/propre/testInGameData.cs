using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeLoveAero;

namespace WeLoveAero
{
    public class testInGameData : MonoBehaviour
    {
        public SaveContentBetweenScenesScript testSccript;
        public bool test;

        // Use this for initialization
        void Start()
        {
            Debug.Log("model de l avion:  " + testSccript.ModelAvion);
            test = false;
           // Debug.Log( "model de l avion:  " + testSccript.StaticModelAvionTest);
        }

        void Update()
        {
            if (test == true)
            {
                //testSccript.ModelAvion = null;
                Debug.Log("model de l avion:  " + testSccript.ModelAvion);
            }
        }
    }
}
