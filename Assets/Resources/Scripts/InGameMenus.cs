using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenus : MonoBehaviour {

    private GameObject currentPlayer;
    private GameObject SM;
    private float mainSoundLevel;
    private float BGSoundLevel;
    private float musicLevel;
    private float renderDist;
    public Slider mainSoundSlider;
    public Text mainSoundText;
    public Slider BGSoundSlider;
    public Text BGSoundText;
    public Slider musicSoundSlider;
    public Text musicSoundText;
    public Slider renderSlider;
    public Text renderText;

    public MySaveOptions myOptions;

    public KeyCode[] abilityCode = new KeyCode[10];
    public Button[] abilityButton = new Button[10];
    
    private int setAbilityCode = -1;

    // Use this for initialization
    void Start () {
		currentPlayer = GameObject.FindGameObjectWithTag("Player");
        SM = GameObject.FindGameObjectWithTag("SceneManager");
        myOptions = new MySaveOptions();

        LoadOptions();

        //
        updateBGSound();
        updateMainSound();
        updateMusic();
        updateRenderDist();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            // check to see if a keycode needs setting
            if (setAbilityCode != -1)
            {
                abilityCode[setAbilityCode-1] = e.keyCode;
                abilityButton[setAbilityCode-1].GetComponentInChildren<Text>().text = abilityCode[setAbilityCode-1].ToString();
                setAbilityCode = -1;
            }
        }
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

        MySaveGame mySaveGame1 = new MySaveGame();
        mySaveGame1.playerName = currentPlayer.GetComponent<Player>().name;
        mySaveGame1.species = currentPlayer.GetComponent<Player>().species;
        mySaveGame1.classID = currentPlayer.GetComponent<Player>().classID;
        mySaveGame1.faceID = currentPlayer.GetComponent<Player>().face;
        mySaveGame1.level = currentPlayer.GetComponent<Player>().level;
        mySaveGame1.currentXP = currentPlayer.GetComponent<Player>().CurrentXP;

        // we will need to put inventory, skill bar, and talenttree in here
        //mySaveGame1.talentTree = currentPlayer.GetComponent<Player>().talentTree;


        SaveGameSystem.SaveGame(mySaveGame1, fileName); // Saves as MySaveGame.sav

        Debug.Log("saving character");
    }

    public void updateMainSound()
    {
        mainSoundLevel = mainSoundSlider.value;
        mainSoundText.text = (mainSoundLevel * 100).ToString("0") + "%";
        
    }

    public void updateBGSound()
    {
        BGSoundLevel = BGSoundSlider.value;
        BGSoundText.text = (BGSoundLevel * 100).ToString("0") + "%";
        
    }

    public void updateMusic()
    {
        musicLevel = musicSoundSlider.value;
        musicSoundText.text = (musicLevel * 100).ToString("0") + "%";
        
    }

    public void updateRenderDist()
    {
        renderDist = renderSlider.value;
        renderText.text = (renderDist * 90 + 10).ToString("0");
        
    }

    public void setAbility(int ability)
    {
        // make sure they are all off before turning one on
        setAbilityCode = ability;
    }

    public void SaveOptions()
    {
        // Save Options
        
        myOptions.mainSoundLevel = mainSoundLevel;
        myOptions.backgroundSoundLevel = BGSoundLevel;
        myOptions.musicSoundLevel = musicLevel;
        myOptions.renderDistance = renderDist;
        myOptions.firstAbility = abilityCode[0];
        myOptions.secondAbility = abilityCode[1];
        myOptions.thirdAbility = abilityCode[2];
        myOptions.fourthAbility = abilityCode[3];
        myOptions.fifthAbility = abilityCode[4];
        myOptions.sixthAbility = abilityCode[5];
        myOptions.seventhAbility = abilityCode[6];

        SM.GetComponent<SceneManager>().mainSoundLevel = mainSoundLevel;
        SM.GetComponent<SceneManager>().BGSoundLevel = BGSoundLevel;
        SM.GetComponent<SceneManager>().musicLevel = musicLevel;
        SM.GetComponent<SceneManager>().renderDist = renderDist;

        for (int i = 0; i < SM.GetComponent<SceneManager>().abilityCode.Length; i++) { SM.GetComponent<SceneManager>().abilityCode[i] = abilityCode[i]; }


        SaveOptionsSystem.SaveOptions(myOptions, "Options.txt");
        // then close options menu
        CloseOptions();
    }

    public void LoadOptions()
    {
        myOptions = SaveOptionsSystem.LoadOptions("Options.txt") as MySaveOptions;
        
        mainSoundSlider.value = myOptions.mainSoundLevel;
        BGSoundSlider.value = myOptions.backgroundSoundLevel;
        musicSoundSlider.value = myOptions.musicSoundLevel;
        renderSlider.value = myOptions.renderDistance;

        abilityCode[0] = myOptions.firstAbility;
        abilityButton[0].GetComponentInChildren<Text>().text = abilityCode[0].ToString();

        abilityCode[1] = myOptions.secondAbility;
        abilityButton[1].GetComponentInChildren<Text>().text = abilityCode[1].ToString();

        abilityCode[2] = myOptions.thirdAbility;
        abilityButton[2].GetComponentInChildren<Text>().text = abilityCode[2].ToString();

        abilityCode[3] = myOptions.fourthAbility;
        abilityButton[3].GetComponentInChildren<Text>().text = abilityCode[3].ToString();

        abilityCode[4] = myOptions.fifthAbility;
        abilityButton[4].GetComponentInChildren<Text>().text = abilityCode[4].ToString();

        abilityCode[5] = myOptions.sixthAbility;
        abilityButton[5].GetComponentInChildren<Text>().text = abilityCode[5].ToString();

        abilityCode[6] = myOptions.seventhAbility;
        abilityButton[6].GetComponentInChildren<Text>().text = abilityCode[6].ToString();

        for (int i = 0; i < SM.GetComponent<SceneManager>().abilityCode.Length; i++) { SM.GetComponent<SceneManager>().abilityCode[i] = abilityCode[i]; Debug.Log(abilityCode[i].ToString()); }

    }

    public void CloseOptions()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Options");
    }

}
