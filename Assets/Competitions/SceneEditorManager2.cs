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
//using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;

//using GooglePlayGames;


namespace WeLoveAero
{
    public class SceneEditorManager2 : MonoBehaviour
    {
        /// <summary>
        /// Update les tableaux pour stocker les enchainements de figures, 20 events max de 5 figures Max
        /// Index Figure : 
        /// 0 = End
        /// 1 = Loop
        /// 2 = montée
        /// 3 = virage droite
        /// 4 =  virage gauche
        /// </summary>

        EVENTCREATOR_Manager manager;

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
        public static int PublicNombreBouton ;

        private Text numberEvent;
        private GameObject buttonInstantiating;

        public  Button yourButton;

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
        void Start ()
        {
           /* WWW www = new WWW("https://static.wixstatic.com/media/3808b8_033ae6d0cced405a957339a3a3834a0a~mv2.jpg/v1/fill/w_281,h_197,al_c,lg_1,q_80/3808b8_033ae6d0cced405a957339a3a3834a0a~mv2.webp");
            yield return www;

            img.texture = www.texture;
            img.SetNativeSize();
            img = gameObject.GetComponet<RawImage>();  */
            //temporaire//////////////////////////
            editorModBeActive = true;
            EditorModeButton.SetActive(false);
            PanelEventInfosPlayer.SetActive(false);
            ////////////////////////////////////////
            //TextMeshPro  eventInfosInputfield = GetComponent< TextMeshPro > ();
            //ImportImage();
            PanelMessageAlert.SetActive(false);
            canBeSave = false;
            boutonEventGenerated = false;
            chrono = 0.5f;
            LocalNombreBouton = 0 ;
            PanelEventChoise.SetActive(true);
            PanelEventEditor.SetActive(false);
            ModifyButton.SetActive(false);
            DeleteButton.SetActive(false);
            numberFigure = 0;      
           manager = GameObject.Find("managerEditor").GetComponent<EVENTCREATOR_Manager>();
            if (gameObject.name != "MANAGER")
            {
                for (int i = 0; i < textArray.Length; i++)
                {
                    textArray[i].text = " ";
                }
            }
        } 

        // Update is called once per frame
        void Update () {
		
	    }

        public void clickButtonFigureChoice (int numeroFigure)
        { 
            if (numeroFigure == 0/* ||*/ )
            {
                endFigure = true;
            }
        
            if(!endFigure)
            {
                
                textArray[numberFigure].text = nameFigure;
                textArray[numberFigure].color = Color.white;
               //numberFigureArray[curentNumberLevel, numberFigure] = numeroFigure; 
               localArrayFigureSuite[numberFigure] = numeroFigure;
                manager.numberOrderFigureTab[numberFigure] = numberFigure;   // save les figures par ordre d appel (facultatif) 
                Debug.Log("test lol = " + manager.numberOrderFigureTab[numberFigure]);
                manager.tabFigureLibrarySave[numberFigure] = numeroFigure;//STOCK l identite de la figure
                numberFigure++;
            }
        } 

        public void SelectEmplacementFigure(int numeroPlacement)
        {
            numberFigure = numeroPlacement;
            Debug.Log("plecemnt numero: " + numberFigure);
        }


        public void testInfos()//a supprimer
        {
           
            
            Debug.Log("infos event: " + manager.IEventInfos);
        }

        public void SaveFigure()                                                                               
        {
            if (EventNameInputfield.text != "") //si vous avez rentré un titre
            {
                PanelMessageAlert.SetActive(true);
                AlertMessageText.text = "L'event à été partagé en ligne";
                SaveNewEventButton.SetActive(false);
                manager.NewEventGetId();
                //manager.CheckButtonEvent(LocalNombreBouton);
                Debug.Log("repere comme un nouvel event");
                manager.IEventName = EventNameInputfield.text;
                manager.IEventInfos = eventInfosInputfield.text;
                manager.IEventUrl = EventUrlInputfield.text;
               // dtBegin.ToString("yyyy-MM-dd HH:mm:ss") = DateBeginTrainingInputField.text;
                manager.IDateDebutEventGeneral = DateBeginTrainingInputField.text;
                // infosTampon= eventInfosInputfield.text;

                Debug.Log("infos tampon "+ infosTampon);
                Debug.Log("IEventInfos " + manager.IEventInfos);
                // manager.IEventInfos = eventInfosInputfield.text;
                int i = 1;
                Debug.Log("sauvegarde locale");
                // while( i <= manager.countFigureTotal /*+ 5*/)
                while (i <=/* manager.numberFigureOfEvent[manager.ActuelFigureSave]*/  5)
                {
                    manager.ActuelFigureSave = manager.numberOrderFigureTab[i];
                    Debug.Log("ActuelFigureSave: " + manager.ActuelFigureSave);
                    manager.CreateFigureCreation();
                    //Debug.Log("CREATION FIGURE");
                    //Debug.Log("figure :  " + manager.IFigureName + " has saved");
                    i++;
                }
            }

            else
            {
                PanelMessageAlert.SetActive(true);
                AlertMessageText.text = "Vous devez rentrer un nom d'event";
            }
        }


            public void SaveMOdificationsFigure()
            {
            if (EventNameInputfield.text != "") //si vous avez rentré un titre
            {
                for (int r = 0; r < 5; r++)
                {
                    if (textArray[r].color == Color.yellow)  //si la modification n est pas complete, vous ne pouvez pas la sauvegarder
                    {
                        PanelMessageAlert.SetActive(true);
                        AlertMessageText.text = "Vous devez remplir toute les cases";
                        Debug.Log("vous devez remplir toutes les cases");
                        canBeSave = false;
                        return;
                        // r = 0;
                    }
                    else
                    {
                       
                        SaveModifyButton.SetActive(false);
                        canBeSave = true;
                        PanelMessageAlert.SetActive(true);
                        AlertMessageText.text = "Les modifications ont étés sauvegardées";
                        manager.IEventName = EventNameInputfield.text;
                        manager.IEventInfos = eventInfosInputfield.text;
                        manager.IEventUrl = EventUrlInputfield.text;

                       // dtBegin = DateBeginTrainingInputField.text;
                        manager.IDateDebutEventGeneral = DateBeginTrainingInputField.text;
                    }
                }
                if (canBeSave == true)
                {
                    int i = 1;
                    // while( i <= manager.countFigureTotal /*+ 5*/)
                    while (i <=/* manager.numberFigureOfEvent[manager.ActuelFigureSave]*/  5)
                    {
                        manager.ActuelFigureSave = manager.numberOrderFigureTab[i];
                        Debug.Log("ActuelFigureSave: " + manager.ActuelFigureSave);
                        manager.ModifieInfoToServer2();
                        Debug.Log("MODIFICATION FIGURE");
                        Debug.Log("figure :  " + manager.IFigureName + " has saved");
                        // manager.CreateFigureCreation();
                        i++;
                    }
                    int r = 1;
                    Debug.Log("sauvegarde locale");

                    while (r <= 5)
                    {
                        manager.ActuelFigureSave = manager.numberOrderFigureTab[r];
                        Debug.Log("ActuelFigureSave: " + manager.ActuelFigureSave);
                        manager.CreateFigureCreation();
                        r++;
                    }
                    //SaveFigure();
                }
            }
            else
            {
                PanelMessageAlert.SetActive(true);
                AlertMessageText.text = "Vous devez rentrer un nom d'event";
            }
        }   

        public void getNameFigure(string name)
        {
            //manager.numberFiguresOfEvent[numberFigure] = countFigureTotal; //sauvegarde du nombre de figures total uniquement dan sla derniere case du tableau
            nameFigure = name;        
        }
        public void selectionLvlFinished()
        {
            //curentNumberLevel = int.Parse(levelNumber);

            for (int i = 0; i < textArray.Length; i++)
            {
                /* if (numberFigureArray[curentNumberLevel, i] == 0)
                 {
                     textArray[i].text = "";
                 }

                 if (numberFigureArray[curentNumberLevel, i] == 1)
                 {
                     textArray[i].text = "Looping";
                 }

                 if (numberFigureArray[curentNumberLevel, i] == 2)
                 {
                     textArray[i].text = "Montée";
                 }
                  if (numberFigureArray[curentNumberLevel, i] == 3)
                 {
                     textArray[i].text = "virage a droite";
                 }
                  if (numberFigureArray[curentNumberLevel, i] == 4)
                  {
                      textArray[i].text = "virage a gauche";
                  }  */
         
                if (manager.tabFigureLibrarySave[i] == 0)
                {
                    textArray[i].text = "";
                }

                if (manager.tabFigureLibrarySave[ i] == 1)
                {
                    textArray[i].text = "Looping";
                }

                if (manager.tabFigureLibrarySave[ i] == 2)
                {
                    textArray[i].text = "Montée";
                }
                if (manager.tabFigureLibrarySave[i] == 3)
                {
                    textArray[i].text = "virage a droite";
                }
                if (manager.tabFigureLibrarySave[i] == 4)
                {
                    textArray[i].text = "virage a gauche";
                }
            }

            numberFigure = 0;
            endFigure = false;
        }

        public void restartLevel()
        {
            SceneManager.LoadScene("Scene_Figure01", LoadSceneMode.Single);
        }

        public void chooseNextLevel(string nextLevelNumber)
        {
            levelFigure = int.Parse(nextLevelNumber);
        }

        public void setTravelingVariable()
        {
            travelingLevelFigure = levelFigure;

            for (int i = 0; i < travelingFigureArray.GetLength(1); i++)
            {
                // travelingFigureArray[travelingLevelFigure, i] = numberFigureArray[travelingLevelFigure, i];
                travelingFigureArray[travelingLevelFigure, i] = localArrayFigureSuite[i] ;
                //Debug.Log(travelingFigureArray[travelingLevelFigure, i]);
            }
        }

        public void GetToServerFigures()
        {
           manager.ReceiveInfoToServeur2();
        }

        public void ChoixEvent(int numeroEventButton)
        {
            manager.INumeroEvent = numeroEventButton;
            // manager.INumeroEvent = 
           // Debug.Log("test: " + manager.INumeroEvent); 
        }

        public void ClicOnEventButton () //fait apparaitre les bouton receive
        {
            if (editorModBeActive == true)
            {
                ModifyButton.SetActive(true);
                DeleteButton.SetActive(true);

            }
            else
            {
                PanelEventChoise.SetActive(false);
                PanelEventInfosPlayer.SetActive(true);
                InfosEventText.text = manager.IEventInfos;
               // manager.test
               // manager.ReceiveInfoToServeur2;
            }


            //Debug.Log("apparition des boutons receive et modify"); 
           
        }

        public void clicOnModifyEvent()

        {
            eventInfosInputfield.text = manager.IEventInfos;
            EventNameInputfield.text = manager.IEventName;
            EventUrlInputfield.text = manager.IEventUrl;
            numberFigure = 0;
                 for (int i = 0;  i < 5; i++)
                {   
                    manager.tabFigureLibrarySave[i] = manager.IDLocalFigure[i];
                    textArray[i].color = Color.yellow;
                //  Debug.Log("tabFigureLibrarySave " + i + " =  " + manager.tabFigureLibrarySave[i]);
            }
                
            DestroyButtonsEvent();
            PanelEventChoise.SetActive(false);
            PanelEventEditor.SetActive(true);
            SaveNewEventButton.SetActive(false);
            SaveModifyButton.SetActive(true);
            boutonEventGenerated = false;
            affichageFigures();
        }

        public void clicOnDeleteEvent()
        {
            manager.DeleteInfoToServer();
            DestroyButtonsEvent();
            GenerationBoutons();
            /*PanelEventChoise.SetActive(false);
            PanelEventEditor.SetActive(true);
            SaveNewEventButton.SetActive(false);
            SaveModifyButton.SetActive(true);
            boutonEventGenerated = false; */
            ModifyButton.SetActive(false);
            DeleteButton.SetActive(false);


        }

        public void clicOnCreateEvent()
        {
           // EventNameInputfield.text = "";
            manager.GetNombreEvent();
            //a voir si c est indispensable
            //GetLastEventId();
            manager.NewEventGetId();
            //manager.CheckButtonEvent(LocalNombreBouton);
            numberFigure = 0;
            for (int i = 0; i < 5; i++)
            {
                manager.IDLocalFigure[i] = 0;
            }
            DestroyButtonsEvent();
            PanelEventChoise.SetActive(false);
            PanelEventEditor.SetActive(true);
            SaveNewEventButton.SetActive(true);
            SaveModifyButton.SetActive(false);
            boutonEventGenerated = false;
            affichageFigures();

        }

        public void clicOnBackButton(int BackButtonID)
        {
            if (BackButtonID == 1)
            {
                Debug.Log("retour au menu");
            }
                if (BackButtonID == 2)
            {
                if (PanelEventEditor.activeSelf)
                {
                    eventInfosInputfield.text = null;
                    eventInfosInputfield.text = "";
                    EventNameInputfield.text = "";
                    EventUrlInputfield.text = "";
                    PanelEventChoise.SetActive(true);
                    PanelEventEditor.SetActive(false);
                    ModifyButton.SetActive(false);
                    DeleteButton.SetActive(false);
                    PanelMessageAlert.SetActive(false);


                    GenerationBoutons();
                }
                else
                {
                    Debug.Log("retour au menu");
                }
            }
            if(BackButtonID == 3)
            {
                PanelEventChoise.SetActive(true);
                PanelEventInfosPlayer.SetActive(false);
            }
        }
        public void affichageFigures()
        {
            for (int i = 0; i < 5; i++)
            {
                // Debug.Log(manager.IDLocalFigure[i]);
                
                if (manager.IDLocalFigure[i] == 0)
                {
                    textArray[i].text = ""; 
                }

                if (manager.IDLocalFigure[i] == 1)
                {
                    textArray[i].text = "Looping 1";
                }

                if (manager.IDLocalFigure[i] == 2)
                {
                    textArray[i].text = "Montée 2";
                }

                if (manager.IDLocalFigure[i] == 3)
                {
                    textArray[i].text = "Virage droite 3";
                }

                if (manager.IDLocalFigure[i] == 4)
                {
                    textArray[i].text = "Virage gauche 4";
                }
            }
        }

        public void GetLastEventId ()
        {
            if (manager.test == 0)
            {
                manager.NewEventGetId();
                //manager.CheckButtonEvent(LocalNombreBouton);
                ModificationEvent = false;
            }

            if (manager.test> 0)
            {
                manager.INumeroEvent = manager.test;
                ModificationEvent = true;
            }
        }

        public void GenerationBoutons()
        {
            DestroyButtonsEvent();
            if ( boutonEventGenerated == false) 
            {
                
                 LocalNombreBouton = 0;
                 bool pause = false;
                 manager.GetNombreEvent();
              /*  if (dt.ToString("yyyy-MM-dd HH:mm:ss") > IDateDebutEvent)//si la date de début est passée
                { */
                    for (int i = 0; i < manager.nombreEvent; i++ )
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
        public void StandBy()
        {
            StartCoroutine(alreadyPlaceToFalse());
        }

        IEnumerator alreadyPlaceToFalse()
        {
            Debug.Log("pause");
            yield return new WaitForSeconds(1.0f);
        }

        public void TaskOnClick()
        {
            //Output this to console when the Button is clicked
            Debug.Log("You have clicked the button!");
        }

        public void DestroyButtonsEvent()
        {
            int childs = EventListPanel.transform.childCount;
          //  Debug.Log("childCount: " + childs);
            for (int i = childs - 1; i > 0; i--)
            {
                Destroy(EventListPanel.transform.GetChild(i).gameObject);
            }
            boutonEventGenerated = false;

        }

        public void ModeEdit(bool EditorModeBool)
        {

            editorModBeActive = EditorModeBool;

            if (editorModBeActive == true)
            {
                EditorModeButton.SetActive(false);
                // PanelEventEditor.SetActive(false);
                //PanelEventChoise.SetActive(true);
                // editorModBeActive = EditorModeBool;
            }
            else
            {
                EditorModeButton.SetActive(true);
                //PanelEventEditor.SetActive(false);
                // PanelEventChoise.SetActive(true);
                // editorModBeActive = EditorModeBool;
            }
        }

       // public void BackButton
        public void MoreInfosEventButton()
        {
            Application.OpenURL(manager.IEventUrl);// lance le lien vers la page rentré
        }

       /* IEnumerator Start()
        {
            WWW www = new WWW("https://static.wixstatic.com/media/3808b8_033ae6d0cced405a957339a3a3834a0a~mv2.jpg/v1/fill/w_281,h_197,al_c,lg_1,q_80/3808b8_033ae6d0cced405a957339a3a3834a0a~mv2.webp");
            yield return www;
            img.texture = www.texture;
            img.SetNativeSize();
        } */
       

        public void SendAnEmail ()
        {
            /* mail.From = new MailAddress("loiclafferriere@gmail.com");
             mail.To.Add("cluzel.k@gmail.com");
             SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
             smtpServer.Port = 587;//GIVE CORRECT PORT HERE
             mail.Subject = "recupération de mot de passe";
             mail.Body = "voici le code que vous avez demandez";
             smtpServer.Credentials = new System.Net.NetworkCredential("loiclafferriere@gmail.com", "") as ICredentialsByHost;
             smtpServer.EnableSsl = true;
             ServicePointManager.ServerCertificateValidationCallback =
             delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
             { return true; };
             smtpServer.Send(mail);
             //smtpServer.SendAsync(mail)
             Debug.Log("success");*/


            //var mail = new MailMessage();
          /*  mail.From = new MailAddress("loiclafferriere@gmail.com");
            mail.To.Add("cluzel.k@gmail.com");
            mail.Subject = "Test Mail Subject";
            mail.Body = "This is for testing SMTP mail from GMAIL";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("loiclafferriere@gmail.com", "")as ICredentialsByHost;
            smtpClient.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = (x, y, z, w) => true;
            smtpClient.Send(mail);   */

        }

        public void EnterDateBeginTrainingEvent()
        {
           
            // dtBegin = DateBeginTrainingInputField.text;
            dt = DateTime.Now;

            //int.Parse(dt);
            string test = dt.ToString("yyyyMMddHHmmss");
            double test2 = Double.Parse(test);
            Debug.Log("test2  " + test2);
            manager.IDateDebutEventGeneral = DateBeginTrainingInputField.text;
            double test3 = Double.Parse(DateBeginTrainingInputField.text);
            Debug.Log("test3  " + test3);

            //int.Parse(manager.IDateDebutEvent);


            if (test2 > test3)//si la date de début est passée
            {
               // Debug.Log("ça marche:  " + int.Parse(manager.IDateDebutEvent));
            }


            //manager.IDateDebutEvent = DateBeginTrainingInputField.text; 
        }
    }


}
