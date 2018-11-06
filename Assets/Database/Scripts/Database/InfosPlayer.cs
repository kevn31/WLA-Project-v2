using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfosPlayer : MonoBehaviour {

    public Text txtPseudo, txtPoints, txtleaderboard, txtleaderboard2, txtleaderboard3, txtleaderboard4, txtleaderboard5;
    DB_Manager manager;
    public Text containerPoints, containerLeaderboard;
    public string MyPoints;

    // Use this for initialization
	void Start () {
        manager = GameObject.Find("MySqlManager").GetComponent<DB_Manager>();
        txtPseudo.text = "Pseudo: " + manager.IPseudo;
    }

    /*#region Add Points
    public void addPoints() {
        manager.IPoints += 10;
        txtPoints.text = "" + manager.IPoints;
    }

    public void addPoints2()
    {
        manager.IPoints2 += 5;
        txtPoints.text = "" + manager.IPoints2;
    }

    public void addPoints3()
    {
        manager.IPoints3 += 15;
        txtPoints.text = "" + manager.IPoints3;
    }

    public void addPoints4()
    {
        manager.IPoints4 += 20;
        txtPoints.text = "" + manager.IPoints4;
    }

    public void addPoints5()
    {
        manager.IPoints5 += 25;
        txtPoints.text = "" + manager.IPoints5;
    }
    #endregion*/

    /*#region Save Points
    public void SavePoints()
    {
        manager.savePoints();
    }

    public void SavePoints2()
    {
        manager.savePoints2();
    }

    public void SavePoints3()
    {
        manager.savePoints3();
    }

    public void SavePoints4()
    {
        manager.savePoints4();
    }

    public void SavePoints5()
    {
        manager.savePoints5();
    }
    #endregion*/

   /* #region Scores
    public void Leaderboard()
    {
        string data = manager.LeaderBoard(5);
        txtleaderboard.text = data;
        containerLeaderboard.text = txtleaderboard.text;

        MyPoints = "My Points: " + manager.IPoints;
        containerPoints.text = MyPoints;
    }

    public void Leaderboard2()
    {
        string data = manager.LeaderBoard2(5);
        txtleaderboard2.text = data;
        containerLeaderboard.text = txtleaderboard2.text;

        MyPoints = "My Points: " + manager.IPoints2;
        containerPoints.text = MyPoints;
    }

    public void Leaderboard3()
    {
        string data = manager.LeaderBoard3(5);
        txtleaderboard3.text = data;
        containerLeaderboard.text = txtleaderboard3.text;

        MyPoints = "My Points: " + manager.IPoints3;
        containerPoints.text = MyPoints;
    }

    public void Leaderboard4()
    {
        string data = manager.LeaderBoard4(5);
        txtleaderboard4.text = data;
        containerLeaderboard.text = txtleaderboard4.text;

        MyPoints = "My Points: " + manager.IPoints4;
        containerPoints.text = MyPoints;
    }

    public void Leaderboard5()
    {
        string data = manager.LeaderBoard5(5);
        txtleaderboard5.text = data;
        containerLeaderboard.text = txtleaderboard5.text;

        MyPoints = "My Points: " + manager.IPoints5;
        containerPoints.text = MyPoints;
    }
    #endregion*/
}