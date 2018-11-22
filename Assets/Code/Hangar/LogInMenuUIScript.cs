using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WeLoveAero;

namespace WeLoveAero
{
    public class LogInMenuUIScript : MonoBehaviour
    {
        public SaveContentBetweenScenesScript scriptStatic;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayCupAfterLog()
        {
            scriptStatic.LogIn = true;
            scriptStatic.saveLogState();
            SceneManager.LoadScene("CupMenu");
        }
    }
}
