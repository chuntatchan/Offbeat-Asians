using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerFighting : MonoBehaviour
{

    [SerializeField]
    private CharacterStats[] allCharacters, enemies, players;
    [SerializeField]
    private GameObject overlay, enemyLayer, BGLayer, playerLayer, continueButton;
    [SerializeField]
    private Image[] button, overlay_button;
    private bool[] isButtonActive;
    [SerializeField]
    private UI_Icon[] UI_Icons;
    [SerializeField]
    private Text tbox;

    [SerializeField]
    private CharacterStats currentCharacter;
    [SerializeField]
    private CharacterStats[] turnOrder;
    [SerializeField]
    private int currentTurnCounter, currentStateCounter;

    private Skill currentSkillToBeDealt;
    private bool isEnemy;

    public GameObject currentPlayerPointer;

    [Space]

    [Header("Level Manager")]
    [SerializeField]
    private LevelManager levelManager;


    // Use this for initialization
    void Start()
    {

        if (GameObject.FindGameObjectWithTag("levelManager") != null)
        {
            levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        }

        //Set currentTurnCounter & currentStateCounter to 0 & set all buttons except button[0] to active
        currentTurnCounter = 0;
        currentStateCounter = 0;
        overlay.SetActive(false);
        isButtonActive = new bool[button.Length];
        displayState0UI();
        continueButton.SetActive(false);

        setPlayers();

		for (int i = 0; i < enemies.Length; i++) {
			allCharacters [i] = enemies [i];
		}
		for (int i = 0; i < players.Length; i++) {
			allCharacters [i + enemies.Length] = players [i];
		}

        resizeAllCharacters(enemies.Length + players.Length, ref allCharacters);

        //Sort allCharacters to turnOrder.
        turnOrder = sortToSpeed(allCharacters);
        
        //Set currentCharacter to turnOrder[currentTurnCounter]
        currentCharacter = turnOrder[currentTurnCounter];

        if (currentCharacter.gameObject.tag == "Enemy")
        {
            isEnemy = true;
        }
        else
        {
            isEnemy = false;
        }
        //Start Game (Animation Stuff?)

    }

    private void resizeAllCharacters(int Size, ref CharacterStats[] Group)
    {
        CharacterStats[] temp = new CharacterStats[Size];
        for (int c = 0; c < Size; c++)
        {
            temp[c] = Group[c];
        }
        Group = temp;

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        setPlayers();
    }

    private void setPlayers()
    {
        players[0] = GameObject.Find("Husband Stats").GetComponent<CharacterStats>();
        players[1] = GameObject.Find("Artist Stats").GetComponent<CharacterStats>();

        //Set Sliders
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetHPSlider().GetComponent<Slider>().maxValue = players[i].GetMaxHealth();
            players[i].GetHPSlider().GetComponent<Slider>().value = players[i].GetHealth();
        }
    }

    void Update()
    {
        disableBlankButtons();
        if (currentPlayerPointer != null)
        {
            currentPlayerPointer.transform.position = new Vector3(currentCharacter.GetHPSlider().transform.position.x, currentCharacter.GetHPSlider().transform.position.y + (Mathf.PingPong(Time.time / 2, 0.6f) + 4.1f), currentCharacter.GetHPSlider().transform.position.z);
        }
    }

    void changeUI()
    {
        //State 0 - Choice of Action
        //Display all possible choices
        //State 1 - Choice of Attack
        //Display all attacks
        //Wait for Input
        //if Attack, then execute attack
        //if return, then return to State 0

        //State 2 - Select Enemy To Attack
        //

        //State 3 - Defend (?)
        //Increased defenses until next turn

        //State 3 - Inventory (Global Consumable Inventory)
        //Display consumables
        //Wait for Input
        //if consumable, execute corresponding item effect
        //if return, then return to State 0



        if (currentStateCounter == 0)
        {
            displayState0UI();
        }
        else if (currentStateCounter == 1)
        {
            displayState1UI();
        }
        else if (currentStateCounter == 2)
        {
            displayState2UI();
        }
        else if (currentStateCounter == 3)
        {
            displayState3UI();
        }
        else if (currentStateCounter == 4)
        {
            displayState4UI();
        }

    }

    public void buttonClicked(int i)
    {
        if (isButtonActive[i] == false)
        {
            return;
        }
        else
        {
            if (currentStateCounter == 0)
            {
                if (i != 0)
                {
                    if (i == 1)
                    {
                        setUITo(i);
                    }
                }
            }
            else if (currentStateCounter == 1)
            {
                if (i == 0)
                {
                    setUITo(i);
                }
                else
                {
                    //Deal damage to corresponding character
                    currentSkillToBeDealt = currentCharacter.GetWeapon().weaponAbility(i-1);
                    setUITo(2);
                }
            }
            else if (currentStateCounter == 2)
            {
                if (i == 0)
                {
                    setUITo(i + 1);
                }
                else
                {
                    currentCharacter.attackAnimation();
                    enemies[i - 1].takeDamage(currentSkillToBeDealt.getDamage());
                    currentSkillToBeDealt = null;
                    if (enemies[i - 1].isDead())
                    {
                        enemies[i - 1].deathAnimation();
                    }
                    startNextTurn();
                }
            }
            else if (currentStateCounter == 3)
            {

            }
            else if (currentStateCounter == 4)
            {

            }
        }
    }

    public void onMouseHoverButton(int i)
    {
        if (currentStateCounter == 0)
        {
            if (i == 1)
            {
                tbox.text = "Attack";
            }
            else if (i == 2)
            {
                tbox.text = "Defend";
            }
            else if (i == 3)
            {
                tbox.text = "Bag";
            }
        }
        else if (currentStateCounter == 1)
        {
            if (i == 0)
            {
                tbox.text = "Return";
            }
            else
            {
				tbox.text = currentCharacter.GetWeapon().weaponAbility(i-1).getDescription();
            }
        }
        else if (currentStateCounter == 2)
        {

        }
        else if (currentStateCounter == 3)
        {
        }
        else if (currentStateCounter == 4)
        {
        }
    }

    public void onMouseExit()
    {
        tbox.text = "";
    }


    private void displayState0UI()
    {
        print("displayState0UI");
        overlay.SetActive(false);
        button[0].sprite = FindIcon("blank");
        button[1].sprite = FindIcon("attack");
        button[2].sprite = FindIcon("blank"); 	//TBA - SUPERS
        button[3].sprite = FindIcon("blank"); 	//TBA - Consumables
    }

    private void displayState1UI()
    {
        print("displayState1UI");
        overlay.SetActive(false);
        button[0].sprite = FindIcon("return");
        for (int i = 1; i <= currentCharacter.GetWeapon().numAbilities(); i++)
        {
            button[i].sprite = currentCharacter.GetWeapon().weaponAbility(i-1).getSkillIcon().GetSprite();
        }
        for (int i = currentCharacter.GetWeapon().numAbilities() + 1; i < button.Length; i++)
        {
            button[i].sprite = FindIcon("blank");
        }
    }


    private void displayState2UI()
    {
        print("displayState2UI");
        overlay.SetActive(true);
        overlay_button[0].sprite = FindIcon("return");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].isDead())
            {
                overlay_button[i + 1].sprite = FindIcon("blank");
            }
            else
            {
                overlay_button[i + 1].sprite = FindIcon("target");
            }
        }
        for (int i = enemies.Length + 1; i < overlay_button.Length; i++)
        {
            overlay_button[i].sprite = FindIcon("blank");
        }
    }


    private void displayState3UI()
    {
    }

    private void displayState4UI()
    {
    }

    private void disableBlankButtons()
    {
        if (currentStateCounter == 2)
        {
            for (int i = 0; i < overlay_button.Length; i++)
            {
                if (overlay_button[i].sprite != FindIcon("blank"))
                {
                    isButtonActive[i] = true;
                }
                else
                {
                    isButtonActive[i] = false;
                }
            }
        }
        else
        {

            for (int i = 0; i < button.Length; i++)
            {
                if (button[i].sprite != FindIcon("blank"))
                {
                    isButtonActive[i] = true;
                }
                else
                {
                    isButtonActive[i] = false;
                }
            }
        }
    }

    private void setUITo(int i)
    {
        tbox.text = "";
        currentStateCounter = i;
        changeUI();
    }

    private void startNextTurn()
    {
        bool playersAllDead = true;
        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].isDead())
            {
                playersAllDead = false;
            }
        }

        if (playersAllDead)
        {                   //PLAYERS ALL DEAD || GAME OVER
            print("Game Over");


        }
        else
        {

            bool enemiesAllDead = true;
            for (int i = 0; i < enemies.Length; i++)
            {
                if (!enemies[i].isDead())
                {
                    enemiesAllDead = false;
                }
            }

            if (enemiesAllDead)
            {               //ENEMIES ALL DEAD || WIN BATTLE
                print("You win!");
                spawnContinueButton();

            }
            else
            {
                overlay.SetActive(false);
                currentStateCounter = 0;
                currentTurnCounter = (currentTurnCounter + 1) % allCharacters.Length;
                currentCharacter = turnOrder[currentTurnCounter];
                if (currentCharacter.isDead())
                {
                    startNextTurn();
                }
                else
                {
                    if (currentCharacter.gameObject.tag == "Enemy")
                    {
                        isEnemy = true;
                        overlay.SetActive(false);
                        for (int i = 0; i < button.Length; i++)
                        {
                            button[i].sprite = FindIcon("blank");
                        }
                        StartCoroutine(startEnemyTurn());
                    }
                    else
                    {
                        isEnemy = false;
                        displayState0UI();
                    }
                }
            }
        }
    }

    private void spawnContinueButton()
    {
        overlay.SetActive(false);
        for (int i = 0; i < button.Length; i++)
        {
            button[i].sprite = FindIcon("blank");
        }
        continueButton.SetActive(true);
    }

    public void continueButtonClicked()
    {
        continueButton.SetActive(false);
        StartCoroutine(startFightingToWalkingTransition());
    }

    IEnumerator startFightingToWalkingTransition()
    {
        Vector3 BGLayerStartPos = BGLayer.transform.position;
        Vector3 enemyLayerStartPos = enemyLayer.transform.position;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetWalkingAnim(true);
        }
        while(BGLayer.transform.localPosition.x > -1279f)
        {
            BGLayer.transform.position = new Vector3(BGLayerStartPos.x - 0.08f, BGLayerStartPos.y, BGLayerStartPos.z);
            enemyLayer.transform.position = new Vector3(enemyLayerStartPos.x - 0.08f, enemyLayerStartPos.y, enemyLayerStartPos.z);
            BGLayerStartPos = BGLayer.transform.position;
            enemyLayerStartPos = enemyLayer.transform.position;
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetWalkingAnim(false);
        }


        //Load Next Level
        levelManager.LoadNextScene();



        yield return 0;
    }

    IEnumerator startEnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        int playerToAtk = Random.Range(0, players.Length);
        currentCharacter.AttackAnim();
        yield return new WaitForSeconds(0.75f);

        //Damage Chargable

        if (currentCharacter.GetWeapon().weaponAbility(0).GetIsChargable())
        {
            if (currentCharacter.GetWeapon().weaponAbility(0).GetIsChargedUp())
            {
                players[playerToAtk].takeDamage(currentCharacter.GetWeapon().weaponAttack(0));
                currentCharacter.GetWeapon().weaponAbility(0).SetIsChargedUp(false);
            }
            else
            {
                currentCharacter.GetWeapon().weaponAbility(0).SetIsChargedUp(true);
            }
        }
        else
        {
            players[playerToAtk].takeDamage(currentCharacter.GetWeapon().weaponAttack(0));
        }


        print("currentPlayer dealt" + currentCharacter.GetWeapon().weaponAttack(0) + " damage.");
        yield return new WaitForSeconds(0.5f);
        if (players[playerToAtk].isDead())
        {
            players[playerToAtk].deathAnimation();
        }
        yield return new WaitForSeconds(0.25f);
        startNextTurn();
        yield return 0;
    }

    private CharacterStats[] sortToSpeed(CharacterStats[] input)
    {
        CharacterStats temp;
        for (int write = 0; write < input.Length; write++)
        {
            for (int sort = 0; sort < input.Length - 1; sort++)
            {
               // print(input[sort].name + "comparing to " + input[sort + 1].name);
                if (input[sort].GetSpeed() < input[sort + 1].GetSpeed())
                {
                   // print("swap: " + input[sort].name + " with " + input[sort+1].name);
                    temp = input[sort + 1];
                    input[sort + 1] = input[sort];
                    input[sort] = temp;
                }
            }
        }
        print("turnSorted");
        return input;
    }

    public Sprite FindIcon(string _name)
    {
        if (_name != "")
        {
            for (int i = 0; i < UI_Icons.Length; i++)
            {
                if (UI_Icons[i].GetName() == _name)
                {
                    return UI_Icons[i].GetSprite();
                }
            }
            return UI_Icons[0].GetSprite();
        }
        else
        {
            return UI_Icons[0].GetSprite();
        }
    }

}
