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
    public class essai_Manager : MonoBehaviour
    {
        SceneEditorManager sceneEditorScript;
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

        public DateTime dt;
        private static int value = 50;
        private static int firstValue = 50;

        public int[] IDFigureLocal = new int[value];
        public int[] IDLocalFigure = new int[value];
        public int[] INumeroFigureId = new int[value];
        public string[] figureNameTab = new string[50];

        public string[] figureNameTab2 = new string[500];   //pour teste, remplace figure tab name


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

        public string IDateDebutEvent;

        // public GameObject boutonTestInfos;
        // Use this for initialization
        private void Start()
        {
            //  IDateDebutEvent = "2018/10/10 12:00:00";
            test = 0;
            // Debug.Log(tabFigureLibrary.Length);
            initNameFigures();
            connect_BDD();
        }

        void Update()
        {

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
            string cmd = "INSERT INTO `eventinfos`(`id`, `idevent`, `eventname`, `figurenumber`,`figurename`,`date`,`identifiantfigure`,`eventdoc`,`eventurl`,datedebut) VALUES(NULL, '" + INumeroEvent + "', '" + IEventName + "', '" + ActuelFigureSave + "', '" + figureNameTab2[tabFigureLibrarySave[ActuelFigureSave]] + "','" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "','" + tabFigureLibrarySave[ActuelFigureSave] + "','" + IEventInfos + "','" + IEventUrl + "','" + IDateDebutEvent + "' )";//,'" + IEventInfos + "' 
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
                    ActuelFigureSave = (int)MyReader["figurenumber"];     //doit passer a 0 pour stoper la boucle
                    IDLocalFigure[numeroActualDataFigureRead] = IFigureID;

                    // Debug.Log("identifiantfigure: " + IDLocalFigure[ActuelFigureSave]);
                    numeroActualDataFigureRead++;
                }
                MyReader.Close();
            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }
            con.Close();
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
                    Debug.Log("nombre d event: " + nombreEvent);
                }

                MyReader.Close();

            }
            catch (Exception Ex) { Debug.Log(Ex.ToString()); }

            con.Close();
        }

    }

}



