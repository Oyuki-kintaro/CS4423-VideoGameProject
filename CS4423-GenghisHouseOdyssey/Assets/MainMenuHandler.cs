using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private ScreenFader screenFader;
    public void Play(){
        screenFader.FadeToColor("House");
        //SceneManager.LoadScene("House");

    }

    public void Quit(){
        Application.Quit();
    }
}
