using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStage : MonoBehaviour {

   // [SerializeField]
   // private GameObject canvas1, canvas2;
   

    // Use this for initialization
    void Start () {

       // canvas1.SetActive(false);
        //canvas2.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
   

    public void restart()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void loadNextLevel(string nextScene)
    {
        Time.timeScale = 1f;
        Debug.Log(nextScene);
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        //SceneManager.LoadScene("level2", LoadSceneMode.Single);
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("MainMenu");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
