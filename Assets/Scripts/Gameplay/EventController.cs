using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject eventOverlay;
	[SerializeField]
	private GameObject fadeScreen;
    [SerializeField]
    private Slider[] playerHPSliders;
    [SerializeField]
    private CharacterFightScene[] playerStats;
    [SerializeField]
    private TMP_Text questionText, promptText;
    [SerializeField]
    private TMP_Text[] buttonsText;
    [SerializeField]
    private Image picture;
    [SerializeField]
    private GameObject[] _buttons;
	[SerializeField]
	private WeaponExchanger _weaponExchanger;
	private bool isWeaponExchangerActive;
    
	[SerializeField]
	public WeaponList weapons;

    [Space]

    [Header("Event")]

    [SerializeField]
    private EventText[] eventTexts;

    private int activeCharacter;

    private int currentStateCounter = 0;

    [Space]

    [Header("Static")]
    [SerializeField]
    private LevelManager levelManager;
	[SerializeField]
	private Inventory inventory;

    // Use this for initialization
    void Start()
    {
		StartCoroutine (fadeIn());
        if (_weaponExchanger  != null)
        {
            _weaponExchanger.gameObject.SetActive(false);
        }
        else
        {
            print("_weaponExchanger is not referenced.");
        }
		isWeaponExchangerActive = false;
        if (GameObject.FindGameObjectWithTag("Player1") != null)
        {
            playerStats[0] = GameObject.FindGameObjectWithTag("Player1").GetComponent<CharacterFightScene>();
        }
        if (GameObject.FindGameObjectWithTag("Player2") != null)
        {
            playerStats[1] = GameObject.FindGameObjectWithTag("Player2").GetComponent<CharacterFightScene>();
        }
        if (GameObject.FindGameObjectWithTag("levelManager") != null)
        {
            levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        }
		if (GameObject.FindGameObjectWithTag ("Inventory") != null) {
			inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory>();
		}

        currentStateCounter = 0;
        if (GameObject.FindGameObjectWithTag("Player1") != null || GameObject.FindGameObjectWithTag("Player2") != null)
        {
            for (int i = 0; i < playerStats.Length; i++)
            {
                playerHPSliders[i].maxValue = playerStats[i].GetMaxHealth();
                playerHPSliders[i].minValue = 0;
                playerHPSliders[i].value = playerStats[i].GetHealth();
            }
        }
        if (eventTexts[0].GetPicture() != null)
        {
            picture.sprite = eventTexts[0].GetPicture();
        }
        setUITo(0);
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

    public void onButtonClick(int i)
    {
		if (!isWeaponExchangerActive) {
			if (currentStateCounter == 0) {
				activeCharacter = i;
				currentStateCounter = 1;
				setUITo (1);
			} else if (currentStateCounter == 1) {
				if (i == 0) {
					setUITo (2);
				} else {
					setUITo (3);
				}
				//Change Stats
				if (playerStats [activeCharacter] != null) {
					print("changing Stats");
					print ("changeMaxHP: " + eventTexts [1].GetEventOption (i).changeMaxHP);
					if (eventTexts [1].GetEventOption (i).changeMaxHP) {
						print ("Player maxHP set to: " + (playerStats[activeCharacter].GetMaxHealth() + eventTexts [1].GetEventOption (i).GetMaxHPChange ()).ToString());
						playerStats [activeCharacter].SetMaxHealth (playerStats[activeCharacter].GetMaxHealth() + eventTexts [1].GetEventOption (i).GetMaxHPChange ());
						if (playerStats [activeCharacter].GetMaxHealth () < playerStats [activeCharacter].GetHealth ()) {
							playerStats [activeCharacter].SetHealth (playerStats [activeCharacter].GetMaxHealth ());
						}
					}
					print ("changeHP: " + eventTexts [1].GetEventOption (i).changeHP);
					if (eventTexts [1].GetEventOption (i).changeHP) {
						print ("changingHP");
						playerStats [activeCharacter].takeDamage (-eventTexts [1].GetEventOption (i).GetHPChange ());
						if (eventTexts [1].GetEventOption (i).GetHPChange () > 0) {
							print ("Player" + activeCharacter + " heal: " + eventTexts [1].GetEventOption (i).GetHPChange ());
						} else {
							print ("Player" + activeCharacter + " take damage: " + -eventTexts [1].GetEventOption (i).GetHPChange ());
						}
					}
					print ("changeFinesse: " + eventTexts [1].GetEventOption (i).changeFinesse);
					if (eventTexts [1].GetEventOption (i).changeFinesse) {
						print ("Player finesse set to: " + (playerStats [activeCharacter].GetFinesse () + eventTexts [1].GetEventOption (i).finesseChange).ToString());
						playerStats [activeCharacter].SetFinesse (playerStats [activeCharacter].GetFinesse () + eventTexts [1].GetEventOption (i).finesseChange);
					}
					print ("changeArmour: " + eventTexts [1].GetEventOption (i).changeArmour);
					if (eventTexts [1].GetEventOption (i).changeArmour) {
						print ("changingArmour: +" + eventTexts [1].GetEventOption (i).armourChange);
						playerStats [activeCharacter].SetArmour (playerStats [activeCharacter].GetArmour () + eventTexts [1].GetEventOption (i).armourChange);
					}
					if (eventTexts [1].GetEventOption (i).weaponToGive != WeaponType.none) {
						_weaponExchanger.gameObject.SetActive (true);
						_weaponExchanger.SetWeaponToGive (weapons.GetWeapon(eventTexts [1].GetEventOption (i).weaponToGive));
						isWeaponExchangerActive = true;
					}
					if (eventTexts [1].GetEventOption (i).consumableToGive != ConsumableType.none) {
						//Add Item to Bag
						print(eventTexts [1].GetEventOption (i).consumableToGive.ToString());
						inventory.Add(eventTexts [1].GetEventOption (i).consumableToGive);
					}
				}

				currentStateCounter = 2;
			} else if (currentStateCounter == 2) {
				//Transition to next Scene;
				StartCoroutine(fadeOut());
			}
		} else { //WeaponExchanger Is Active
			if (i == 2) {
				_weaponExchanger.gameObject.SetActive (false);
				isWeaponExchangerActive = false;
			} else {
				WeaponStats tempWeapon = playerStats [i].GetWeapon ();
				playerStats [i].SetWeapon(_weaponExchanger.GetWeaponToGive ());
				_weaponExchanger.SetWeaponToGive(tempWeapon);
				_weaponExchanger.SetCharacterCurrentWeaponText (playerStats[i].GetWeapon().GetWeaponType().ToString(), i);
				_weaponExchanger.SetWeaponDescription (tempWeapon.weaponDescription);
				_weaponExchanger.SetWeaponIcon (tempWeapon.GetWeaponIconForTransition());
			}
		}
    }

    private void setUITo(int i)
    {
        if (i == 0)
        {
            for (int j = 0; j < playerHPSliders.Length; j++)
            {
                playerHPSliders[j].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int j = 0; j < playerHPSliders.Length; j++)
            {
                playerHPSliders[j].gameObject.SetActive(false);
            }
        }

        if (eventTexts[i].GetPicture() != null) {
            picture.sprite = eventTexts[i].GetPicture();
        }

        questionText.text = eventTexts[i].GetEventText();
        promptText.text = eventTexts[i].GetEventPrompt();
        if (eventTexts[i].isContinue())
        {
            _buttons[1].SetActive(false);
            buttonsText[0].text = "Continue";
        }
        else
        {
            for (int j = 0; j < _buttons.Length; j++)
            {
                buttonsText[j].text = eventTexts[i].GetEventOption(j).GetOptionText();
            }
        }

    }
}
