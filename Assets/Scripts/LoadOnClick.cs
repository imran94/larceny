using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public GameObject LoadingImg;

    public void LoadScene (int sceneIndex)
    {
        LoadingImg.SetActive(true);
        SceneManager.LoadScene(sceneIndex);
    }

}
