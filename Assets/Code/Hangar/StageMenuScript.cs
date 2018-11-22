using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WeLoveAero;

namespace WeLoveAero
{

    public class StageMenuScript : MonoBehaviour
    {
        public SaveContentBetweenScenesScript scriptStatic;

        public GameObject playButton;


        // Use this for initialization
        void Start()
        {
            playButton.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TypeDeStage(string LocalTypeStage)
        {
            scriptStatic.TypeStage = LocalTypeStage;
            scriptStatic.saveTypeStage();
          //  Debug.Log("type de stage:  " + scriptStatic.TypeStage);
            playButton.SetActive(true);

        }
       
        public void PlayButton()
        {
            SceneManager.LoadScene("HangarScene");// a changer
        }

        public void BackButton()
        {
                SceneManager.LoadScene("MainMenu 1");   
        }
    }
}


