using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponExchanger : MonoBehaviour {

	[SerializeField]
	private Image weaponIcon;
	[SerializeField]
	private TMP_Text weaponDescription;
	[SerializeField]
	private TMP_Text[] characterCurrentWeapon;
	[SerializeField]
	private WeaponStats weaponToGive;

	// Use this for initialization
	void Start () {
		
	}

	public void SetCharacterCurrentWeaponText(string weaponName, int i) {
	
		characterCurrentWeapon[i].text = "Current Weapon:\n" + weaponName;
		
	}

	public void SetWeaponIcon(Sprite _sprite) {
		weaponIcon.sprite = _sprite;
	}

	public void SetWeaponDescription(string _text) {
		weaponDescription.text = _text;
	}

	public void SetWeaponToGive(WeaponStats n_WeaponToGive) {
		weaponToGive = n_WeaponToGive;
	}

	public WeaponStats GetWeaponToGive() {
		return weaponToGive;
	}

}
