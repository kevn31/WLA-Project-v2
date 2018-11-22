using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeLoveAero;

namespace WeLoveAero
{
    public class CalculScore : MonoBehaviour
    {
        //LoopPrefabFigure loopPrefabScript;
        private float perfectScore;
        private float goodScore;
        private float badScore;

        private float actualScore;
        private static float scoreTotal;

        private float pourcentGood;
        private float pourcentBad;

        private float nbrCheckpoint;

        private float scoreFigure;

        private GameObject FigureTest;

        private GameObject plane;
        //private LoopPrefabFigure scriptPrefab;
        LoopPrefabFigure scriptPrefab;

        // Use this for initialization                   
        void Start()
        {

           // scriptPrefab = GameObject.Find("Xtra_330").GetComponent<LoopPrefabFigure>();
            scoreFigure = 100;
            pourcentGood = 50;
            pourcentBad = 25;

            SetCheckPointEnd();

            perfectScore = scoreFigure / nbrCheckpoint;
            goodScore = perfectScore * (pourcentGood / 100);
            badScore = perfectScore * (pourcentBad / 100);

            /* Debug.Log(perfectScore);
             Debug.Log(goodScore);
             Debug.Log(badScore);*/
        }




        public void scoreTotalFigure(int nbrPerfect, int nbrGood, int nbrBad)
        {
            /* Debug.Log("Perfect = " + nbrPerfect);
             Debug.Log("Good = " + nbrGood);
             Debug.Log("Bad = " + nbrBad);*/

            if (nbrPerfect + nbrGood + nbrBad <= nbrCheckpoint)
            {
                //Debug.Log("not an error ! ");

                actualScore = (perfectScore * nbrPerfect) + (goodScore * nbrGood) + (badScore * nbrBad);
                //scriptPrefab.scoreTxt.enabled = true;
                // scriptPrefab.CheckpointSuccess.enabled = true;


                if (actualScore == 100)
                {
                    //  scriptPrefab.CheckpointSuccess.color = Color.yellow;
                    // scriptPrefab.CheckpointSuccess.text = "PERFECT Figure !";
                }

                else if (actualScore == 0)
                {
                    //  scriptPrefab.CheckpointSuccess.color = Color.red;
                    //  scriptPrefab.CheckpointSuccess.text = "FIGURE FAILED !";
                }
                else
                {
                    // scriptPrefab.CheckpointSuccess.color = Color.black;
                    // scriptPrefab.CheckpointSuccess.text = "Figure finished";
                }

                scoreTotal += actualScore;
                //   scriptPrefab.scoreTxt.text = scoreTotal.ToString();

                //Debug.Log(actualScore);
                Destroy(gameObject);

            }

            else
            {
                Debug.Log("error");
                actualScore = scoreFigure / 2;

            }

            Destroy(gameObject);
        }


        public void SetCheckPointEnd()
        {

            //plane =  GameObject.FindWithTag("plane");
            //  scriptPrefab = plane.GetComponent<LoopPrefabFigure>();
            scriptPrefab = GameObject.Find("Xtra_330").GetComponent<LoopPrefabFigure>();
            FigureTest = GameObject.FindGameObjectWithTag("onGoingFigure");

            //if (gameObject.transform.name == "loopCollider(Clone)")
            if (FigureTest.transform.name == "loopCollider(Clone)")
            {
                nbrCheckpoint = 11;

                scriptPrefab.checkPointEnd = 11;
                Debug.Log("checkPointEnd : " + scriptPrefab.checkPointEnd);

            }

            // else if (gameObject.transform.name == "slopeCollider(Clone)")
            else if (FigureTest.transform.name == "slopeCollider(Clone)")
            {
                nbrCheckpoint = 5;
                scriptPrefab.checkPointEnd = 5;
                Debug.Log("checkPointEnd : " + scriptPrefab.checkPointEnd);
            }
            else if (FigureTest.transform.name == "ligneDroite(Clone)" || FigureTest.transform.name == "slalom(Clone)")
            {
                // Debug.Log("checkPointEnd" + scriptPrefab.checkPointEnd);
                nbrCheckpoint = 3;
                scriptPrefab.checkPointEnd = 3;
                Debug.Log("checkPointEnd : " + scriptPrefab.checkPointEnd);
            }
            else
            {
                Debug.Log("error");
                scriptPrefab.checkPointEnd = 5;
                Debug.Log("checkPointEnd : " + scriptPrefab.checkPointEnd);
            }
           
        }

    }
}
