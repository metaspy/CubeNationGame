using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelect : MonoBehaviour {
    public Transform parentCanvas;
    public GameObject panel;
    public int index = 1;
    public GameObject CreatePanel;
    public Text nameField;
    public Text seedField;

    private GameObject SM;
    // Use this for initialization
    void Start () {
        SM = GameObject.FindGameObjectWithTag("SceneManager");
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath + "/world/"))
        {
            GameObject obj = Instantiate(Resources.Load("Button"), parentCanvas) as GameObject;
            obj.GetComponent<Button>().onClick.AddListener((delegate { WorldSelection(file); }));
            Debug.Log(index + ": " + file);
            obj.transform.SetPositionAndRotation(new Vector3(parentCanvas.transform.position.x, index * 75, 0), new Quaternion(0, 0, 0, 0));

            obj.GetComponentInChildren<Text>().text = QuickLoad(file);
            
            index++;

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WorldSelection(string file)
    {
        SM.GetComponent<SceneManager>().WorldSaveFile = file;
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("WorldSelect");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Ground", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Player", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public string QuickLoad(string file)
    {

        WorldSave myWorldSave = SaveGameSystem.LoadGame(file) as WorldSave;
        
        string worldName = myWorldSave.worldName;

        return (worldName + " Seed: " + myWorldSave.seed + " explored:" +  myWorldSave.explored);
        
    }


    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("WorldSelect");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("CharacterSelect", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void WorldSelectContinue()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("WorldSelect");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Ground", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Player", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void createWorld()
    {
        int seedparse;
        if (nameField.text == "")
        {
            Debug.Log("Name field is empty");
        }
        else if (seedField.text =="" || !int.TryParse(seedField.text,out seedparse))
        {
            Debug.Log("Seed is empty or not an integer");
        } else
        {
            WorldSave newWorldSave = new WorldSave();
            newWorldSave.explored = 0;
            newWorldSave.worldName = nameField.text;
            newWorldSave.seed = seedparse;

            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            SaveGameSystem.SaveGame(newWorldSave, nameField.text + cur_time);

            SM.GetComponent<SceneManager>().WorldSaveFile = nameField.text + cur_time;

            WorldSelectContinue();
        }
    }

    public void showCreateWorld()
    {
        CreatePanel.SetActive(true);
    }

    public void createWorldCancel()
    {
        nameField.text = "";
        seedField.text = "";
        CreatePanel.SetActive(false);
    }

}

[Serializable]
public class WorldSave : SaveGame
{
    public string worldName = "Default";
    public int seed;
    public float explored;

}
