using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void WorldSelectContinue()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("WorldSelect");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Ground", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Player", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
