using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FigureChoice : MonoBehaviour {
    /// <summary>
    /// Update les tableaux pour stocker les enchainements de figures, 20 events max de 5 figures Max
    /// Index Figure : 
    /// 0 = End
    /// 1 = Loop
    /// 2 = montée
    /// </summary>

    private static int numberStage = 20;
    private static int numberFigureTotal = 5;

    private static int[,] numberFigureArray = new int[numberStage, numberFigureTotal];
    public int[,] travelingFigureArray = new int[numberStage, numberFigureTotal];

   // public static int[,] test = new int[numberStage, numberFigureTotal];

    private int curentNumberLevel = 0;
    private int numberFigure;

    private static int levelFigure = 0;
    public int travelingLevelFigure = 0;

    private bool endFigure = false;


    public Text[] textArray;

    private string carre = "square";
    private string nameFigure;
    private Text holly;

    // Use this for initialization
    void Start () {
        
        if (gameObject.name != "MANAGER")
        {
            //Debug.Log("je suis ici");
            numberFigure = 0;

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
        //Debug.Log(nameFigure);

        if (numeroFigure == 0/* ||*/ )
        {
            endFigure = true;
        }
        
        if(!endFigure)
        {
            numberFigureArray[curentNumberLevel, numberFigure] = numeroFigure;
            textArray[numberFigure].text = nameFigure;
            numberFigure++;
            
        }

        /*
        for (int i = 0; i < textArray.Length; i++)
        {
            Debug.Log(numberFigureArray[curentNumberLevel, i]);
        }*/

    } 
    
    public void getNameFigure(string name)
    {
        nameFigure = name;        
    }


    public void selectionLvlFinished(string levelNumber)
    {
        curentNumberLevel = int.Parse(levelNumber);

        for (int i = 0; i < textArray.Length; i++)
        {
           // Debug.Log(numberFigureArray[curentNumberLevel, i]);

            if (numberFigureArray[curentNumberLevel, i] == 0)
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
        }

        numberFigure = 0;
        endFigure = false;



      //  Debug.Log(curentNumberLevel);

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
                travelingFigureArray[travelingLevelFigure, i] = numberFigureArray[travelingLevelFigure, i];
                //Debug.Log(travelingFigureArray[travelingLevelFigure, i]);
        }
       

    }

}
