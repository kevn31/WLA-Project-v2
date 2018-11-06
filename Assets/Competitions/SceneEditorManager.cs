using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WeLoveAero;
using System.IO;
using TMPro;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;

namespace WeLoveAero
{
    public class SceneEditorManager : MonoBehaviour
    {
        EVENTCREATOR_Manager manager;

        essai_Manager essai;

        public bool ModificationEvent;
        private static int numberStage = 20;
        private static int numberFigureMax = 10;// max de figures possibles
        private int countFigureActuelle;

        private static int[,] numberFigureArray = new int[numberStage, numberFigureMax];
        private static int[] localArrayFigureSuite = new int[numberFigureMax];
        public int[,] travelingFigureArray = new int[numberStage, numberFigureMax];
        private int curentNumberLevel = 0;
        private int numberFigure;

        private static int levelFigure = 0;
        public int travelingLevelFigure = 0;

        private bool endFigure = false;
        public Text[] textArray;
        private string carre = "square";
        private string nameFigure;
        private Text holly;
        [Header("UI")]

        public GameObject ModifyButton;
        public GameObject SaveModifyButton;
        public GameObject SaveNewEventButton;
        public GameObject PanelEventChoise;
        public GameObject PanelEventEditor;
        public GameObject PanelEventInfosPlayer;
        public Text EventNameText;
        public Text EventDateText;
        public Text InfosEventText;
        public GameObject EventButton;
        public GameObject EventListPanel;
        public GameObject DeleteButton;
        public GameObject PlayerModeButton;
        public GameObject EditorModeButton;

        public GameObject PanelMessageAlert;
        public Text AlertMessageText;

        public InputField EventNameInputfield;
        //public Text EventNameContainer;
        public string EventName; // pas forcement indispensable
        public InputField EventUrlInputfield;
        public string EventUrl;

        public InputField DateBeginTrainingInputField;

        public InputField MiniatureUrlInputfield;

        public int LocalNombreBouton;
        public static int PublicNombreBouton;

        private Text numberEvent;
        private GameObject buttonInstantiating;

        public Button yourButton;

        private float chrono;

        private bool boutonEventGenerated;

        private bool canBeSave;

        private EventBoutonScript btnScript;

        public Text eventInfosContainer;
        public TextMeshProUGUI eventInfosInputfield;
        private string infosTampon;

        public bool editorModBeActive;
        public DateTime dtBegin;

        // private MailMessage mail = new MailMessage();

        // public RawImage img;

        public DateTime dt;

        void Start()
        {
            manager = GameObject.Find("MySqlManager").GetComponent<EVENTCREATOR_Manager>();
        }

        public void GenerationBoutons()
        {
            manager.GetNombreEvent();
           // Debug.Log("nombre event" + manager.nombreEvent);
            // DestroyButtonsEvent();
            if (boutonEventGenerated == false)
            {

                LocalNombreBouton = 0;
                bool pause = false;
               // manager.GetNombreEvent();
                //Debug.Log("nombre event" + manager.nombreEvent);
                /*  if (dt.ToString("yyyy-MM-dd HH:mm:ss") > IDateDebutEvent)//si la date de début est passée
                  { */
                for (int i = 0; i < manager.nombreEvent; i++)//5
                {

                    if (pause == false)
                    {
                        chrono = 5.0f;
                        while (chrono > -0.1f)
                        {
                            if (chrono > 0.2f)
                            {
                                pause = true;
                            }
                            chrono -= Time.deltaTime;
                            // manager.CheckButtonEvent(LocalNombreBouton);
                            if (chrono < 0.1f && pause == true)
                            {

                                /*if (manager.test == LocalNombreBouton)
                                    {  */
                                Button btn = yourButton.GetComponent<Button>();

                                btn = Instantiate(yourButton, EventListPanel.transform);
                                LocalNombreBouton = LocalNombreBouton + 1;
                                manager.globalNombreBouton = LocalNombreBouton;
                                PublicNombreBouton = LocalNombreBouton;
                                //Debug.Log(PublicNombreBouton);
                                numberEvent = btn.GetComponentInChildren<Text>();
                                btnScript = btn.GetComponent<EventBoutonScript>();
                                numberEvent.text = " Event " + LocalNombreBouton;
                                btnScript.tsointsoin(LocalNombreBouton);   //donne la bonne valeur a chaque bouton
                                pause = false;
                                // }

                            }
                        }
                    }
                }
                //}
                boutonEventGenerated = true;
            }

        }

        public void GetToServerFigures()
        {
            manager.ReceiveInfoToServeur2();
        }

        public void GetEventInfos()
        {
            Debug.Log(manager.IEventInfos);
        }

        public void ClicOnEventButton() //fait apparaitre les bouton receive
        {

            StartCoroutine(AfficheInfosButton());
           

                // manager.test
                // manager.ReceiveInfoToServeur2;
        }

        IEnumerator AfficheInfosButton()
        {
            yield return new WaitForSeconds(1.0f);
           
                InfosEventText.text = manager.IEventInfos;
                EventNameText.text = manager.IEventName;
                EventDateText.text = manager.IDateDebutEventGeneral;

        }
    }


}
