using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using WeLoveAero;

/*namespace WeLoveAero
{ */
    public class MainMenu : MonoBehaviour
    {

        DB_Manager manager;
        //EVENTCREATOR_Manager managerEvent;
        InfosTransition infosTransition;
        ChoixManette choixManette;
        SaveContentBetweenScenesScript saveContentBetweenScripts;

        public static bool BeLogin;
        public GameObject[] cadenas;
        public GameObject[] cadenasStages;
        public Button _ButtonStageCheck;
        public Button buttonblocked;
        public Button _ButtonLog;
        public Button ConnectionButton;
        public Button DeconnectionButton;
        public int currentStagePlayer;
        public InputField pseudoInputfield;
        public GameObject PanelInfosMission;
        public GameObject ButtonPlayCompetition;
        public GameObject PanelAlertMission;
        public GameObject PanelAlertMainMenu;
        public GameObject PanelCompetitionsList;
        public GameObject PopUpMission1;
        public GameObject PopUpMission2;
        public GameObject PopUpMission3;
        public GameObject PopUpMission4;
        public GameObject PopUpMission5;

    public GameObject SeenVisualizationButton;

    public GameObject hangarCanvas;
        // public GameObject PopUpMission6;
        //public GameObject PopUpMission7;

        public int NumMission;

        public Text DescriptionStageText;
        public String textContent;


        // Use this for initialization
        void Start()
        {

        hangarCanvas.SetActive(false);
        // SeenVisualizationButton.SetActive(false);
        saveContentBetweenScripts = GameObject.Find("_Managers").GetComponent<SaveContentBetweenScenesScript>();
            //DescriptionStageText = new Text;
            StartCoroutine(PreloadMenu());
            textContent = "j'ai changé le texte";
            DescriptionStageText.text = textContent;

            for (int i = 0; i < 6; i++) //reset
            {
                //saveContentBetweenScripts.figureNumberTabSave[i] = 0;
                saveContentBetweenScripts.figureNumberTabSave[i] = 0;
            }
        }

        IEnumerator PreloadMenu()
        {
            yield return new WaitForSeconds(0.3f);
            manager = GameObject.Find("MySqlManager").GetComponent<DB_Manager>();
            // managerEvent = GameObject.Find("MySqlManager").GetComponent<EVENTCREATOR_Manager>();
            cadenas = GameObject.FindGameObjectsWithTag("Cadenas");
            currentStagePlayer = 5;

            if (cadenas.Length > 0)
            {
                foreach (GameObject cadena in cadenas)
                {
                    cadena.SetActive(false);
                }
            }

            CheckLogin();
            infosTransition = GameObject.Find("InfosStock").GetComponent<InfosTransition>();
            choixManette = GameObject.Find("Stages").GetComponent<ChoixManette>();
            infosTransition.CheckManette();
            choixManette.MenuPrincipal();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void beloginToFalse()
        {
            manager.IPseudo = null;
            manager.IBelogin = false;
            manager.SaveBeLogin();
            BeLogin = false;
            CheckLogin();
        }

        public void CheckLogin()
        {
            _ButtonLog = GetComponent<Button>();
            if (manager.IPseudo != null)
            {
                manager.IBelogin = true;
                manager.SaveBeLogin();
                BeLogin = true;
                PanelAlertMainMenu.SetActive(false);
                if (ConnectionButton != null)
                {
                    ConnectionButton.gameObject.SetActive(false);
                    DeconnectionButton.gameObject.SetActive(true);
                }
            }

            else

            {
                PanelAlertMainMenu.SetActive(true);
                if (ConnectionButton != null)
                {
                    ConnectionButton.gameObject.SetActive(true);
                    DeconnectionButton.gameObject.SetActive(false);
                }

                GuestRepercution();
            }
        }

        public void GuestRepercution()
        {
            buttonblocked = GetComponent<Button>();
            if (GameObject.FindGameObjectWithTag("Guest") != null)
            {
                buttonblocked = GameObject.Find("PROFILE").GetComponent<Button>();

                if (cadenas.Length > 0)
                {
                    foreach (GameObject cadena in cadenas)
                    {
                        cadena.SetActive(true);
                    }
                }
                buttonblocked.interactable = false;
            }
        }

        public void BackToDatabase()
        {
            if (cadenas.Length > 0)
            {
                foreach (GameObject cadena in cadenas)
                {
                    cadena.SetActive(true);
                }
            }
            SceneManager.LoadScene("Database");
        }

        #region Stages
        public void checkStageValidation()
        {
            StartCoroutine(StageValidation());  
        }

        IEnumerator StageValidation()
        {
        yield return new WaitForSeconds(1.0f);
        _ButtonStageCheck = GetComponent<Button>();

            for (int i = 1; i < 6; i++)// 8 = totalStages
            {
                Debug.Log("pass par ici:" + i);
                _ButtonStageCheck = GameObject.Find("Stagestest" + i).GetComponent<Button>();

                if (i <= currentStagePlayer)// si i inferieur au stage actuel (par raport au dernier stage complete, la var change à la fin de chaque stage)
                {
                    _ButtonStageCheck.interactable = true;
                }
            }

            if (currentStagePlayer == 5 && BeLogin == false)
            {
                _ButtonStageCheck.interactable = false;
            }
            if (currentStagePlayer == 5 && BeLogin == true)
            {
                _ButtonStageCheck.interactable = true;
            }
        }

        public void incrementStages()
        {
            currentStagePlayer++;
            PlayerPrefs.SetInt("currentStagePlayer", currentStagePlayer);
        }

        public void LoadLocalInfos()
        {
            currentStagePlayer = PlayerPrefs.GetInt("currentStagePlayer");
        }

        public void PopUpMissions(int numMission)
        {
            NumMission = numMission;
            // PopUp = GameObject.Find("PanelDetailsMission"+ numMission);
            if (numMission == 1)
            {
                PopUpMission1.SetActive(true);
            }

            if (numMission == 2)
            {
                PopUpMission2.SetActive(true);
            }

            if (numMission == 3)
            {
                PopUpMission3.SetActive(true);
            }

            if (numMission == 4)
            {
                PopUpMission4.SetActive(true);
            }

            if (numMission == 5)
            {
                PopUpMission5.SetActive(true);
            }

           /* if (numMission == 6)
            {
                PopUpMission6.SetActive(true);
            }

            if (numMission == 7)
            {
                PopUpMission7.SetActive(true);
            }*/
        }

        public void ClosePopUpMissions()
        {
            // PopUp = GameObject.Find("PanelDetailsMission"+ numMission);
            PopUpMission1.SetActive(false);
            PopUpMission2.SetActive(false);
            PopUpMission3.SetActive(false);
            PopUpMission4.SetActive(false);
            PopUpMission5.SetActive(false);
          //  PopUpMission6.SetActive(false);
         //   PopUpMission7.SetActive(false);
        }

        public void VisualizationButton()
        {
        SeenVisualizationButton.SetActive(true);

        }


        public void PlayMissions()
        {
            // PopUp = GameObject.Find("PanelDetailsMission"+ numMission);
            if (NumMission == 1)
            {
                SceneManager.LoadScene("level1");
            }

            if (NumMission == 2)
            {
                SceneManager.LoadScene("level2.1");
            }

            if (NumMission == 3)
            {
                SceneManager.LoadScene("HangarScene");
            }

            if (NumMission == 4)
            {
                SceneManager.LoadScene("HangarScene");
            }

            if (NumMission == 5)
            {
                SceneManager.LoadScene("HangarScene");
            }

           /* if (NumMission == 6)
            {
                SceneManager.LoadScene("HangarScene");
            }

            if (NumMission == 7)
            {
                SceneManager.LoadScene("HangarScene");
            }*/
        }
        #endregion

        public void SavePasswordInMenu()
        {
            manager.LPassword.text = pseudoInputfield.text;
            manager.Login();
        }

        public void GetCadenas()
        {
            StartCoroutine(LoadStageCadenas());
        }

        IEnumerator LoadStageCadenas()
        {
            yield return new WaitForSeconds(0.1f);
            cadenasStages = GameObject.FindGameObjectsWithTag("CadenasStages");

            _ButtonLog = GetComponent<Button>();
            if (manager.IPseudo != null)
            {
                if (cadenasStages.Length > 0)
                {
                    foreach (GameObject cadena in cadenasStages)
                    {
                        cadena.SetActive(false);
                    }
                }
            }

            else

            {
                if (cadenasStages.Length > 0)
                {
                    foreach (GameObject cadena in cadenasStages)
                    {
                        cadena.SetActive(true);
                    }
                }
            }
        }

        public void ResetCadenasStage()
        {
            if (cadenasStages.Length > 0)
            {
                foreach (GameObject cadena in cadenasStages)
                {
                    cadena.SetActive(true);
                }
            }
        }

        #region CompetitionsPage
        public void OpenPanelInfo()
        {
            StartCoroutine(AffichePanelInfo());

        }

        IEnumerator AffichePanelInfo()
        {
            yield return new WaitForSeconds(1.0f);

            PanelInfosMission.SetActive(true);
            PanelCompetitionsList.SetActive(false);
            if (currentStagePlayer < 5)//REGISTER ET CERTIFIE
            {
                PanelAlertMission.SetActive(true);


            }



        }

        /*public void EventFinishedRepercutions()
        {
            if (PanelInfosMission != null)
            {
                 if (infosTransition.EventState == "Closed")
                 {
                     ButtonPlayCompetition.SetActive(false);
                 }
                 else
                 {
                     ButtonPlayCompetition.SetActive(true);
                 }
            }


        }*/

        public void ClosePanelInfo()
        {
            // ButtonPlayCompetition.SetActive(true);
            PanelInfosMission.SetActive(false);
            // PanelCompetitionsList.interactable = true;
            PanelCompetitionsList.SetActive(true);
        }

        public void OpenAlertPanelMainMenu() //si je n ai pas fais le stage 7
        {
            if (manager.IBelogin == true)
            {

            }
        }

        public void CheckButtonBeLogin()
        {
            if (manager.IBelogin == true)
            {

            }

            else

            {
                PanelAlertMainMenu.SetActive(true);
            }
        }

        public void PlayCompetition()
        {
            //loadscene avec l event name 
        }

        public void GoToHangar()
        {
            SceneManager.LoadScene("HangarScene");
        }

        public void ShowHangar()
        {
            hangarCanvas.SetActive(true);
        }

    /* public void ActivePanelCompetitionsList()
     {
         //PanelCompetitionsList.interactable = false;
         PanelCompetitionsList.SetActive(false);
     }*/
    #endregion
}
//}

