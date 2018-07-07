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
    
    [Space]

    [Header("Event")]
    [SerializeField]
    private int[] damage;

    private int activeCharacter;

    private int currentStateCounter = 0;

    // Use this for initialization
    void Start()
    {
        currentStateCounter = 0;
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerHPSliders[i].maxValue = playerStats[i].GetMaxHealth();
            playerHPSliders[i].minValue = 0;
            playerHPSliders[i].value = playerStats[i].GetHealth();
        }
        //Change Slider's Text to be the actual player's name
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onButtonClick(int i)
    {

        if (currentStateCounter == 0)
        {
            playerStats[i].takeDamage(damage[i]);
        }
        else if (currentStateCounter == 1)
        {
            eventOverlay.SetActive(false);
        }
        nextState();
    }

    private void nextState()
    {
        currentStateCounter++;
        setUITo(currentStateCounter);
    }

    private void setUITo(int i)
    {
        if (i == 1)
        {
            for (int j = 0; j < playerHPSliders.Length; j++)
            {
                playerHPSliders[j].gameObject.SetActive(false);
            }
            questionText.text = "You got lectured pretty badly. You promised them that it won't happen again. You actually feel bad, but then you wonder how long that's going to last.";
            promptText.text = "At least you got the money.";
            _buttons[1].SetActive(false);
            buttonsText[0].text = "Continue";
        }
    }
}
