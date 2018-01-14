using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CharSelectContinue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("WorldSelect");
        
    }
}
