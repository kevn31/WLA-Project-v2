using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeColor : MonoBehaviour {

    public GameObject barreRouge;
    public GameObject barreOrange;
    public GameObject allButton;
    public Vector3 actualPosition;
    private float actualPositionY;
    private bool movment;
    private bool movmentback;

    // Use this for initialization
    void Start () {
        actualPosition = allButton.transform.position;
        actualPositionY = actualPosition.y;
        movment = false;
        movmentback = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (movment)
        {
            if(actualPositionY < 45 && !movmentback)
            {
                allButton.transform.rotation = Quaternion.Euler(1, actualPositionY, 1);
                actualPositionY += Time.deltaTime * 50;
            }

            else if(actualPositionY > 0)
            {
                movmentback = true;
                allButton.transform.rotation = Quaternion.Euler(1, actualPositionY, 1);
                actualPositionY -= Time.deltaTime * 75;
            }
            else
            {
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
            
            
        }
    }

    public void ChangeTheColor()
    {
        movment = true;
        barreRouge.SetActive(false);
        barreOrange.SetActive(true);
        

        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
