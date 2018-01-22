using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    // general 
    public bool menuShowing = false;
    public bool showQuests = false;
    private GameObject SM;
    public int classID;
    public int species;
    public int face;
    public string charName;


    // compass
    public Vector3 northDirection;
    public Transform player;
    public Quaternion missionDirection;
    public RectTransform northLayer;
    public RectTransform missionLayer;
    public Transform missionPlace;

    // XP and level variables
    private int currentXP;
    private int maxXP;
    public int level;
    private float minXPXValue;
    private float maxXPXValue;
    public RectTransform XPTransform;
    public Image XPImage;
    public int CurrentXP
    {
        get
        {
            return currentXP;
        }

        set
        {
            currentXP = value;
            HandleXP();
        }
    }

    // HP and damage variables
    private float minHPXValue;
    private float maxHPXValue;
    public RectTransform HPTransform;
    private int currentHP;
    public int maxHP;
    private int HPRegen;
    private bool HPRegenOnCD;
    private int HPRegenCooldown = 1;
    public Image HPImage;
    private bool onCD;
    public int damageCooldown = 1;
    public int CurrentHP
    {
        get {
            return currentHP;
        }

        set {
            currentHP = value;
            HandleHP();
        }
    }

    // MP and ability variables
    private float minMPXValue;
    private float maxMPXValue;
    public RectTransform MPTransform;
    private int currentMP;
    public int maxMP;
    private int MPRegen;
    private bool MPRegenOnCD;
    public Image MPImage;
    public KeyCode[] abilityKeyCode;
    public int[] abilityCooldown = new int[] { 5, 7, -1, -1, -1, -1, -1, -1, -1, -1 };
    public int[] ability = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    public int[] manaCost = new int[] { 50, 75, -1, -1, -1, -1, -1, -1, -1, -1 };
    public int CurrentMP
    {
        get{
            return currentMP;
        }

        set{
            currentMP = value;
            HandleMP();
        }
    }


    // Use this for initialization
    void Start () {
        abilityKeyCode = new KeyCode[10];
        for (int i = 0; i < abilityKeyCode.Length; i++) { abilityKeyCode[i] = KeyCode.None; }

        SM = GameObject.FindGameObjectWithTag("SceneManager");
        if (showQuests)
        {
           // make the damn thing invisible
        }

        // initialize the X position for the HP bars
        maxXPXValue = XPTransform.position.x;
        minXPXValue = XPTransform.position.x - XPTransform.rect.width;

        // initialize the X position for the HP bars
        maxHPXValue = HPTransform.position.x;
        minHPXValue = HPTransform.position.x - HPTransform.rect.width;

        // initialize X position for the MP bars
        maxMPXValue = MPTransform.position.x;
        minMPXValue = MPTransform.position.x - MPTransform.rect.width;
        
        // load character stats, abilities, and such
        LoadCharacter(SM.GetComponent<SceneManager>().CharacterSaveFile);

        // set current HP to Max
        CurrentHP = maxHP;

        // set current MP to max
        CurrentMP = maxMP;
        
        // start MP and HP regen
        StartCoroutine(MPRegeneration());
        StartCoroutine(HPRegeneration());
    }
	
    public void LoadCharacter(string filename)
    {
        MySaveGame mySaveGame = SaveGameSystem.LoadGame(filename) as MySaveGame;
        
        // level = mySaveGame.level;
        // CurrentXP = mySaveGame.currentXP;
        species = mySaveGame.species;
        face = mySaveGame.faceID;
        classID = mySaveGame.classID;
        charName = mySaveGame.playerName;

        // these will be replaced by our save file loading

        CurrentXP = 0;
        level = 1;
        ability[0] = 0;
        ability[1] = 1;


        // now that we can load from file, 
        // we need to calculate all the side stuff
        // skill tree or class affects

        maxHP = 100; // 
        HPRegen = 1; // 
        MPRegen = 1; // 
        maxXP = level * level + 1;
        
        
    }

    // Update is called once per frame
    void Update () {
        // check for button presses
        // someday we will allow for users to set which keys do what, until then hard coded button codes

        if (Input.GetKeyUp(KeyCode.Escape) && !menuShowing)
        {
            menuShowing = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("InGameMenus", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        } else if (Input.GetKeyUp(KeyCode.Escape) && menuShowing)
        {
            menuShowing = false;
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("InGameMenus");
        }

        if (Input.GetKeyUp(SM.GetComponent<SceneManager>().abilityCode[0]))
        {
            // check if there is an ability on that slot
            if (ability[0] != -1)
            {
                // use it - this will check for cooldowns and mana availability
                UseAbility(ability[0]);
            }
        }
        // check for button code 2 pressed
        if (Input.GetKeyUp(SM.GetComponent<SceneManager>().abilityCode[1]))
        {
            // check if there is an ability assigned to that slot
            if (ability[1] != -1)
            {
                // use it - this will check for cooldowns and mana availablility
                UseAbility(ability[1]);
            }
        }
        // check for button code 3 pressed
        if (Input.GetKeyUp(SM.GetComponent<SceneManager>().abilityCode[2]))
        {
            // check if there is an ability assigned to that slot
            if (ability[1] != -1)
            {
                // use it - this will check for cooldowns and mana availablility
                UseAbility(ability[2]);
            }
        }
        // check for button code 4 pressed
        if (Input.GetKeyUp(SM.GetComponent<SceneManager>().abilityCode[3]))
        {
            // check if there is an ability assigned to that slot
            if (ability[1] != -1)
            {
                // use it - this will check for cooldowns and mana availablility
                UseAbility(ability[3]);
            }
        }
        // check for button code 5 pressed
        if (Input.GetKeyUp(SM.GetComponent<SceneManager>().abilityCode[4]))
        {
            // check if there is an ability assigned to that slot
            if (ability[1] != -1)
            {
                // use it - this will check for cooldowns and mana availablility
                UseAbility(ability[4]);
            }
        }
        // check for button code 6 pressed
        if (Input.GetKeyUp(SM.GetComponent<SceneManager>().abilityCode[5]))
        {
            // check if there is an ability assigned to that slot
            if (ability[1] != -1)
            {
                // use it - this will check for cooldowns and mana availablility
                UseAbility(ability[5]);
            }
        }
        // check for button code 7 pressed
        if (Input.GetKeyUp(SM.GetComponent<SceneManager>().abilityCode[6]))
        {
            // check if there is an ability assigned to that slot
            if (ability[1] != -1)
            {
                // use it - this will check for cooldowns and mana availablility
                UseAbility(ability[6]);
            }
        }
        // ***** just for testing at the moment - Z and X key ****************
        if (Input.GetKeyUp(KeyCode.Z))
        {
            // deal 10 damage
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GainXP(100);
        }

        // ******* End just for testing ***********************************

        ChangeMissionDirection();
        ChangeNorthDirection();
    }

    // damages player for amount specified
    void TakeDamage(int amount)
    {
        // check if they are able to be damaged and have HP greater than or equal to 1
        if (!onCD && CurrentHP >= 1)
        {
            // start our invincibility cooldown
            StartCoroutine(CoolDownDamage());
            // check if this will put us below 0
            if (currentHP - amount >= 0)
            {
                // if it doesn't just subtract the amount
                CurrentHP -= amount;
            } else
            {
                // otherwise set HP to 0
                CurrentHP = 0;
                // kill the player
                Death();
            }
        }
    }

    // heals player for amount specified
    void Heal( int amount)
    {
        // if heal is less than max
        if (currentHP + amount <= maxHP)
        {
            // add the amount to current
            CurrentHP += amount;
        } else
        {
            // otherwise just put us to max
            CurrentHP = maxHP;
        }
    }

    // RIP Player
    void Death()
    {
        
        // explode into a hundred voxels and fall to the ground
        // request player to hit respawn
    }

    // Use Abilities specified
    void UseAbility(int abilityNum)
    {
        // check if we have the mana
        // will also check Cooldown, but I haven't figured out how yet
        if (currentMP-manaCost[abilityNum]>0)
        {
            // put ability on cooldown
            //StartCoroutine(CoolDownAbility(abilityNum));
            // put mana regen on cooldown
            StartCoroutine(CoolDownMPRegen());
            // subtract the mana cost from our current mana
            CurrentMP -= manaCost[abilityNum];

            DoAbility(abilityNum);
        }
    }

    // will need ability number and target if applicable
    void DoAbility(int abilityNum)
    {
        // just a test - we will have to have a database of abilities 
        if (abilityNum == 1)
        {
            Heal(10);
        }
    }

    // gain experience points
    void GainXP(int amount)
    {
        // if it is less than the max
        if (currentXP + amount < maxXP)
        {
            // add it 
            CurrentXP += amount;
        } else
        {
            // otherwise
            // while the combined is greater than maxXP
            while(currentXP + amount > maxXP)
            {
                // deduct the cost and level up
                amount = amount - (maxXP - currentXP);
                Levelup();
                
            }
            // set current xp to the remainder
            CurrentXP += amount;
            
        }
        // Debug.Log("current XP: " + currentXP);
    }

    void Levelup()
    {
        // Debug.Log("Level: " + level);
        // increase level
        level++;
        // set how much xp is needed for next level
        maxXP = level * level +1;
        // set current xp to 0
        CurrentXP = 0;
        // sets HP and MP to max on levelup
        CurrentHP = maxHP; 
        currentMP = maxMP;
    }

    // adjusts UI on mana bar based on current MP
    void HandleMP()
    {
        float newMPPos = (currentMP) * (maxMPXValue - minMPXValue) / (maxMP) + minMPXValue;
        MPTransform.position = new Vector3(newMPPos, MPTransform.position.y);
    }

    // adjusts UI on Heath bar based on current HP
    void HandleHP()
    {
        float newHPPos = (CurrentHP) * (maxHPXValue - minHPXValue) / (maxHP) + minHPXValue;
        HPTransform.position = new Vector3(newHPPos, HPTransform.position.y);
        // changes the color based on how low their HP is 100% to 50% turns from green to yellow
        if (CurrentHP > (maxHP / 2))
        {
            byte red = (byte) ((255 - (CurrentHP * 255 / maxHP))*2);
            HPImage.color = new Color32(red, 255, 0, 255);
        }
        // 50% to 100% turns from yellow to red
        else
        {
            byte green = (byte)((CurrentHP * 255 / maxHP)*2);
            HPImage.color = new Color32(255, green, 0, 255);
        }
    }

    // adjusts UI on Experience bar based on current XP
    void HandleXP()
    {

        float newXPPos = (CurrentXP) * (maxXPXValue - minXPXValue) / (maxXP) + minXPXValue;
        //Debug.Log(CurrentXP);
        //Debug.Log(newXPPos);
        XPTransform.position = new Vector3(newXPPos, XPTransform.position.y);
    }


    // invincibility timer
    IEnumerator CoolDownDamage()
    {
        // turns invincibilty cooldown to true
        onCD = true;
        // turns regen off
        HPRegenOnCD = true;
        // timer of invincibility
        yield return new WaitForSeconds(damageCooldown);
        // turns back off the invincibility
        onCD = false;
        // timer for Regen
        yield return new WaitForSeconds(HPRegenCooldown);
        // turns back on regen
        HPRegenOnCD = false;
    }

    // regen timer
    IEnumerator HPRegeneration()
    {
        // each second
        yield return new WaitForSeconds(1);
        // if current hp 
        if (CurrentHP <= maxHP && !HPRegenOnCD)
        {
            // if current hp + regen is less than max
            if (currentHP + HPRegen <= maxHP)
            {
                // just add it
                CurrentHP += HPRegen;
            } else
            {
                // otherwise just make it max
                CurrentHP = maxHP;
            }
            
        }
        // keep the regen going
        StartCoroutine(HPRegeneration());
    }

    // ability cooldown timer
    IEnumerator CoolDownAbility(int ability)
    {
        // when i figure out how to put abilities on cooldown, i will do it here
        yield return new WaitForSeconds(abilityCooldown[ability]);
        
    }

    // MP regen timer
    IEnumerator CoolDownMPRegen()
    {
        // puts MP regen on cooldown
        MPRegenOnCD = true;
        // wait for cooldown
        yield return new WaitForSeconds(2);
        // turn MP regen back on
        MPRegenOnCD = false;
    }

    // MP regen
    IEnumerator MPRegeneration()
    {
        // each second
        yield return new WaitForSeconds(1);
        // if 
        if (currentMP <= maxMP && !MPRegenOnCD)
        {
            // if the current MP plus regen is less than max
            if (currentMP + MPRegen <= maxHP)
            {
                // just add it
                CurrentMP += MPRegen;
            } else
            {
                // otherwise make it max
                CurrentMP = maxHP;
            }
        }
        // keep the regen going
        StartCoroutine(MPRegeneration());
    }

    public void ChangeNorthDirection()
    {
        northDirection.z = player.eulerAngles.y;
        northLayer.localEulerAngles = northDirection;
    }
    public void ChangeMissionDirection()
    {
        Vector3 dir = transform.position - missionPlace.position;
        missionDirection = Quaternion.LookRotation(dir);
        missionDirection.z = -missionDirection.y;
        missionDirection.y = 0;
        missionDirection.x = 0;

        missionLayer.localRotation = missionDirection * Quaternion.Euler(northDirection);
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
