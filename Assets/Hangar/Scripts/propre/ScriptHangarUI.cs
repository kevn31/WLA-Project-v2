using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WeLoveAero;


namespace WeLoveAero
{
    public class ScriptHangarUI : MonoBehaviour
    {
        public SaveContentBetweenScenesScript scriptStatic;

        public GameObject ButtonPlayHangar;
        private GameObject planeShowed;
        GameObject[] AvionsTypeVoltige;
        GameObject[] AvionsTypeChasse;
        GameObject[] AvionsTypeLigne;

        private bool bePlanesAviable;//pour eviter que la fonction ne se lise en boucle

        // text infons avions
        public Text Textcontainer;
        public Text infosF35;
        public Text infosExtra330;
        public Text infosA350;
        public Text infosA220;

        void Start()
        {
            bePlanesAviable = false;
            AvionsTypeVoltige = GameObject.FindGameObjectsWithTag("Voltige");
            AvionsTypeChasse = GameObject.FindGameObjectsWithTag("Chasse");
            AvionsTypeLigne = GameObject.FindGameObjectsWithTag("Ligne");
            scriptStatic.ModelAvion = null;
            
            //Debug.Log("mode de jeu test  " + scriptStatic.ModeDeJeu);
           // SetActivePlayButton();
            ButtonPlayHangar.SetActive(false);
        }

        void Update()
        {
            if (scriptStatic.SaveDataBetweenSceneFinished == true && bePlanesAviable == false)
            {
                SetActivePlaneButtons();
                bePlanesAviable = true;
            }
                Debug.Log("type de stage dans le hangar: " + scriptStatic.TypeStage);
        }

       public void PlaneShow() //montre le model 3d
        {
            if (planeShowed != null)   //si premier avion selectionne
            {
                planeShowed.transform.position = new Vector3(10, 101, 10);
            }

            planeShowed = GameObject.FindGameObjectWithTag(scriptStatic.ModelAvion).gameObject;
            planeShowed.SetActive(true);
            Vector3 spawnPosition = new Vector3(0, 1, 0);
            planeShowed.transform.position = spawnPosition;
        }

        public void PlayButton()
        {
           SceneManager.LoadScene("InGameTest");// a changer
        }

        public void BackButton()
        {
            /*  if (scriptStatic.ModeDeJeu == "HangarMode")
                  SceneManager.LoadScene("MainMenu 1");

              if (scriptStatic.ModeDeJeu == "FreeMode")
                  SceneManager.LoadScene("MainMenu 1");

              if (scriptStatic.ModeDeJeu == "StageMode")
                  SceneManager.LoadScene("ChoixDuStageScene");

              if (scriptStatic.ModeDeJeu == "CupMode")
                  SceneManager.LoadScene("CupMenu");*/
            SceneManager.LoadScene("MainMenu");
        }

         public void avionSelected(string PlaneCLic)
         {
            scriptStatic.ModelAvion = PlaneCLic;
            scriptStatic.saveModelAvion();
            Debug.Log("avion choisi par le joueur:  " + scriptStatic.ModelAvion);
            AffichageInfos();
            PlaneShow();
            SetActivePlayButton();
         }

        public void AffichageInfos()
        {
             if (scriptStatic.ModelAvion == "F-35")
            {
                Textcontainer.text = infosF35.text;
            }

            if (scriptStatic.ModelAvion == "Extra330")
            {
                Textcontainer.text = infosExtra330.text;
            }

            if (scriptStatic.ModelAvion == "A350")
            {
                Textcontainer.text = infosA350.text;
            }

            if (scriptStatic.ModelAvion == "A220")
            {
                Textcontainer.text = infosA220.text;
            }
        }

        public void SetActivePlayButton()  //se lance lors du start ou quand un bouton plane est clic
        {
          /*  if (scriptStatic.ModelAvion == null)
            {
                ButtonPlayHangar.SetActive(false);
            }   */
           /* if (scriptStatic.ModeDeJeu == "HangarMode")// dans le mode hangar on ne peut pas play 
            {
                ButtonPlayHangar.SetActive(false);
                
            }*/
           /* if (scriptStatic.ModeDeJeu == "CupMode" || scriptStatic.ModeDeJeu == "FreeMode" || scriptStatic.ModeDeJeu == "StageMode")
            {*/

                if (scriptStatic.ModelAvion == null)
                {
                    ButtonPlayHangar.SetActive(false);
                }

                else

                {
                    ButtonPlayHangar.SetActive(true);
                }
           // }
        }

      public void SetActivePlaneButtons()
        {

            Debug.Log("tets" + scriptStatic.TypeStage); // type stage se lit bien dans l update mais pas au moment du start c est chelou
           
            if (scriptStatic.ModeDeJeu == "StageMode" || scriptStatic.ModeDeJeu == "CupMode")
            {
                if (scriptStatic.TypeStage != null)
                {
                    if (scriptStatic.TypeStage == "Chasse")
                    {
                        foreach (GameObject r in AvionsTypeLigne)
                        {
                            Destroy(r.gameObject);
                        }
                        foreach (GameObject r in AvionsTypeVoltige)
                        {
                            Destroy(r.gameObject);
                        }
                    }
                    if (scriptStatic.TypeStage == "Ligne")
                    {
                        foreach (GameObject r in AvionsTypeVoltige)
                        {
                            Destroy(r.gameObject);
                        }
                        foreach (GameObject r in AvionsTypeChasse)
                        {
                            Destroy(r.gameObject);
                        }
                    }
                    if (scriptStatic.TypeStage == "Voltige")
                    {
                        foreach (GameObject r in AvionsTypeLigne)
                        {
                            Destroy(r.gameObject);
                        }
                        foreach (GameObject r in AvionsTypeChasse)
                        {
                            Destroy(r.gameObject);
                        }
                    }
                    if (scriptStatic.TypeStage == "All")
                    {
                          // aucun avion a suprimer
                    }
                }
                    if (scriptStatic.ModeDeJeu == "FreeMode")
                {
                    Debug.Log("tous les types d'avions");
                }
            }
        }  
    }  
}
