using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeLoveAero;


namespace WeLoveAero
{
    public class GenerationPlaneScript : MonoBehaviour
    {
        public SaveContentBetweenScenesScript scriptStatic;

        //Les prefabs des avions
        //public Transform Pr_Extra300;
        public GameObject Pr_Extra330;
        public GameObject Pr_F35;
        public GameObject Pr_A350;

        public GameObject[] plane;

        public GameObject PlaneEmpty;
        private Vector3 EmptyPosition;
        private GameObject test;

        private string ModelAvion;
        private bool alreadyInstantiate = true;

        // Use this for initialization
        void Start()
        {
            alreadyInstantiate = false;
            //test = Instantiate(plane[0]);
        }

        // Update is called once per frame
        void Update()
        {
            if(!alreadyInstantiate)
            {

                if (scriptStatic.ModelAvion == "Extra330")
                {
                    test = Instantiate(plane[0]);
                }

                if (scriptStatic.ModelAvion == "F-35")
                {
                    test = Instantiate(plane[1]);
                }

                if (scriptStatic.ModelAvion == "A350")
                {
                    test = Instantiate(plane[2]);
                }
                alreadyInstantiate = true;
            }
           
        }
    }
}
