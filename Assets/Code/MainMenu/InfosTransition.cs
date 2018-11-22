using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfosTransition : MonoBehaviour
{
    public bool JoypadBeConnected;

    public string EventState;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public SaveStateOfEventOpen()
    {
        EventState
    }*/

    public void CheckManette()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            JoypadBeConnected = true;
        }
       else
        {
            JoypadBeConnected = false;
        }
    }

}
