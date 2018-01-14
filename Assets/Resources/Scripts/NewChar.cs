using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewChar : MonoBehaviour {
    public Transform playerParent;
    private GameObject playerModel;
    private GameObject playerWeapon;
    private GameObject playerFace;
    public Text faceText;
    public Text speciesText;
    public Text classText;
    public InputField nameField;

    private int currentFace = 0;
    private int currentSpecies = 0;
    private int currentClass = 0;

    private int[] maxFace = new int[] { 2, 3, 4 }; // largest amount of faces available for each species
    private int maxSpecies = 3;
    private string[] speciesName = new string[] { "Human", "Frog", "Robot" };

    private int maxClass = 3;
    private string[] className = new string[] { "Warrior", "Ranger", "Necromancer" };
    
    // Use this for initialization
    void Start () {
        UpdateModel();
     }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextFace()
    {
        if (currentFace < maxFace[currentSpecies]-1)
        {
            currentFace++;
        } else
        {
            currentFace = 0;
        }
        faceText.text = "Face " + currentFace;
        UpdateModel();
    }

    public void PrevFace()
    {
        if (currentFace > 0)
        {
            currentFace--;
        }
        else
        {
            currentFace = maxFace[currentSpecies]-1;
        }
        faceText.text = "Face " + currentFace;
        UpdateModel();
    }

    public void NextSpecies()
    {
        
        if (currentSpecies < maxSpecies-1)
        {
            currentSpecies++;
        }
        else
        {
            currentSpecies = 0;
        }
        speciesText.text = speciesName[currentSpecies];
        if (currentFace >= maxFace[currentSpecies])
        {
            NextFace();
        }
        else
        {
            UpdateModel();
        }
    }
    public void PrevSpecies()
    {

        if (currentSpecies > 0)
        {
            currentSpecies--;
        }
        else
        {
            currentSpecies = maxSpecies-1;
        }
        speciesText.text = speciesName[currentSpecies];
        
        if (currentFace >= maxFace[currentSpecies])
        {
            NextFace();
        } else
        {
            UpdateModel();
        }
    }
    
    public void NextClass()
    {
        if (currentClass < maxClass-1)
        {
            currentClass++;
        }
        else
        {
            currentClass = 0;
        }
        classText.text = className[currentClass];
        UpdateModel();
    }
    public void PrevClass()
    {
        if (currentClass > 0)
        {
            currentClass--;
        }
        else
        {
            currentClass = maxClass - 1;
        }
        speciesText.text = className[currentClass];
        UpdateModel();
    }
    

    public void UpdateModel()
    {
        if (playerModel!= null)
        {
            DestroyImmediate(playerModel);
        }
        if (playerWeapon != null)
        {
            DestroyImmediate(playerWeapon);
        }
        if (playerFace != null)
        {
            DestroyImmediate(playerFace);
        }
        playerWeapon = Instantiate(Resources.Load(className[currentClass])) as GameObject;
        playerWeapon.SetActive(true);
        playerWeapon.transform.SetParent(playerParent);
        playerWeapon.transform.SetPositionAndRotation(new Vector3(242, 0, 270), new Quaternion(0, 0, 0, 0));

        playerModel = Instantiate(Resources.Load(speciesName[currentSpecies])) as GameObject;
        playerModel.SetActive(true);
        playerModel.transform.SetParent(playerParent);
        playerModel.transform.SetPositionAndRotation(new Vector3(250, -7, 270),new Quaternion(0,180,0,0));

        playerFace = Instantiate(Resources.Load(speciesName[currentSpecies]+"Face"+currentFace)) as GameObject;
        playerFace.SetActive(true);
        playerFace.transform.SetParent(playerParent);
        playerFace.transform.SetPositionAndRotation(new Vector3(250, -7, 270), new Quaternion(0, 180, 0, 0));

    }

    public void CreateNew()
    {

        if (nameField.text == "")
        {
            Debug.Log("Name field is empty");
        } else
        {
            MySaveGame mySaveGame1 = new MySaveGame();
            mySaveGame1.playerName = nameField.text;
            mySaveGame1.species = currentSpecies;
            mySaveGame1.classID = currentClass;
            mySaveGame1.faceID = currentFace;
            mySaveGame1.level = 1;
            mySaveGame1.currentXP = 0;

            // we will need to put inventory, skill bar, and talenttree in here
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            SaveGameSystem.SaveGame(mySaveGame1, nameField.text + cur_time ); // Saves as MySaveGame.sav

            // Loading a saved game.
            // MySaveGame mySaveGame2 = SaveGameSystem.LoadGame("MySaveGame") as MySaveGame;
            //Debug.Log(mySaveGame2.playerName); 
            //Debug.Log(mySaveGame2.species);
            //Debug.Log(mySaveGame2.classID);
            //Debug.Log(mySaveGame2.faceID);
        }
        
    }

}

[Serializable]
public class MySaveGame : SaveGame
{
    public string playerName = "Default";

    public int species { get; set; }

    public int classID { get; set; }

    public int faceID { get; set; }

    public int level { get; set; }
    
    public int currentXP { get; set; }

    public List<int> inventory { get; set; } // we will have to work out how we handle this later

    public List<int> abilityBar { get; set; } // we will have to work out how we handle this later

    public List<int> skillTree { get; set; } // we will have to work out how we handle this later
}
