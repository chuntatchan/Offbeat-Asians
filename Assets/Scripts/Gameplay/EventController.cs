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
    private CharacterStats[] playerStats;
    [SerializeField]
    private TMP_Text questionText, promptText;
    [SerializeField]
    private TMP_Text[] buttonsText;
    [SerializeField]
    private GameObject[] _buttons;
    
	[SerializeField]
	public weaponContainer[] weapons;

    [Space]

    [Header("Event")]

    [SerializeField]
    private EventText[] eventTexts;

	[System.Serializable]
	public struct weaponContainer
	{
		public WeaponType type;
		public GameObject weaponPrefab;
	}


    private int activeCharacter;

    private int currentStateCounter = 0;

    // Use this for initialization
    void Start()
    {
        playerStats[0] = GameObject.FindGameObjectWithTag("Player1").GetComponent<CharacterStats>();
        playerStats[1] = GameObject.FindGameObjectWithTag("Player2").GetComponent<CharacterStats>();

        currentStateCounter = 0;
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerHPSliders[i].maxValue = playerStats[i].GetMaxHealth();
            playerHPSliders[i].minValue = 0;
            playerHPSliders[i].value = playerStats[i].GetHealth();
        }
        //Change Slider's Text to be the actual player's name
    }

    public void onButtonClick(int i)
    {

        if (currentStateCounter == 0)
        {
            activeCharacter = i;
            currentStateCounter = 1;
            setUITo(1);
        }
        else if (currentStateCounter == 1)
        {
            if (i == 0)
            {
                setUITo(2);
            }
            else
            {
                setUITo(3);
            }
            if (eventTexts[i].GetEventOption(i).changeHP)
            {
                playerStats[activeCharacter].takeDamage(-eventTexts[i].GetEventOption(i).GetHPChange());
            }

        }
        else if (currentStateCounter == 2)
        {
            //Transition to next Scene;
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

        questionText.text = eventTexts[i].GetEventText();
        promptText.text = eventTexts[i].GetEventPrompt();
        for (int j = 0; j < _buttons.Length; j++)
        {
            buttonsText[j].text = eventTexts[i].GetEventOption(j).GetOptionText();
        }
        if (eventTexts[i].isContinue())
        {
            _buttons[1].SetActive(false);
            buttonsText[0].text = "Continue";
        }
    }
}
