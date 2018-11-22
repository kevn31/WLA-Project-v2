using System.Collections;                                                                                                                  
using System.Collections.Generic;
using UnityEngine;
using WeLoveAero;

namespace WeLoveAero
{
    public class FigureManager : MonoBehaviour
    {
        ///////////// script////////////
        SaveContentBetweenScenesScript saveContentScript;

        LoopPrefabFigure scriptLoopPrefabFigure;

        CalculScore calculScoreScript;

        private GameObject plane;
        private Vector3 planePosition;
        private Quaternion planeRotation;

        public GameObject[] prefabFigure;
        public bool alreadyPlace;

        private GameObject newFigure;
        private int futureFigure;

       // private FigureChoice scriptArrayFigure;

        private int numberStage;
        private int numberFigure;
        private int[,] figure;


        private int actualFigure;

        // Use this for initialization
        void Start()
        {
            calculScoreScript = GameObject.Find("_manager").GetComponent<CalculScore>();
            saveContentScript = GameObject.Find("_manager").GetComponent<SaveContentBetweenScenesScript>();
            scriptLoopPrefabFigure = GameObject.Find("Xtra_330").GetComponent<LoopPrefabFigure>();
            numberStage = 0;
            numberFigure = 0;
            actualFigure = 0;

            plane = GameObject.FindWithTag("planeGeneratorFigure");
            alreadyPlace = false;
            futureFigure = 0;
            // scriptArrayFigure = gameObject.GetComponent<FigureChoice>();



            // scriptArrayFigure.setTravelingVariable();
            // figure = scriptArrayFigure.travelingFigureArray;
            //figure[scriptArrayFigure.travelingLevelFigure, i]
            if (!alreadyPlace)
            {
              
                placePlane();
            }
            int v = 0;
            while (v < 10)
            {
              
                Debug.Log("figureNumberTabSave: " + saveContentScript.figureNumberTabSave[v]);
                //  saveContentScript.figureNumberTabSave[actualFigure] = saveContentScript.figureNumberTabSave[actualFigure] + 1;
                v++;
            }

        }

        //LoopPrefabFigure()
       // setScript()
        // Update is called once per frame

        void Update()
        {

            



        }

        public void placePlane()
        {
           // Debug.Log("figureNumberTabSave: " + saveContentScript.figureNumberTabSave[actualFigure]);
            if (newFigure != null )
            {
                Destroy(newFigure);
            }
            else
            {
              
                if (saveContentScript.figureNumberTabSave[actualFigure] != null || saveContentScript.figureNumberTabSave[actualFigure] != 0)
                {
                    planePosition = plane.transform.position;
                    planeRotation = Quaternion.Euler(new Vector3(0, plane.transform.rotation.eulerAngles.y, 0));
                    // Debug.Log(planeRotation);


                    // newFigure = Instantiate(prefabFigure[figure[scriptArrayFigure.travelingLevelFigure, futureFigure]], planePosition, planeRotation);

                    // figure = StaticfigureNumberTabSave
                    // scriptArrayFigure.travelingLevelFigure, futureFigure devient le numero actuel de la figure
                    newFigure = Instantiate(prefabFigure[saveContentScript.figureNumberTabSave[actualFigure]], planePosition, planeRotation);
                    alreadyPlace = true;

                    //Debug.Log(newFigure);
                    // scriptLoopPrefabFigure = plane.GetComponent<LoopPrefabFigure>();    //repere et permet d appeler le script en local


                    scriptLoopPrefabFigure.setScript();
                    futureFigure++;
                    actualFigure++;

                    calculScoreScript.SetCheckPointEnd();
                    Debug.Log("competion");

                }
                else
                {
                    Debug.Log("fin de la competition");
                }

            }
           
            // Debug.Log("PlacePlane");



        }

        public void NextFigure()
        {

        }

        public void allowToPlace()
        {
            Debug.Log("working");
            StartCoroutine(alreadyPlaceToFalse());
        }

        IEnumerator alreadyPlaceToFalse()
        {
            yield return new WaitForSeconds(0.5f);

  
            alreadyPlace = false;
           // NextFigure();
            //placePlane();
        }
    }
}
