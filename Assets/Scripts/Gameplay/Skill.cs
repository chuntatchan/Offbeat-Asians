using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{

    [SerializeField]
    private string skillDescription;
    [SerializeField]
	private int damage;
    [SerializeField]
    private bool multiHit;
    [SerializeField]
    private float stunChance;
	[SerializeField]
	private int finesseBoost;
	[SerializeField]
	private int finesseBoostLength;
    [SerializeField]
    private UI_Icon skillIcon;

    public int getDamage()
    {
        //We can fix this later (dice roll like For The King?)
        return damage;
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

	public bool isMultiHit() {
		return multiHit;
	}

	public float getStunChance() {
		return stunChance;
	}

	public int getFinesseBoost() {
		return finesseBoost;
	}

	public int getFinesseBoostLength() {
		return finesseBoostLength;
	}

}
