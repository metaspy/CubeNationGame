using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SceneManager", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Main");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("CharacterSelect", UnityEngine.SceneManagement.LoadSceneMode.Additive);

    }

    public void OptionButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Options", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
