using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using WeLoveAero;

namespace WeLoveAero
{  
    public class EVENTCREATOR_Manager : MonoBehaviour
    {
        SceneEditorManager sceneEditorScript;
        EventBoutonScript eventButtonScript;
        [Header("DATABASE")]
        public string host;
        public string database, username, password;
        [Header("EVENT")]
        MySqlConnection con;
        public int voila;
        public string IEventName;
        public string IEventUrl;
        public string IEventInfos;
        public string IFigureName;
        public int IFigureID;
        //public int test;
        public int INumeroFigure;
        public int INumeroEvent;
        public int test; // numero de l event select
        public int numEventTest;

        public DateTime dt;
        private static int value = 50;
        private static int firstValue = 50;

        public int[] IDFigureLocal = new int[value];
        public int[] IDLocalFigure = new int[value];
        public int[] INumeroFigureId = new int[value];
        public string[] figureNameTab = new string[50];

        public string[] figureNameTab2 = new string[500];   //pour test, remplace figure tab name


        public string TamponName;
        private int numeroActualDataFigureRead;
        private int numerofigureLocal;
        private int nombreFigures;
        public int countFigureTotal;
        public int ActualEvent;
        public int nombreEvent;
        [Header("TEST")]
        public int IValue;
        public int globalNombreBouton;

        public int eventChoisi;
        [Header("NOUVELLES VAR")]

        public int numberOrderFigure;  //ordre d arrivee des figures à l'interieur du show (pas indispensable)
                                       // public int[] tabNumberOrderFigure = new int[value]; //tableau qui va stocker l ordre des figures 
        public int[] numberOrderFigureTab = new int[50];  //tableau qui va stocker l'ordre des figures
        public int figureIdentity;//numéro qui défini le préfab  appeler
        public int[] tabFigureLibrary = new int[10]; //tableau qui ne change pas et qui stock simplement identités de chaque figure

        public int[] tabFigureLibrarySave = new int[500];
        public int IdGlobalServerLine; // numéro de la ligne dans le server
                                       //  public int[] numberFiguresOfEvent= new int[value];
        public int ActuelFigureSave; // n a d interet que dansleventcreator elle montre le numero de la figure actuelle par ordre d appel

        public int[] numberFigureOfEvent = new int[50];  //nombre de figures dans chaque event


        public string IDateDebutEventGeneral;
        public string IDateFinEventGeneral;

        public string IDateDebutEvent1;
        public string IDateFinEvent1;
        public string ITempsRestantEvent1;
        public string IDateDebutEvent2;
        public string IDateFinEvent2;
        public string ITempsRestantEvent2;
        public string IDateDebutEvent3;
        public string IDateFinEvent3;
        public string ITempsRestantEvent3;
        public string IDateDebutEvent4;
        public string IDateFinEvent4;
        public string ITempsRestantEvent4;
        public string IDateDebutEvent5;
        public string IDateFinEvent5;
        public string ITempsRestantEvent5;

        //indispensable
        public DateTime dateFinEventPanelInfo;
        public DateTime dateDebutEvent1;
        public DateTime dateDebutEvent2;
        public DateTime dateDebutEvent3;
        public DateTime dateDebutEvent4;
        public DateTime dateDebutEvent5;

        //indispensable
        public DateTime dateFinEvent1;
        public DateTime dateFinEvent2;
        public DateTime dateFinEvent3;
        public DateTime dateFinEvent4;
        public DateTime dateFinEvent5;
        public DateTime dtActual;

        public TimeSpan TempsRestantEvent1;
        public TimeSpan TempsRestantEvent2;
        public TimeSpan TempsRestantEvent3;
        public TimeSpan TempsRestantEvent4;
        public TimeSpan TempsRestantEvent5;

        public int IEventSlotChoice;
        public string IPricesInfos;


        // public GameObject boutonTestInfos;
        // Use this for initialization
        private void Start()
        {
          //  IDateDebutEvent = "2018/10/10 12:00:00";
            test = 0;
            numEventTest = 10;
           // Debug.Log(tabFigureLibrary.Length);
            initNameFigures(); 
            connect_BDD();
        }

        public void Chrono()
        {
            dtActual = DateTime.Now;

            //////////////EVENT1/////////////////
            //TempsRestantEvent1 = dateFinEvent1 - dtActual;
            //ITempsRestantEvent1 = TempsRestantEvent1.ToString();
            Debug.Log("ITempsRestantEvent1: " + ITempsRestantEvent1);
            //////////////EVENT2/////////////////
            //TempsRestantEvent2 = dateFinEvent2 - dtActual;
            //ITempsRestantEvent2 = TempsRestantEvent2.ToString();
            Debug.Log("ITempsRestantEvent2: " + dateFinEvent1); 
            //////////////EVENT3/////////////////
            TempsRestantEvent3 = dateFinEvent1 - dtActual;
            ITempsRestantEvent3 = TempsRestantEvent3.ToString();
            //////////////EVENT4/////////////////
            TempsRestantEvent4 = dateFinEvent1 - dtActual;
            ITempsRestantEvent4 = TempsRestantEvent4.ToString();
            //////////////EVENT5/////////////////
            TempsRestantEvent5 = dateFinEvent1 - dtActual;
            ITempsRestantEvent5 = TempsRestantEvent5.ToString();
        }

        void Update()
        {
            if (TempsRestantEvent1 != null  && dateFinEvent1 != null /*&& nombreEvent >=1*/)
            {
                dtActual = DateTime.Now;
                if (dtActual < dateDebutEvent1)//avant debut
                {
                    ITempsRestantEvent1 = " Opening on  " + IDateDebutEvent1;//faire aussi en sorte qu il ne soit pas interactible
                }
                if (dtActual > dateDebutEvent1 && dtActual < dateFinEvent1)//pendant
                {
                    TempsRestantEvent1 = dateFinEvent1 - dtActual;
                    ITempsRestantEvent1 = TempsRestantEvent1.Days.ToString() + " Days  and " + TempsRestantEvent1.Hours.ToString() + " : " + TempsRestantEvent1.Minutes.ToString() + " : " + TempsRestantEvent1.Seconds.ToString();
                }
                if (dtActual > dateFinEvent1)
                {
                    ITempsRestantEvent1 = "Closed";
                    //eventButtonScript.buttonEvent.interactable = false;

                }
                 //Debug.Log("Temps restant : " + TempsRestantEvent1);

            }
            if (TempsRestantEvent1 != null && dateFinEvent2 != null /*&& nombreEvent >= 2*/)
            {
                dtActual = DateTime.Now;
                if (dtActual < dateDebutEvent2)//avant debut
                {
                    ITempsRestantEvent2 = " Opening on  " + IDateDebutEvent2;//faire aussi en sorte qu il ne soit pas interactible
                }
                if (dtActual > dateDebutEvent2 && dtActual < dateFinEvent2)//pendant
                {
                    TempsRestantEvent2 = dateFinEvent2 - dtActual;
                    ITempsRestantEvent2 = TempsRestantEvent2.Days.ToString() + " Days  and " + TempsRestantEvent2.Hours.ToString() + " : " + TempsRestantEvent2.Minutes.ToString() + " : " + TempsRestantEvent2.Seconds.ToString();
                }
                if (dtActual > dateFinEvent2)
                {
                   ITempsRestantEvent2 = "Closed";
                    //eventButtonScript.buttonEvent.interactable = false;
                }
                //Debug.Log("Temps restant : " + TempsRestantEvent1);

            }
            if (TempsRestantEvent1 != null && dateFinEvent3 != null /*&& nombreEvent >= 2*/)
            {

                dtActual = DateTime.Now;
                if(dtActual < dateDebutEvent3)//avant debut
                {
                    ITempsRestantEvent3 = " Opening on  " + IDateDebutEvent3;//faire aussi en sorte qu il ne soit pas interactible
                }

                if(dtActual > dateDebutEvent3 && dtActual < dateFinEvent3)//pendant
                {
                    TempsRestantEvent3 = dateFinEvent3 - dtActual;
                    ITempsRestantEvent3 = TempsRestantEvent3.Days.ToString() + " Days  and " + TempsRestantEvent3.Hours.ToString() + " : " + TempsRestantEvent3.Minutes.ToString() + " : " + TempsRestantEvent3.Seconds.ToString();

                }
                if (dtActual > dateFinEvent3)//apres la fin
                {
                    ITempsRestantEvent3 = "Closed";
                    //eventButtonScript.buttonEvent.interactable = false;
                }
                //Debug.Log("Temps restant : " + TempsRestantEvent1);

            }
            if (TempsRestantEvent1 != null && dateFinEvent4 != null /*&& nombreEvent >= 2*/)
            {
                dtActual = DateTime.Now;
                if (dtActual < dateDebutEvent4)//avant debut
                {
                    ITempsRestantEvent4 = " Opening on  " + IDateDebutEvent4;//faire aussi en sorte qu il ne soit pas interactible
                }
                if (dtActual > dateDebutEvent4 && dtActual < dateFinEvent4)//pendant
                {
                    TempsRestantEvent4 = dateFinEvent4 - dtActual;
                    ITempsRestantEvent4 = TempsRestantEvent4.Days.ToString() + " Days  and " + TempsRestantEvent4.Hours.ToString() + " : " + TempsRestantEvent4.Minutes.ToString() + " : " + TempsRestantEvent4.Seconds.ToString();
                }

                if (dtActual > dateFinEvent4)
                {
                    ITempsRestantEvent4 = "Closed";
                    //eventButtonScript.buttonEvent.interactable = false;
                }
                //Debug.Log("Temps restant : " + TempsRestantEvent1);

            }
            if (TempsRestantEvent1 != null && dateFinEvent5 != null /*&& nombreEvent >= 2*/)
            {

                dtActual = DateTime.Now;
                if (dtActual < dateDebutEvent5)//avant debut
                {
                    ITempsRestantEvent5 = " Opening on  " + IDateDebutEvent5;//faire aussi en sorte qu il ne soit pas interactible
                }
                if (dtActual > dateDebutEvent5 && dtActual < dateFinEvent5)//pendant
                {
                    TempsRestantEvent5 = dateFinEvent5 - dtActual;
                    ITempsRestantEvent5 = TempsRestantEvent5.Days.ToString() + " Days  and " + TempsRestantEvent5.Hours.ToString() + " : " + TempsRestantEvent5.Minutes.ToString() + " : " + TempsRestantEvent5.Seconds.ToString();
                }
               
                if (dtActual > dateFinEvent5)
                {
                    ITempsRestantEvent5 = "Closed";
                    //eventButtonScript.buttonEvent.interactable = false;
                }
                //Debug.Log("Temps restant : " + TempsRestantEvent1);

            }
        }

        public void initNameFigures()
        {
            figureNameTab2[0] = "end";
            figureNameTab2[1] = "loop";
            figureNameTab2[2] = "montée";
            figureNameTab2[3] = "virage à droite";
            figureNameTab2[4] = "virage à gauche";
        }


        void connect_BDD()
        {
            string cmd = "SERVER=" + host + ";" + "database=" + database + ";User ID=" + username + ";Password=" + password + ";Pooling=true;Charset=utf8";

            try
            {
                con = new MySqlConnection(cmd);
                // con.Open();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }

        public void CreateFigureCreation()   // doit etre appeler autant de fois qu il y a de figures a sauvegarder
        {
            dt = DateTime.Now; //donne les infos temporels
            con.Open();
            string cmd = "INSERT INTO `eventinfos`(`id`, `idevent`, `eventname`, `figurenumber`,`figurename`,`date`,`identifiantfigure`,`eventdoc`,`eventurl`,`datedebut`,`dateend`,`prices`,`eventchoice`) VALUES(NULL, '" + INumeroEvent + "', '" + IEventName + "', '" + ActuelFigureSave + "', '" + figureNameTab2[tabFigureLibrarySave[ActuelFigureSave]] + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "','" + tabFigureLibrarySave[ActuelFigureSave] + "','" + IEventInfos + "','" + IEventUrl + "','" + IDateDebutEventGeneral + "','" + IDateFinEventGeneral + "' ,'" + IPricesInfos + "' ,'" + IEventSlotChoice + "'  )";//,'" + IEventInfos + "' 
            MySqlCommand CmdSql = new MySqlCommand(cmd, con);

            try
            {
                CmdSql.ExecuteReader();
                Debug.Log("sauvegarde sur le serveur");

            }
            catch (Exception Ex)
            {
                Debug.Log(Ex.ToString());
            }
            con.Close();
        }

        //public 

        public void ModificationFiguresOfEvent()   // doit être appeler autant de fois qu il y a de figures à sauvegarder
        {
            dt = DateTime.Now; //donne les infos temporels
            con.Open();
            string cmd = "UPDATE `eventinfos` SET `identifiantfigure`=" + tabFigureLibrarySave[ActuelFigureSave] + " WHERE `idevent`= '" + INumeroEvent + "'";
            MySqlCommand CmdSql = new MySqlCommand(cmd, con);

            try
            {
                CmdSql.ExecuteReader();
                Debug.Log("sauvegarde sur le serveur");

            }
            catch (Exception Ex)
            {
                Debug.Log(Ex.ToString());
            }
            con.Close();
        }


        public void UpdateFigureCreation()
        {
            con.Open();

            string cmd = "UPDATE `eventinfos` SET `idevent`=" + INumeroEvent + "'";
            MySqlCommand CmdSql = new MySqlCommand(cmd, con);

            try
            {
                CmdSql.ExecuteReader();
                Debug.Log("nom de figure" + IFigureName);

            }
            catch (Exception Ex)
            {
                Debug.Log(Ex.ToString());
            }
            con.Close();
        }

        public void ReceiveNameButtonToServer()    //quand j arrive sur la page des events, les infos qui seront presentes sur les button chargent
        {

            try
            {
                numeroActualDataFigureRead = 1; // a changer  s incremente de 1 a chaque tour
                con.Open();

                MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `eventinfos` WHERE `idevent`='" + numeroActualDataFigureRead + "'", con);
                Debug.Log("numero de l id :");

                MySqlDataReader MyReader = CmdSql.ExecuteReader();//erreur a partir d ici

                while (MyReader.Read())
                {
                    IEventName = MyReader["eventname"].ToString();

                    Debug.Log("Data: " + IEventName);
                }

                MyReader.Close();

            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); } 
            con.Close();
        }

        public void ReceiveNameButtonToServer2()    //quand j arrive sur la page des events, les infos qui seront presentes sur les button chargent
        {

            try
            {
   
                numeroActualDataFigureRead = 0; // a changer  s incremente de 1 a chaque tour
                con.Open();

                MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `eventinfos` WHERE `idevent`='" + INumeroEvent + "'", con);
                MySqlDataReader MyReader = CmdSql.ExecuteReader();//erreur a partir d ici
                while (ActuelFigureSave > 0)
                {
                    while (MyReader.Read())
                    {
                        ActuelFigureSave = (int)MyReader["figurenumber"];
                        Debug.Log("Data: " + ActuelFigureSave);
                    }

                    MyReader.Close();
                }
            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }

            con.Close();
        }

        public void ReceiveInfoToServeur2() // quand je clic sur un bouton les infos lies a l enchainement des figures de l event clicke chargent dans un tableau
        {
            Debug.Log("test: " + test);
            ActuelFigureSave = 1;
            try
            {
                numeroActualDataFigureRead = 0; // a changer  s incremente de 1 a chaque tour
                con.Open(); 
                MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `eventinfos` WHERE `idevent`='" +/* ActualEventINumeroEvent*/ test + "'ORDER BY `figurenumber` ASC ", con);
               // Debug.Log("numero de l id : "+ test);

                MySqlDataReader MyReader = CmdSql.ExecuteReader();//erreur a partir d ici

                while (MyReader.Read())
                {
                    IEventName = (string)MyReader["eventname"];
                    IEventInfos = (string)MyReader["eventdoc"];
                    IEventUrl = (string)MyReader["eventurl"];
                    IFigureID = (int)MyReader["identifiantfigure"];
                   // IDateDebutEvent = (DateTime)MyReader["datedebut"];
                    dateFinEventPanelInfo = (DateTime)MyReader["dateend"];
                    Debug.Log("dateFinEventPanelInfo: " + dateFinEventPanelInfo);
                    // dateFinEvent2 = (DateTime)MyReader["datedebut"];

                    IDateDebutEventGeneral  = dateFinEventPanelInfo.ToString("yyyy-MM-dd    HH:mm");
                    //.ToString("yyyy-MM-dd-HH-mm")

                    ActuelFigureSave = (int)MyReader["figurenumber"];     //doit passer a 0 pour stoper la boucle
                    IDLocalFigure[numeroActualDataFigureRead] = IFigureID;

                  

                   // Debug.Log("identifiantfigure: " + IDLocalFigure[ActuelFigureSave]);
                   numeroActualDataFigureRead++;
                } 
                MyReader.Close();  
            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }
            con.Close();

            //Debug.Log("ieventinfos: " + IEventInfos);
        }

        public void ReceiveInfoButtonToServeur(int numEvent) // quand je clic sur un bouton les infos lies a l enchainement des figures de l event clicke chargent dans un tableau
        {
          
            try
            {
                con.Open();
                MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `eventinfos` WHERE `idevent`='" +/* ActualEventINumeroEvent*/ numEvent + "'ORDER BY `figurenumber` ASC ", con);
                // Debug.Log("numero de l id : "+ test);

                MySqlDataReader MyReader = CmdSql.ExecuteReader();//erreur a partir d ici

                while (MyReader.Read())
                {
                    IEventName = (string)MyReader["eventname"];

                    //IEventInfos = (string)MyReader["eventdoc"];
                    //IEventUrl = (string)MyReader["eventurl"];
                    //IFigureID = (int)MyReader["identifiantfigure"];
                     //IDateDebutEvent = (DateTime)MyReader["datedebut"];
                    
                    if(numEvent == 1)
                    {
                        dateDebutEvent1 = (DateTime)MyReader["datedebut"];
                        dateFinEvent1 = (DateTime)MyReader["dateend"];
                        IDateDebutEvent1 = dateDebutEvent1.ToString("yyyy-MM-dd    HH:mm");
                        IDateFinEvent1 = dateFinEvent1.ToString("yyyy-MM-dd    HH:mm");
                       // Debug.Log("dateFinEvent1:  " + IDateFinEvent1);
                        //Debug.Log("dateFinEvent2:  " + IDateFinEvent2);
                    }
                    else if (numEvent == 2)
                    {
                        //IDateDebutEventGeneral = dateFinEvent2.ToString("yyyy-MM-dd    HH:mm");
                        dateDebutEvent2 = (DateTime)MyReader["datedebut"];
                        dateFinEvent2 = (DateTime)MyReader["dateend"];
                        //dateFinEvent2 = dateFinEvent1;
                        IDateDebutEvent2 = dateDebutEvent5.ToString("yyyy-MM-dd    HH:mm");
                        IDateFinEvent2 = dateFinEvent2.ToString("yyyy-MM-dd    HH:mm");
                       // Debug.Log("dateFinEvent2:  " + IDateFinEvent2);
                       
                    }
                    else if (numEvent == 3)
                    {
                        //IDateDebutEventGeneral = dateFinEvent2.ToString("yyyy-MM-dd    HH:mm");
                        dateDebutEvent3 = (DateTime)MyReader["datedebut"];
                        dateFinEvent3 = (DateTime)MyReader["dateend"];
                        //dateFinEvent2 = dateFinEvent1;
                        IDateDebutEvent3 = dateDebutEvent3.ToString("yyyy-MM-dd    HH:mm");
                        IDateFinEvent3 = dateFinEvent3.ToString("yyyy-MM-dd    HH:mm");
                        // Debug.Log("dateFinEvent2:  " + IDateFinEvent2);

                    }
                    else if (numEvent == 4)
                    {
                        //IDateDebutEventGeneral = dateFinEvent2.ToString("yyyy-MM-dd    HH:mm");
                        dateDebutEvent4 = (DateTime)MyReader["datedebut"];
                        dateFinEvent4 = (DateTime)MyReader["dateend"];
                        //dateFinEvent2 = dateFinEvent1;
                        IDateDebutEvent4 = dateDebutEvent4.ToString("yyyy-MM-dd    HH:mm");
                        IDateFinEvent4 = dateFinEvent4.ToString("yyyy-MM-dd    HH:mm");
                        // Debug.Log("dateFinEvent2:  " + IDateFinEvent2);

                    }
                    else if (numEvent == 5)
                    {
                        //IDateDebutEventGeneral = dateFinEvent2.ToString("yyyy-MM-dd    HH:mm");
                        dateDebutEvent5 = (DateTime)MyReader["datedebut"];
                        dateFinEvent5 = (DateTime)MyReader["dateend"];
                        //dateFinEvent2 = dateFinEvent1;
                        IDateDebutEvent5 = dateDebutEvent5.ToString("yyyy-MM-dd    HH:mm");
                        IDateFinEvent5 = dateFinEvent5.ToString("yyyy-MM-dd    HH:mm");
                        // Debug.Log("dateFinEvent2:  " + IDateFinEvent2);

                    }

                    //.ToString("yyyy-MM-dd-HH-mm")

                    //ActuelFigureSave = (int)MyReader["figurenumber"];     //doit passer a 0 pour stoper la boucle
                    //IDLocalFigure[numeroActualDataFigureRead] = IFigureID;



                    // Debug.Log("identifiantfigure: " + IDLocalFigure[ActuelFigureSave]);
                    //numeroActualDataFigureRead++;
                }
                MyReader.Close();
            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }
            con.Close();

            //Debug.Log("ieventinfos: " + IEventInfos);
        }


        public void ModifieInfoToServer2() // quand je clic sur un bouton les infos lies a l enchainement des figures de l event clicke chargent dans un tableau
        {
            ActuelFigureSave = 1;
            try
            {
                numeroActualDataFigureRead = 0; // a changer  s incremente de 1 a chaque tour
                con.Open();
                MySqlCommand CmdSql = new MySqlCommand("DELETE  FROM `eventinfos` WHERE `idevent`='" + test + /*" AND WHERE  `figurenumber`= '" + 1  +*/ "'", con);

                Debug.Log("numero de l id : " + test);

                MySqlDataReader MyReader = CmdSql.ExecuteReader();//erreur a partir d ici
                                                                  //int test = 0;

                while (MyReader.Read())
                {
                    numeroActualDataFigureRead++;
                }
                MyReader.Close();
            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }
            con.Close();
        }


        public void DeleteInfoToServer() // quand je clic sur un bouton les infos lies a l enchainement des figures de l event clicke chargent dans un tableau
        {
            
            try
            { 
                con.Open();
                MySqlCommand CmdSql = new MySqlCommand("DELETE  FROM `eventinfos` WHERE `idevent`='" + test + /*" AND WHERE  `figurenumber`= '" + 1  +*/ "'", con);
              
                MySqlDataReader MyReader = CmdSql.ExecuteReader();//erreur a partir d ici
     
                while (MyReader.Read())
                {
          
                }
                MyReader.Close();
            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }
            con.Close();
        }

        public void NewEventGetId()
        {
            try
            {
                con.Open();

                MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `eventinfos` WHERE `idevent`>'" + 0 + "'ORDER BY idevent DESC  LIMIT 1 ", con);
                MySqlDataReader MyReader = CmdSql.ExecuteReader();
                                                              
                while (MyReader.Read())
                {
                    INumeroEvent = (int)MyReader["idevent"] + 1; //on assigne le numero d event le plus elevé de la base de donnés et on lui rajoute 1
                    test = (int)MyReader["idevent"] + 1;
                    Debug.Log("Data: " + INumeroEvent);
                }

                MyReader.Close();
            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }
            con.Close();
        }

        public void CheckButtonEvent(int localIdEvent)
        {
            try
            {
                con.Open();

                MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `eventinfos` WHERE `idevent`>'" + localIdEvent + "'", con);
                MySqlDataReader MyReader = CmdSql.ExecuteReader();

                while (MyReader.Read())
                {
                   // INumeroEvent = (int)MyReader["idevent"];
                  // INumeroEvent = (int)MyReader["idevent"] + 1; //on assigne le numero d event le plus eleve de la base de donnés et on lui rajoute 1
                   test = (int)MyReader["idevent"];
                    Debug.Log("Data: " + test);
                }

                MyReader.Close();
            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }
            con.Close();
        }

        public void GetNombreEvent()
        {
            try
            {
                con.Open();

                MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `eventinfos` WHERE `idevent`>'" + 0 + "'ORDER BY idevent DESC LIMIT 1 ", con);
                MySqlDataReader MyReader = CmdSql.ExecuteReader();
                while (MyReader.Read())
                {
                    nombreEvent = (int)MyReader["idevent"]; //on assigne le numero d event le plus eleve de la base de donnes et on lui rajoute 1 
                }

                MyReader.Close();

            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }

            con.Close(); 
        }

      

    }

}



