using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableGate : MonoBehaviour {
    

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerExit(Collider other)
    {
       // Debug.Log("ici");
        gameObject.SetActive(false);
    }
}
