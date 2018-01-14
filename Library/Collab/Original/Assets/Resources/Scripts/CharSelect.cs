using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour {
    public GameObject characterButton;
    public Transform parentCanvas;
    public GameObject panel;
	// Use this for initialization
	void Start () {
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
        {
            characterButton = Instantiate(Resources.Load("Button")) as GameObject;
            Debug.Log(characterButton);
            characterButton.transform.SetParent(parentCanvas);
            characterButton.GetComponentInChildren<Text>().text = file;
            
            characterButton.transform.SetPositionAndRotation(new Vector3(242, 100, 0), new Quaternion(0, 0, 0, 0));
            
            //  characterButton.onClick.AddListener(delegate { CharacterSelect(file); });
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CharacterSelect(string file)
    {
        Debug.Log(file);
    }

    public void CharSelectContinue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("WorldSelect");
        
    }
}
