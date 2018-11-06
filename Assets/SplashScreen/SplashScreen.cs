using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public Canvas CanvasSplashScreen;
    public Image SplashImage;

    // Use this for initialization
    void Start () {
        StartCoroutine(ToDatabase());
    }

    IEnumerator ToDatabase()
    {
        SplashImage.canvasRenderer.SetAlpha(0.0f);
        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        CanvasSplashScreen.gameObject.SetActive(false);
        SceneManager.LoadScene("Database");
    }

    void FadeIn()
    {
        SplashImage.CrossFadeAlpha(1.0f, 2.5f, false);
    }

    void FadeOut()
    {
        SplashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
