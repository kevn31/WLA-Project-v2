using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WeLoveAero;

namespace WeLoveAero
{
    public class MainMenuUIScript : MonoBehaviour
    {
      [Header("Manager")]

       public SaveContentBetweenScenesScript scriptStatic;
        
        void Start()
        {

        }

        public void PlayFreeMode()
        {
            scriptStatic.ModeDeJeu = "FreeMode";
            scriptStatic.saveModeDeJeu();// pour que la donnée soit conservée en static

            SceneManager.LoadScene("HangarScene");
        }

        public void PlayStagesMode()
        {
            scriptStatic.ModeDeJeu = "StageMode";
            scriptStatic.saveModeDeJeu();
            SceneManager.LoadScene("ChoixDuStageScene");
        }

        public void PlayCupMode()
        {
            scriptStatic.ModeDeJeu = "CupMode";
            scriptStatic.saveModeDeJeu();

             if (scriptStatic.LogIn == true)
               {
                   SceneManager.LoadScene("CupMenu");
               }
             if (scriptStatic.LogIn == false)           
               {
                // SceneManager.LoadScene("Database");
                SceneManager.LoadScene("LogInSceneTemporaire");
            }    
        }

        public void PlayHangarMode ()
        {
            scriptStatic.ModeDeJeu = "HangarMode";
            scriptStatic.saveModeDeJeu();
            SceneManager.LoadScene("HangarScene");//on charge la meme scene qu en freeFlight mais avec la 
            //le mode de jeu en hangar, je vais neutraliser la possibilité de play
        }

        public void PlayWeLoveAero()
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.welove.aero");
        }

    }
}
