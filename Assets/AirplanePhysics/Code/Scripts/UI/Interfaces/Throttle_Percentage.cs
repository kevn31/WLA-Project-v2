using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace FakePhysics
{

    public class Throttle_Percentage : MonoBehaviour
    {
       public Text percentageText;
       public float ValeurThrottle;

        // Use this for initialization
        void Start()
        {
            percentageText = GetComponent<Text> ();
         
        }

        public void textUpdate (float Value)
        {
            percentageText.text = Mathf.Round(Value * 100) + " %";
            //Debug.Log(Value);

            ValeurThrottle = Mathf.Round(Value * 100);

        }
    }
}
