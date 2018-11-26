using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnveloppeDeVol : MonoBehaviour {

    [SerializeField]
    private float minHeight, maxHeight;
    private float pastRotationRollX;

    private Vector3 pastRotationForTheRoll;

    //public GameObject outOfFlightAreaUI;

    // Use this for initialization
    void Start () {

        //outOfFlightAreaUI.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        detectionHeight();


    }

    private void detectionHeight()
    {
        // Debug.Log(gameObject.transform.position.y);
        
        if (gameObject.transform.position.y < minHeight)
        {
            //outOfFlightAreaUI.SetActive(true);

            pastRotationForTheRoll = transform.eulerAngles;
            
            if (pastRotationForTheRoll.x > 330 || pastRotationForTheRoll.x < 180)
            {
               pastRotationRollX = pastRotationForTheRoll.x - 100 * Time.deltaTime;
            }

            else
            {
                pastRotationRollX = pastRotationForTheRoll.x;
            }

            transform.rotation = Quaternion.Euler(pastRotationRollX, pastRotationForTheRoll.y, pastRotationForTheRoll.z);
        }
        
        else if(gameObject.transform.position.y > maxHeight)
        {
            //outOfFlightAreaUI.SetActive(true);
            pastRotationForTheRoll = transform.eulerAngles;

            if (pastRotationForTheRoll.x < 3 || pastRotationForTheRoll.x > 180)
            {
                pastRotationRollX = pastRotationForTheRoll.x + 75 * Time.deltaTime;
            }

            else
            {
                pastRotationRollX = pastRotationForTheRoll.x;
            }

            transform.rotation = Quaternion.Euler(pastRotationRollX, pastRotationForTheRoll.y, pastRotationForTheRoll.z);
        }

        else
        {
            //outOfFlightAreaUI.SetActive(false);
        }
    }


}
