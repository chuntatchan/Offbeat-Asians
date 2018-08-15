using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerFightingv2 : MonoBehaviour
{

    [SerializeField]
    private CharacterFightScene[] allCharacters, enemies, players;
    [SerializeField]
    private GameObject enemyLayer, BGLayer, playerLayer, continueButton, fadeScreen;

	[Header("Weapon Overlay")]
    [SerializeField]
    private GameObject[] overlayWeaponDetailConnector;
	[SerializeField]
	private GameObject[] overlayWeaponDetailTab;
	[SerializeField]
	private GameObject overlayWeapon, overlayWeaponDetail;
	[SerializeField]
	private Image[] weaponButtonImage;

	[Header("Inventory Overlay")]
	[SerializeField]
	private ItemSlot[] itemSlots;
	[SerializeField]
	private GameObject overlayBag;

	[Space]

    [SerializeField]
    private BoxCollider2D[] enemiesColliders;
    [SerializeField]
	private Image[] buttonImage;
	[SerializeField]
    private bool[] isButtonActive;
    [SerializeField]
    private UI_IconList UI_Icons;

    [SerializeField]
    private CharacterFightScene currentCharacter;
    [SerializeField]
    private CharacterFightScene[] turnOrder;
    [SerializeField]
    private int currentTurnCounter, currentStateCounter;

    private Skill currentSkillToBeDealt;
	private Consumable currentConsumableToUse;
	private int currentConsumableInt;
    private bool isEnemy;

    public GameObject currentPlayerPointer;

    private bool hasSetPlayers = false;

    [Space]

    [Header("Level Manager")]
    [SerializeField]
    private LevelManager levelManager;

	[Space]

	[Header("Inventory Manager")]
	[SerializeField]
	private Inventory playerInventory;
	[SerializeField]
	private WeaponStats weaponToGive;
	[SerializeField]
	private Consumable consumableToGive;
	[SerializeField]
	private ItemsList allItems;


    // Use this for initialization
    void Start()
    {
		StartCoroutine (fadeIn());
        if (GameObject.FindGameObjectWithTag("levelManager") != null)
        {
            levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        }
		if (GameObject.FindGameObjectWithTag("Inventory") != null)
		{
			playerInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
		}

		float itemToGiveRandomVal = Random.value;
		int randomValInt = -1;
		if (itemToGiveRandomVal <= 0.1f) {
			//Spawn Legendary
			itemToGiveRandomVal = Random.value;
			if (itemToGiveRandomVal <= 0.5f) {
				//Spawn Legendary Consumable
				randomValInt = Random.Range(0, allItems.allLegendaryConsumables.Length);
				consumableToGive = allItems.allLegendaryConsumables [randomValInt].consumablePrefab;
			} else {
				//Spawn Weapon
				randomValInt = Random.Range(0, allItems.allWeapons.GetLength());
				weaponToGive = allItems.allWeapons.GetRandomWeapon(randomValInt);
			}
		} else if (itemToGiveRandomVal <= 0.25f) {
			//Spawn Rare
			randomValInt = Random.Range(0, allItems.allRareConsumables.Length);
			consumableToGive = allItems.allRareConsumables [randomValInt].consumablePrefab;
		} else if (itemToGiveRandomVal <= 0.5f) {
			//Spawn Uncommon
			randomValInt = Random.Range(0, allItems.allUncommonConsumables.Length);
			consumableToGive = allItems.allUncommonConsumables [randomValInt].consumablePrefab;
		} else {
			//Spawn Common
			randomValInt = Random.Range(0, allItems.allCommonConsumables.Length);
			consumableToGive = allItems.allCommonConsumables [randomValInt].consumablePrefab;
		}


        //Set currentTurnCounter & currentStateCounter to 0 & set all buttons except buttonImage[0] to active
        currentTurnCounter = 0;
        currentStateCounter = 0;
        overlayWeapon.SetActive(false);
        overlayBag.SetActive(false);


        isButtonActive = new bool[itemSlots.Length+1];


		setUITo (0);
        continueButton.SetActive(false);

        hasSetPlayers = setPlayers();

		//Set Sliders
		for (int i = 0; i < players.Length; i++)
		{
			players[i].GetHPSlider().maxValue = players[i].GetMaxHealth();
			players[i].GetHPSlider().value = players[i].GetHealth();
		}

        if (hasSetPlayers)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                allCharacters[i] = enemies[i];
            }
            for (int i = 0; i < players.Length; i++)
            {
                allCharacters[i + enemies.Length] = players[i];
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
    }

	IEnumerator fadeIn() {
		float a = 1f;
		Color screenColor = fadeScreen.GetComponent<Image> ().color;
		fadeScreen.GetComponent<Image> ().color = new Color (screenColor.r, screenColor.g, screenColor.b, a);
		fadeScreen.SetActive (true);
		while (a > 0) {
			a -= 0.04f;
			fadeScreen.GetComponent<Image> ().color = new Color (screenColor.r, screenColor.g, screenColor.b, a);
			yield return new WaitForEndOfFrame ();
		}
		fadeScreen.SetActive (false);
	}

	IEnumerator fadeOut() {
		float a = 0;
		fadeScreen.SetActive (true);
		Color screenColor = fadeScreen.GetComponent<Image> ().color;
		while (a < 1) {
			a += 0.04f;
			fadeScreen.GetComponent<Image> ().color = new Color (screenColor.r, screenColor.g, screenColor.b, a);
			yield return new WaitForEndOfFrame ();
		}
		levelManager.LoadNextScene ();
	}

    private void resizeAllCharacters(int Size, ref CharacterFightScene[] Group)
    {
        CharacterFightScene[] temp = new CharacterFightScene[Size];
        for (int c = 0; c < Size; c++)
        {
            temp[c] = Group[c];
        }
        Group = temp;

    }

//    private void OnEnable()
//    {
//        SceneManager.sceneLoaded += OnSceneLoaded;
//    }

//    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    {
//        setPlayers();
//    }

    private bool setPlayers()
    {
        if (GameObject.Find("Husband Stats") != null && GameObject.Find("Artist Stats") != null)
        {
            players[0] = GameObject.Find("Husband Stats").GetComponent<CharacterFightScene>();
            players[1] = GameObject.Find("Artist Stats").GetComponent<CharacterFightScene>();
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
		disableBlankButtons ();
        if (currentPlayerPointer != null && hasSetPlayers)
        {
            currentPlayerPointer.transform.position = new Vector3(currentCharacter.GetPointerLoc().position.x, currentCharacter.GetPointerLoc().position.y + (Mathf.PingPong(Time.time / 2, 0.6f)), currentCharacter.GetPointerLoc().position.z);
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


        if (currentStateCounter == 0) 		//Base
        {
            displayState0UI();
        }
        else if (currentStateCounter == 1) 	//Weapon Skill Overlay
        {
            displayState1UI();
        }
        else if (currentStateCounter == 2) 	//SkillChosen
        {
            displayState2UI();
        }
        else if (currentStateCounter == 3) 	//Bag Overlay
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
			if (i == 1)
			{
				setUITo(1);
			}
			else if (i == 2)
			{
				//Open Bag
				setUITo(3);
			}
        }
    }

    public void detailButtonClicked(int i)
    {
		if (isButtonActive [i] == false) {
			return;
		} else {
			if (currentStateCounter == 1) {
				if (i == 0) {
					setUITo (i);
				} else {
					//Deal damage to corresponding character
					currentSkillToBeDealt = currentCharacter.GetWeapon ().weaponAbility (i - 1);
					setUITo (2);

					//Show Weapon Detail
					overlayWeaponDetailConnector [i - 1].SetActive (true);
				}
			} else if (currentStateCounter == 3 || currentStateCounter == 4) {
				if (i == 0) {
					setUITo (i);
				} else {
					//Select chosen item
					print("Item: " + i.ToString() + " Chosen");
					if (playerInventory.At (i - 1).isTargetable ()) {
						currentConsumableToUse = playerInventory.At (i - 1);
						currentConsumableInt = i - 1;
						setUITo (4);

					} else {
						playerInventory.At (i - 1).SetTarget (currentCharacter);

						playerInventory.At (i - 1).Use ();
						if (!playerInventory.At (i - 1).hasUsesLeft ()) {
							playerInventory.Remove (i-1);
						}
						setInventoryImages ();

						startNextTurn ();
					}
				}
			}
		}
    }

    public void enemyButtonClicked(int i)
    {
		if (isButtonActive [i] == false) {
			return;
		} else {
			if (currentStateCounter == 2) {
				print ("State 2: Button " + i + " Pressed.");
				if (i == 0) {
					setUITo (0);
				} else {
					currentCharacter.attackAnimation ();
					//print ("damageDealt: " + Mathf.CeilToInt (currentSkillToBeDealt.getDamage () * currentCharacter.GetDamageMultiplier ()).ToString ());
					enemies [i - 1].takeDamage (Mathf.CeilToInt (currentSkillToBeDealt.getDamage () * currentCharacter.GetDamageMultiplier ()));
					currentSkillToBeDealt = null;
					if (enemies [i - 1].isDead ()) {
						enemies [i - 1].deathAnimation ();
					}
					startNextTurn ();
				}
			} else if (currentStateCounter == 4) {
				currentConsumableToUse.SetTarget (enemies[i - 1]);
				currentConsumableToUse.Use ();
				if (!currentConsumableToUse.hasUsesLeft ()) {
					playerInventory.Remove (currentConsumableInt);
				}
				setInventoryImages ();
				startNextTurn ();
			}
		}
    }

    public void onMouseHoverButton(int i)
    {

    }

    public void onMouseExit()
    {

    }


    private void displayState0UI()
    {
        print("displayState0UI");
        overlayWeapon.SetActive(false);
        overlayBag.SetActive(false);
        for (int i = 0; i < overlayWeaponDetailConnector.Length; i++)
        {
            overlayWeaponDetailConnector[i].SetActive(false);
        }
        for (int i = 0; i < enemiesColliders.Length; i++)
        {
            enemiesColliders[i].enabled = false;
        }
        currentSkillToBeDealt = null;
		currentConsumableToUse = null;
        overlayWeaponDetail.SetActive(false);
        buttonImage[0].sprite = FindIcon("blank");
        buttonImage[1].sprite = FindIcon("attack");
        buttonImage[2].sprite = FindIcon("backpack");
    }

    private void displayState1UI()
    {
        print("displayState1UI");

		overlayBag.SetActive(false);

        overlayWeapon.SetActive(true);
        buttonImage[0].sprite = FindIcon("return");
        for (int i = 0; i < currentCharacter.GetWeapon().numAbilities(); i++)
        {
            weaponButtonImage[i].sprite = currentCharacter.GetWeapon().weaponAbility(i).getSkillIcon().GetSprite();
            overlayWeaponDetailTab[i].SetActive(true);
        }
        for (int i = currentCharacter.GetWeapon().numAbilities() + 1; i < weaponButtonImage.Length; i++)
        {
            weaponButtonImage[i].sprite = FindIcon("blank");
            overlayWeaponDetailTab[i].SetActive(false);
        }
    }


    private void displayState2UI()
    {
        print("displayState2UI");

		overlayBag.SetActive (false);

        overlayWeaponDetail.SetActive(true);
        for (int j = 0; j < enemiesColliders.Length; j++)
        {
            enemiesColliders[j].enabled = true;
        }
    }


    private void displayState3UI()
    {
		print ("displayState3UI");
		overlayWeapon.SetActive (false);
		overlayWeaponDetail.SetActive (false);
		buttonImage[0].sprite = FindIcon("return");

		overlayBag.SetActive (true);
		setInventoryImages ();

    }

	private void setInventoryImages() {
		if (playerInventory != null) {
			for (int i = 0; i < itemSlots.Length; i++) {
				if (playerInventory.consumableInventory [i] != null) {
					itemSlots [i].SetItemImage (playerInventory.consumableInventory [i].GetImage ());
					itemSlots [i].SetColor (playerInventory.consumableInventory [i].GetColorType ());
				} else {
					itemSlots [i].SetItemImage (FindIcon("blank"));
					itemSlots [i].SetColor (FindIcon("greyCircle"));
				}
			}
		}
	}

    private void displayState4UI()
    {
		for (int j = 0; j < enemiesColliders.Length; j++)
		{
			enemiesColliders[j].enabled = true;
		}
    }

    private void disableBlankButtons()
    {
        if (currentStateCounter == 1) //WeaponSelect
        {
			isButtonActive [0] = true;
            for (int i = 0; i < weaponButtonImage.Length; i++)
            {
                if (weaponButtonImage[i].sprite == FindIcon("blank"))
                {
					isButtonActive[i+1] = false;
                }
                else
                {
                    isButtonActive[i+1] = true;
                }
            }
        }
		else if (currentStateCounter == 3) //ConsumableSelect
		{
			isButtonActive [0] = true;
			for (int i = 0; i < itemSlots.Length; i++) {
				if (itemSlots [i].GetItemSprite () == FindIcon ("blank")) {
					isButtonActive [i+1] = false;
				} else {
					isButtonActive [i+1] = true;
				}
			}
		}
        else
        {
            for (int i = 0; i < buttonImage.Length; i++) //Base
            {
                if (buttonImage[i].sprite == FindIcon("blank"))
                {
                    isButtonActive[i] = false;
                }
                else
                {
                    isButtonActive[i] = true;
                }
            }
        }
    }

    private void setUITo(int i)
    {
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

			SceneManager.LoadScene ("GameOver");

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

				//Spawn Item/Weapon Drop Popup
				if (weaponToGive != null) {
					//WeaponSwapPopUp
				} else if (consumableToGive != null) {
					playerInventory.Add (consumableToGive);
				}


                spawnContinueButton();

            }
            else
            {
                overlayWeapon.SetActive(false);
                overlayBag.SetActive(false);
                currentStateCounter = 0;
                currentTurnCounter = (currentTurnCounter + 1) % allCharacters.Length;
                currentCharacter = turnOrder[currentTurnCounter];

				currentCharacter.turnTick (); //For tempStats

                if (currentCharacter.isDead())
                {
                    startNextTurn();
                }
                else
                {
                    if (currentCharacter.gameObject.tag == "Enemy")
                    {
                        isEnemy = true;
                        overlayWeapon.SetActive(false);
                        overlayBag.SetActive(false);
                        for (int i = 0; i < buttonImage.Length; i++)
                        {
                            buttonImage[i].sprite = FindIcon("blank");
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
        overlayWeapon.SetActive(false);
        overlayBag.SetActive(false);
        for (int i = 0; i < buttonImage.Length; i++)
        {
            buttonImage[i].sprite = FindIcon("blank");
        }
        continueButton.SetActive(true);
    }

    public void continueButtonClicked()
    {
        continueButton.SetActive(false);
		StartCoroutine (fadeOut());
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
        yield return new WaitForSeconds(2.5f);
        int playerToAtk = Random.Range(0, players.Length);
        
        //Damage Chargable

        if (currentCharacter.GetWeapon().weaponAbility(0).GetIsChargable())
        {
            if (currentCharacter.GetWeapon().weaponAbility(0).GetIsChargedUp())
            {
                dealDamage(playerToAtk);
                currentCharacter.GetWeapon().weaponAbility(0).SetIsChargedUp(false);
            }
            else
            {
                currentCharacter.GetWeapon().weaponAbility(0).SetIsChargedUp(true);
            }
        }
        else
        {
            dealDamage(playerToAtk);
        }

        yield return new WaitForSeconds(1.2f);

        currentCharacter.SetActiveTbox(false);

        print("currentPlayer dealt" + currentCharacter.GetWeapon().weaponAttack(0) + " damage.");
        if (players[playerToAtk].isDead())
        {
            players[playerToAtk].deathAnimation();
        }
        yield return new WaitForSeconds(0.25f);
        startNextTurn();
        yield return 0;
    }

    private void dealDamage(int playerToAtk)
    {
		players[playerToAtk].takeDamage(Mathf.RoundToInt(currentCharacter.GetWeapon().weaponAttack(0) * currentCharacter.GetDamageMultiplier()));
        currentCharacter.AttackAnim();
        currentCharacter.SetActiveTbox(true);
        //Set Text
        currentCharacter.GetTboxText().text = "Don't you have homework to do?";
    }

    private CharacterFightScene[] sortToSpeed(CharacterFightScene[] input)
    {
        CharacterFightScene temp;
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
			for (int i = 0; i < UI_Icons.GetLength(); i++)
            {
				if (UI_Icons.At(i).GetName() == _name)
                {
					return UI_Icons.At(i).GetSprite();
                }
            }
			return UI_Icons.At(0).GetSprite();
        }
        else
        {
			return UI_Icons.At(0).GetSprite();
        }
    }

}
