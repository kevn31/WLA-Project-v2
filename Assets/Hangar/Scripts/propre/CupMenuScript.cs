using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WeLoveAero;


namespace WeLoveAero
{
    public class CupMenuScript : MonoBehaviour
    {
        public SaveContentBetweenScenesScript scriptStatic;

        public GameObject ButtonPlayCup;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetActivePlayButton()  //se lance lors du start ou quand un bouton plane est clic
        {
          
                if (scriptStatic.TypeStage == null)
                {
                    ButtonPlayCup.SetActive(false);
                }

                else
                {
                    ButtonPlayCup.SetActive(true);
                }
            
        }

        public void PlayButton()
        {
            SceneManager.LoadScene("HangarScene");
        }

        public void BackButton()
        {
            SceneManager.LoadScene("MainMenu 1");
        }


        public void TypeEventChoisi(string LocalTypeEvent)   //on utilise la variable typeStage pour le choix de l event
        {
            scriptStatic.TypeStage = LocalTypeEvent;
            scriptStatic.saveTypeStage();
        }
    }
}
