using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager : MonoBehaviour {

    public string CharacterSaveFile;
    public string WorldSaveFile;

    public float mainSoundLevel;
    public float BGSoundLevel;
    public float musicLevel;
    public float renderDist;

    public KeyCode[] abilityCode = new KeyCode[10];
 

    // Use this for initialization
    void Start () {
        MySaveOptions myOptions = SaveOptionsSystem.LoadOptions("Options.txt") as MySaveOptions;

        mainSoundLevel = myOptions.mainSoundLevel;
        BGSoundLevel = myOptions.backgroundSoundLevel;
        musicLevel = myOptions.musicSoundLevel;
        renderDist = myOptions.renderDistance;

        abilityCode[0] = myOptions.firstAbility;
        abilityCode[1] = myOptions.secondAbility;
        abilityCode[2] = myOptions.thirdAbility;
        abilityCode[3] = myOptions.fourthAbility;
        abilityCode[4] = myOptions.fifthAbility;
        abilityCode[5] = myOptions.sixthAbility;
        abilityCode[6] = myOptions.seventhAbility;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
