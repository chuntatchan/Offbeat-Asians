using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{

    [SerializeField]
    private string skillDescription;
    [SerializeField]
    private int maxDamage, numRolls;

    [SerializeField]
    private bool procsBleeding;
    [SerializeField]
    private float accuracyModifier;
    [SerializeField]
    private UI_Icon skillIcon;

    public int getDamage()
    {
        //We can fix this later (dice roll like For The King?)
        return maxDamage;
    }

    public UI_Icon getSkillIcon()
    {
        return skillIcon;
    }

    public Sprite getSkillIconSprite()
    {
        return skillIcon.GetSprite();
    }

	public string getDescription() {
		return skillDescription;
	}

}
