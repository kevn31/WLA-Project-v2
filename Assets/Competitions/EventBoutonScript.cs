using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WeLoveAero;

namespace WeLoveAero
{
    public class EventBoutonScript : MonoBehaviour
    {

        EVENTCREATOR_Manager manager1;

        // private SceneEditorManager scriptEditorManager;

        public string TypeEvent;
        public Text TypeEventText;
        public int NumberOfEvent;

        public string TitreEvent;
        public Text TitreEventText;
        public Text DateFinText;

        public GameObject test;

        public bool InfosButtonBeGet;

        public Button buttonEvent;

        //public GameObject ChildGameObject2 = ParentGameObject.transform.GetChild(1).gameObject;

        // Use this for initialization
        void Start()
        {
            InfosButtonBeGet = false;
            // NumberOfEvent = 0;

            // scriptEditorManager = test.GetComponent<SceneEditorManager>();
            //  test = this.GameObject;  
            //test = Find.GameObject<GameObject>;
             
            manager1 = GameObject.Find("MySqlManager").GetComponent<EVENTCREATOR_Manager>();
            GetInfoButtonToServer();
            //GetInfoButtonToServer();
            // UpdateButton();
            // Debug.Log("manager marche : " + manager1.InumeroEvent);

            // GameObject.onClick.AddListener(TaskOnClick);
            //NumberOfEvent = manager.globalNombreBouton;
            //NumberOfEvent = SceneEditorManager.PublicNombreBouton;

            // Debug.Log("EventButton: " + SceneEditorManager.PublicNombreBouton);
            // TitreEventText.text = "Event: " + manager.globalNombreBouton;
            //TitreEventText.text = manager.nombreEvent;

            //
            //test.name = "Whatever";
        }

        // Update is called once per frame
        void Update()
        {
           
            if (NumberOfEvent == 1)
            {
                //buttonEvent = GetComponent<Button>();
                DateFinText.text = manager1.ITempsRestantEvent1;
                //buttonEvent.interactable = false;
                // Debug.Log("E11111 " );
                if (manager1.ITempsRestantEvent1 == "Closed")
                {
                   buttonEvent.interactable = false;
                }
                else
                {
                    buttonEvent.interactable = true;
                }


            }
            if (NumberOfEvent == 2)
            {
               // buttonEvent = GetComponent<Button>();
                DateFinText.text = manager1.ITempsRestantEvent2;
                //buttonEvent.interactable = false;
                // Debug.Log("E2222 ");
                if (manager1.ITempsRestantEvent2 == "Closed")
                {
                   buttonEvent.interactable = false;
                }
                else
                {
                    buttonEvent.interactable = true;
                }
            }

            if (NumberOfEvent == 3)
            {
                // buttonEvent = GetComponent<Button>();
                DateFinText.text = manager1.ITempsRestantEvent3;
                //buttonEvent.interactable = false;
                // Debug.Log("E2222 ");
                if (manager1.ITempsRestantEvent3 == "Closed")
                {
                    buttonEvent.interactable = false;
                }
                else
                {
                    buttonEvent.interactable = true;
                }
            }
            if (NumberOfEvent == 4)
            {
                // buttonEvent = GetComponent<Button>();
                DateFinText.text = manager1.ITempsRestantEvent4;
                //buttonEvent.interactable = false;
                // Debug.Log("E2222 ");
                if (manager1.ITempsRestantEvent4 == "Closed")
                {
                    buttonEvent.interactable = false;
                }
                else
                {
                    buttonEvent.interactable = true;
                }
            }
            if (NumberOfEvent == 5)
            {
                // buttonEvent = GetComponent<Button>();
                DateFinText.text = manager1.ITempsRestantEvent5;
                //buttonEvent.interactable = false;
                // Debug.Log("E2222 ");
                if (manager1.ITempsRestantEvent5 == "Closed")
                {
                    buttonEvent.interactable = false;
                }
                else
                {
                    buttonEvent.interactable = true;
                }
            }
        }

        void TaskOnClick()
        {
            //Output this to console when the Button is clicked
        }

        public void tsointsoin(int valeurDemande)
        {
            NumberOfEvent = valeurDemande;
            // Debug.Log("tsointsoin à encore frappé !! : " + NumberOfEvent);
            // manager.ActualEvent = valeurDemande;
        }

        public void clicOnEvent()
        {
            // scriptEditorManager.ChoixEvent(NumberOfEvent);
            manager1.test = NumberOfEvent;
            //NumberOfEvent = manager1.numEventTest;

            //TitreEventText.text = manager1.IEventName;

            //TitreEventText.text ="0000000000";
        }

        public void GetInfoButtonToServer ()
        {
            StartCoroutine(findInfosButton());
           /* while (NumberOfEvent != 0 && InfosButtonBeGet == false)
            {
                Debug.Log("passe ici " + NumberOfEvent);
                manager1.ReceiveInfoButtonToServeur(NumberOfEvent);
                UpdateButton();
                InfosButtonBeGet = true;
            }*/

            //coroutine updateButton
            
        }

        IEnumerator findInfosButton()
        {
            yield return new WaitForSeconds(1.5f);
            while (NumberOfEvent != 0 && InfosButtonBeGet == false)
            {
                manager1.ReceiveInfoButtonToServeur(NumberOfEvent);
                UpdateButton();
                InfosButtonBeGet = true;
            }
         
        }

        public void UpdateButton()
        {
         
            //TitreEventText.text = "0000000000";
            if (NumberOfEvent == 1)
            {
                TitreEventText.text = manager1.IEventName;
                DateFinText.text = manager1.ITempsRestantEvent1;
            }
            if (NumberOfEvent == 2)
            {
                TitreEventText.text = manager1.IEventName;
                DateFinText.text = manager1.ITempsRestantEvent2;
            }
            if (NumberOfEvent == 3)
            {
                TitreEventText.text = manager1.IEventName;
                DateFinText.text = manager1.ITempsRestantEvent3;
            }
            if (NumberOfEvent == 4)
            {
                TitreEventText.text = manager1.IEventName;
                DateFinText.text = manager1.ITempsRestantEvent4;
            }
            if (NumberOfEvent == 5)
            {
                TitreEventText.text = manager1.IEventName;
                DateFinText.text = manager1.ITempsRestantEvent5;
            }


            // DateFinText.text = manager1.IDateDebutEvent;// changer pour date de fin
        }
    }
}
