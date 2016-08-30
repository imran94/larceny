using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public GameObject LoadingImg;
    public GameObject PauseImg;

    public void LoadScene (int sceneIndex)
    {
        LoadingImg.SetActive(true); //Displays loading screen overlay
        SceneManager.LoadScene(sceneIndex); //Loads scene based on the index of each scene
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickPause()
    {
        if (Time.timeScale != 0 && LoadingImg == false)
        {
            Time.timeScale = 0;
            PauseImg.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseImg.SetActive(false);
        }
    }


}
