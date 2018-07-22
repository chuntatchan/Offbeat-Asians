using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
	[SerializeField]
	public string weaponName;
    [SerializeField]
    private string weaponType;
    [SerializeField]
    private bool isBreakable;
    [SerializeField]
    private Skill[] weaponAbilities;

    public int numAbilities()
    {
		return weaponAbilities.Length;
    }

    public Skill weaponAbility(int i)
    {
        return weaponAbilities[i-1];
    }

    public int weaponAttack(int i)
    {
        return weaponAbilities[i].getDamage();
    }

	public WeaponType Wtype;

}

public enum WeaponType {
	none, axe, sword, whatever
}
