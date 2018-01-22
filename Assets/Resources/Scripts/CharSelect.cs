using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour {
    public Transform parentCanvas;
    public GameObject panel;
    public int index = 1;
    private GameObject SM;

    // Use this for initialization
    void Start () {
        SM = GameObject.FindGameObjectWithTag("SceneManager");
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
        {
            Debug.Log(index + ": " + file);
            GameObject obj = Instantiate(Resources.Load("Button"), parentCanvas) as GameObject;
            obj.GetComponent<Button>().onClick.AddListener((delegate { CharacterSelect(file); }));
            obj.transform.SetPositionAndRotation(new Vector3(parentCanvas.transform.position.x, index * 75, 0), new Quaternion(0, 0, 0, 0));

            obj.GetComponentInChildren<Text>().text = QuickLoadCharacter(file);
            
            index++;
            
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string QuickLoadCharacter(string file)
    {
        Debug.Log(file);
        MySaveGame mySaveGame = SaveGameSystem.LoadGame(file) as MySaveGame;

        string[] speciesName = new string[] { "Human", "Frog", "Robot" };
        string[] className = new string[] { "Warrior", "Ranger", "Necromancer" };
        Debug.Log(mySaveGame);
        Debug.Log(mySaveGame.playerName);
        string charName = mySaveGame.playerName;

        return (charName + " Level: " + mySaveGame.level + " " + speciesName[mySaveGame.species] + " " + className[mySaveGame.classID] );
    }

    public void CharacterSelect(string file)
    {
        SM.GetComponent<SceneManager>().CharacterSaveFile = file;
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("CharacterSelect");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("WorldSelect", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        
    }

    public void NewCharacter()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("CharacterSelect");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("NewChar", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("CharacterSelect");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
