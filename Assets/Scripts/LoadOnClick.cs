using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public GameObject LoadingImg;
    public GameObject PauseImg;
    public GameObject NxtBtn;


    public void LoadScene (int sceneIndex)
    {
        LoadingImg.SetActive(true); //Displays loading screen overlay
        SceneManager.LoadScene(sceneIndex); //Loads scene based on the index of each scene
        Time.timeScale = 1;
    }

    public void LoadMenu(int sceneIndex)
    {
        LoadingImg.SetActive(true); //Displays loading screen overlay
        SceneManager.LoadScene(sceneIndex); //Loads scene based on the index of each scene
        Loader.level = 1;
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickPause()
    {
        if (Time.timeScale != 0) //If it is not currently paused
        {
            Time.timeScale = 0; //Pause the game
            PauseImg.SetActive(true);
        }
        else
        {
            Time.timeScale = 1; //Unpause the game
            PauseImg.SetActive(false);
        }

        Debug.Log("Paused");
    }

    public void LevelTransition (int sceneIndex)
    {
        Loader.level++;
        SceneManager.LoadScene(sceneIndex);
        if (Loader.maxLevel == true)
        {
            Debug.Log("MaxLevel CHecked");
            NxtBtn.SetActive(false);
        }
    }


}
