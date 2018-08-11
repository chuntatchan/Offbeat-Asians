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

    [Header("LevelManager")]
    [SerializeField]
    private LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
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
					if (eventTexts [i].GetEventOption (i).changeHP) {
						playerStats [activeCharacter].takeDamage (-eventTexts [i].GetEventOption (i).GetHPChange ());
					}
					if (eventTexts [i].GetEventOption (i).changeMaxHP) {
						playerStats [activeCharacter].SetMaxHealth (eventTexts [i].GetEventOption (i).GetMaxHPChange ());
					}
					if (eventTexts [i].GetEventOption (i).changeFinesse) {
						playerStats [activeCharacter].SetFinesse (playerStats [activeCharacter].GetFinesse () + eventTexts [i].GetEventOption (i).finesseChange);
					}
					if (eventTexts [i].GetEventOption (i).changeArmour) {
						playerStats [activeCharacter].SetArmour (playerStats [activeCharacter].GetArmour () + eventTexts [i].GetEventOption (i).armourChange);
					}
					if (eventTexts [i].GetEventOption (i).weaponToGive != WeaponType.none) {
						_weaponExchanger.gameObject.SetActive (true);
						_weaponExchanger.SetWeaponToGive (weapons.GetWeapon(eventTexts [i].GetEventOption (i).weaponToGive));
						isWeaponExchangerActive = true;
					}
					if (eventTexts [i].GetEventOption (i).consumableToGive != ConsumableType.none) {
						//Add Item to Bag
					}
				}

				currentStateCounter = 2;
			} else if (currentStateCounter == 2) {
				//Transition to next Scene;
				levelManager.LoadNextScene ();
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
