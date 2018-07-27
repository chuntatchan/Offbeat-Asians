using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour {

	[SerializeField]
	private weaponContainer[] weapons;

	[System.Serializable]
	public struct weaponContainer
	{
		public WeaponType type;
		public WeaponStats weaponPrefab;
	}

	public WeaponStats GetWeapon(WeaponType _type) {
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons [i].type == _type) {
				return weapons [i].weaponPrefab;
			}
		}
		return null;
	}

}
