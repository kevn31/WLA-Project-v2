using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeLoveAero;

namespace WeLoveAero
{
   
    public class SaveContentBetweenScenesScript : MonoBehaviour
    {
        //scripts
        EVENTCREATOR_Manager manager;
        //public MainMenuUIScript mainMenuUIScript;
        //
        public string ModeDeJeu;
        public string ModelAvion;
        public bool LogIn;
        public string TypeStage;   //on l utilise en premier lieu pour le mode stage mais il est aussi utilisé pour le choix de l event (dans ce cas la on aura un seul nom d avion qui correspondra à mon choix d avion possible)

        //STATICS
        static public string StaticModeDeJeu;
        static public string StaticModelAvion;
        static public bool StaticLogIn;                                                                                                                       
        static public string StaticTypeStage;
        static public int[] StaticfigureNumberTabSave = new int[50];
        //infos utilisables partout mais reiniit a chaque nouvelle scene
        public bool SaveDataBetweenSceneFinished; // verifie que le script ne se joue que quand les données sont disponibles



        // EVENT

        //public string[] figureNameTabSave = new string[50];
        public int[] figureNumberTabSave = new int[50];

        // Use this for initialization
        void Start()
        {
            SaveDataBetweenSceneFinished = false;
            //  StaticModeDeJeu = ModeDeJeu;
        

            GiveDataAccecible();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("model de l'avion sauvegardé:  " + StaticModelAvion);
        }

        // pour sauvegarder les var statics, il faut les recuperer en normal puis lancer une fonction pour save cette derniere comme ci dessous
        public void saveModeDeJeu ()
        {
            StaticModeDeJeu = ModeDeJeu;
        }

        public void saveModelAvion()
        {
            StaticModelAvion = ModelAvion;
        }

        public void saveLogState()
        {
            StaticLogIn = LogIn;
        }

        public void saveTypeStage()
        {
            StaticTypeStage = TypeStage;
        }

        // au moment du changement de scene, il faut redonner aux var non static la meme valeur que celles des statics pour pouvoir les reutilisaer dans d autres scripts
        public void GiveDataAccecible()
        {
            ModeDeJeu = StaticModeDeJeu;
            ModelAvion = StaticModelAvion;
            LogIn = StaticLogIn;
            TypeStage = StaticTypeStage;

            SaveDataBetweenSceneFinished = true;
            for (int i = 0; i < 6; i++) 
            {
                figureNumberTabSave[i] = StaticfigureNumberTabSave[i];
            }
           
          

        }

        public void SaveEvent()
        {
            /*for (int i = 0; i < 6; i++) //reset
            {
                figureNumberTabSave[i] = null;
            } */

            for (int i = 0; i < 6; i++) //assigne valeurs
            {
                StaticfigureNumberTabSave[i] =  figureNumberTabSave[i] /*manager.tabFigureLibrarySave[i]*/;     //a changer
               // Debug.Log("figure numero" + i + " : " + figureNumberTabSave[i]);
            }
                                                                                                        
        }


        public void WritheValuesEvent()
        {
            for (int i = 0; i < 5; i++) //assigne valeurs
            {
                Debug.Log("figure numero" + i + " : " + StaticfigureNumberTabSave[i]);
            }
           
        }


    }
}
