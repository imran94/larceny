using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public GameObject LoadingImg;

    public void LoadScene (int sceneIndex)
    {
        LoadingImg.SetActive(true); //Displays loading screen overlay
        SceneManager.LoadScene(sceneIndex); //Loads scene based on the index of each scene
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
