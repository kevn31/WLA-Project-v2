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

        public GameObject PlaneEmpty;
        private Vector3 EmptyPosition;
        private GameObject test;

        // Use this for initialization
        void Start()
        {

            PlaneEmpty = GameObject.Find("Airbus_Plane");
            EmptyPosition = PlaneEmpty.transform.position;
          
            //  Instantiate(Pr_Extra300);
            // Pr_Extra300.transform.parent = PlaneEmpty.transform;
            if (scriptStatic.ModelAvion == "Extra330")
            {
                test = Instantiate(Pr_Extra330, EmptyPosition, Quaternion.identity);
                test.transform.parent = PlaneEmpty.transform;
                // Debug.Log("generation du extra330");

                //Instantiate(Pr_Extra330);
                // test.transform.parent = PlaneEmpty.transform;
            }

            if (scriptStatic.ModelAvion == "F-35")
            {
                test = Instantiate(Pr_F35, EmptyPosition, Quaternion.identity);
                test.transform.parent = PlaneEmpty.transform;
            }
            if (scriptStatic.ModelAvion == "A350")
            {
                test = Instantiate(Pr_A350, EmptyPosition, Quaternion.identity);
                test.transform.parent = PlaneEmpty.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
