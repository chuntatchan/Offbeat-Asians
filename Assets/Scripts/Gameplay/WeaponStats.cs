using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{

	public WeaponType _weapontype;

    [SerializeField]
    private Skill[] weaponAbilities;
	[SerializeField]
	private int damageReflect = 0;


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

}

public enum WeaponType {
	none, speak, pillow, guitar, reportCard
}
