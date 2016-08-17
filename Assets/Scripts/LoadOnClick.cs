using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public GameObject LoadingImg;
    public GameObject WinImg;

    public void LoadScene (int sceneIndex)
    {
        LoadingImg.SetActive(true); //Displays loading screen overlay
        SceneManager.LoadScene(sceneIndex); //Loads scene based on the index of each scene
    }

    //public void LoadMenu( int sceneIndex)
    //{
    //    SceneManager.LoadScene(sceneIndex); 
    //}

    public void OnClickWin ()  //Temporary way to enable WinImg
    {
        WinImg.SetActive(true);
    }

    //public void OnClickRestart()
    //{

    //}

    public void OnClickExit()
    {
        Application.Quit();
    }
}
