using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventOptionStats: MonoBehaviour {

    public string optionText;

    public bool changeHP;
    public int HPChange = 0;

    public bool changeMaxHP;
    public int maxHPChange = 0;

    public bool changeFinesse;
    public int finesseChange = 0;

    public bool changeArmour;
    public int armourChange = 0;

	public WeaponType weaponToGive;

	public ConsumableType consumableToGive;


    public int GetHPChange()
    {
        return HPChange;
    }

    public int GetMaxHPChange()
    {
        return maxHPChange;
    }

    public int GetFinesseChange()
    {
        return finesseChange;
    }

    public int GetArmourChange()
    {
        return armourChange;
    }

    public string GetOptionText()
    {
        return optionText;
    }

	public WeaponType GetWeaponTypeToGive()
	{
		return weaponToGive;
	}

	public ConsumableType GetConsumableTypeToGive()
	{
		return consumableToGive;
	}
		
}