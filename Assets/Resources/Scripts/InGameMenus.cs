using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenus : MonoBehaviour {

    private GameObject currentPlayer;
    private GameObject SM;

	// Use this for initialization
	void Start () {
		currentPlayer = GameObject.FindGameObjectWithTag("Player");
        SM = GameObject.FindGameObjectWithTag("SceneManager");
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    
    public void CharSelectButton()
    {
        // before leaving this game save the character
        SaveChar();
        // unload the menus / player / ground scenes - leave the scenemanager
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("InGameMenus");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ground");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Player");
        // load the character select scene
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("CharacterSelect", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void ResumeButton()
    {
        // turn menu showing to false
        currentPlayer.GetComponent<Player>().menuShowing = false;
        // unload menus scene
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("InGameMenus");
    }

    public void ExitButton()
    {
        // save character before exiting
        SaveChar();
        // quit ze application
        Application.Quit();
    }

    public void OptionButton()
    {
        // load the options scene additively to the current scenes
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Options", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
    public void SaveChar()
    {
        // get the name of the character save file from the scene manager
        string fileName = SM.GetComponent<SceneManager>().CharacterSaveFile;

        Debug.Log("saving character");
    }

    public void SaveOptions()
    {
        // Save Options
        // then close options menu
        CloseOptions();
    }

    public void CloseOptions()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Options");
    }

}
