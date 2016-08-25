using UnityEngine;
using System.Collections;

public class PreserveOnLoad : MonoBehaviour {
    //Initialization
    public AudioClip BGM_Lvl1_Surface_Tension;
    private AudioSource source;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //For the destruction of BGM_Menu when there is more than one instance
        //if (GameObject.FindGameObjectsWithTag("Music_Menu").Length > 1)
        //{
        //    Destroy("Music_Menu");
        //}

        source = GetComponent<AudioSource>(); //Gets audio file
    }

    void OnLevelWasLoaded (int sceneIndex)
    {
        if (sceneIndex == 1)
        {
            Destroy(gameObject);
            //Implementation for BGM_Lvl1
            source.clip = BGM_Lvl1_Surface_Tension;
            source.Play();
        }
    }
}
