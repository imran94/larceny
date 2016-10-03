using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public GameObject LoadingImg;
    public GameObject PauseImg;
    public GameObject NextBtn;
    private Player player;

    public void LoadScene (int sceneIndex)
    {
        Loader.level = 1;

        LoadingImg.SetActive(true); //Displays loading screen overlay
        SceneManager.LoadScene(sceneIndex); //Loads scene based on the index of each scene
        Time.timeScale = 1;
    }

    public void LoadSceneContinue(int sceneIndex)
    {
        LoadingImg.SetActive(true); //Displays loading screen overlay
        SceneManager.LoadScene(sceneIndex); //Loads scene based on the index of each scene
        Time.timeScale = 1;
    }

    public void LoadMenu(int sceneIndex)
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (Time.timeScale != 0) //If it is not currently paused
        {
            Time.timeScale = 0; //Pause the game
            PauseImg.SetActive(true);
            player.input = false;
        }
        else
        {
            Time.timeScale = 1; //Unpause the game
            PauseImg.SetActive(false);
            player.input = true;
        }
    }

    public void LevelTransition (int sceneIndex)
    {
        Loader.level++;
        SceneManager.LoadScene(sceneIndex);
    }


}
