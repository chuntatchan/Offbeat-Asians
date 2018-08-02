using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{

	public WeaponType _weapontype;
	public string weaponDescription;
    [SerializeField]
    private Skill[] weaponAbilities;
	[SerializeField]
	private int damageReflect = 0;
	[SerializeField]
	private Sprite weaponIconForTransition;


    public int numAbilities()
    {
		return weaponAbilities.Length;
    }

    public Skill weaponAbility(int i)
    {
        return weaponAbilities[i];
    }

    public int weaponAttack(int i)
    {
        return weaponAbilities[i].getDamage();
    }

	public WeaponType GetWeaponType() {
		return _weapontype;
	}

	public Sprite GetWeaponIconForTransition() {
		return weaponIconForTransition;
	}

}

public enum WeaponType {
	none, speak, pillow, guitar, reportCard
}
