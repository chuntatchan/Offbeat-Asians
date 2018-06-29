using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerFighting : MonoBehaviour
{

    [SerializeField]
    private CharacterStats[] allCharacters, enemies;
    [SerializeField]
    private GameObject overlay;
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


    // Use this for initialization
    void Start()
    {
        //Set currentTurnCounter & currentStateCounter to 0 & set all buttons except button[0] to active
        currentTurnCounter = 0;
        currentStateCounter = 0;
        overlay.SetActive(false);
        isButtonActive = new bool[button.Length];
        displayState0UI();
        //Sort allCharacters to turnOrder.
        turnOrder = sortToSpeed(allCharacters);
        //Set currentCharacter to turnOrder[currentTurnCounter]
        currentCharacter = turnOrder[currentTurnCounter];
        if (currentCharacter.gameObject.tag == "Enemy")
        {
            isEnemy = true;
        }
        //Start Game (Animation Stuff?)

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
        if (currentStateCounter == 0)
        {
            if (i != 0)
            {
                setUITo(i);
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
                currentSkillToBeDealt = currentCharacter.GetWeapon().weaponAbility(i);
                setUITo(2);
            }
        }
        else if (currentStateCounter == 2)
        {
            if (i == 0)
            {
                setUITo(i);
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

    private void displayState0UI()
    {
        print("displayState0UI");
        button[0].sprite = FindIcon("blank");
        button[1].sprite = FindIcon("attack");
        button[2].sprite = FindIcon("defend");
        //		button [3] = FindIcon ("bag");
    }

    private void displayState1UI()
    {
        print("displayState1UI");
        button[0].sprite = FindIcon("return");
        for (int i = 1; i <= currentCharacter.GetWeapon().numAbilities(); i++)
        {
            button[i].sprite = currentCharacter.GetWeapon().weaponAbility(i).getSkillIcon().GetSprite();
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
        for (int i = 1; i < overlay_button.Length; i++)
        {
            overlay_button[i].sprite = currentSkillToBeDealt.getSkillIconSprite();
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

    private void setUITo(int i)
    {
        currentStateCounter = i;
        changeUI();
    }

    private void startNextTurn()
    {
        overlay.SetActive(false);
        currentStateCounter = 0;
        currentTurnCounter = (currentTurnCounter + 1) % allCharacters.Length;
        currentCharacter = turnOrder[currentTurnCounter];
        if (currentCharacter.gameObject.tag == "Enemy")
        {
            isEnemy = true;
        }
        else
        {
            isEnemy = false;
        }
        displayState0UI();
    }

    private CharacterStats[] sortToSpeed(CharacterStats[] input)
    {
        CharacterStats temp;
        for (int write = 0; write < input.Length; write++)
        {
            for (int sort = 0; sort < input.Length - 1; sort++)
            {
                if (input[sort].GetSpeed() < input[sort + 1].GetSpeed())
                {
                    temp = input[sort + 1];
                    input[sort + 1] = input[sort];
                    input[sort] = temp;
                }
            }
        }
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
