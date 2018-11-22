using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChoixManette : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject Gameobjetchoisi;

    InfosTransition infosTransition;
    // Use this for initialization
    void Start ()
    {
        infosTransition = GameObject.Find("InfosStock").GetComponent<InfosTransition>();
        infosTransition.CheckManette();
    }

    // Update is called once per frame
    void Update ()
    {
	}

    public void Stages ()
    {
        if(infosTransition.JoypadBeConnected == true)
        {
            Gameobjetchoisi = GameObject.Find("Stagestest1");
            eventSystem.SetSelectedGameObject(Gameobjetchoisi);
        }
        else
        {
            Gameobjetchoisi = GameObject.Find("ButtonStartJoypad");
        }
    }

    public void MenuPrincipal()
    {
        if (infosTransition.JoypadBeConnected == true)
        {
            Gameobjetchoisi = GameObject.Find("ButtonStartJoypad");
            eventSystem.SetSelectedGameObject(Gameobjetchoisi);
        }
        else
        {
            Gameobjetchoisi = GameObject.Find("ButtonStartJoypad");
        }
    }

    public void Tableau()
    {
        if (infosTransition.JoypadBeConnected == true)
        {
            Gameobjetchoisi = GameObject.Find("Event Dubai");
            eventSystem.SetSelectedGameObject(Gameobjetchoisi);
        }
        else
        {
            Gameobjetchoisi = GameObject.Find("ButtonStartJoypad");
        }
    }

    public void Reglages()
    {
        if (infosTransition.JoypadBeConnected == true)
        {
            Gameobjetchoisi = GameObject.Find("Graphics");
            eventSystem.SetSelectedGameObject(Gameobjetchoisi);
        }
        else
        {
            Gameobjetchoisi = GameObject.Find("ButtonStartJoypad");
        }
    }
}
