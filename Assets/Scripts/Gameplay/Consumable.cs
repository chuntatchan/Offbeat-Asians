using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour {

	[Header("UI")]

	[SerializeField]
	private Sprite consumableImage;

	[SerializeField]
	private Sprite consumableColorType;

	[SerializeField]
	private Rarity rarity;

	[Space]

	[Header("StatChanges")]


	private CharacterFightScene target;
	[SerializeField]
	private int HPChange;
	[SerializeField]
	private bool targetable;
	[SerializeField]
	private int ArmourChange;
	[SerializeField]
	private int FinesseChange;
	[SerializeField]
	private int duration;
	[SerializeField]
	private bool stun;
	[SerializeField]
	private int usesLeft = 1;
	[SerializeField]
	private float damageMultiplier = 1f;
	[SerializeField]
	private float damageReductionMultiplier = 1f;
	[SerializeField]
	private Sprite[] usedSprites;


	public Sprite GetColorType() {
		return consumableColorType;
	}

	public Sprite GetImage() {
		return consumableImage;
	}

	public Rarity GetRarity() {
		return rarity;
	}

	public bool isTargetable() {
		return targetable;
	}

	public void SetTarget(CharacterFightScene _target) {
		target = _target;
	}

	public void Use() {
		if (target != null) {
			target.AddTempStats (HPChange, ArmourChange, FinesseChange, damageMultiplier, damageReductionMultiplier, duration);
		}
		usesLeft--;
		if (usesLeft > 0) {
			consumableImage = usedSprites [usesLeft];
		}
	}

	public bool hasUsesLeft() {
		return (usesLeft > 0);
	}

	/*
	public int GetHPChange() {
		return HPChange;
	}

	public int GetArmourChange() {
		return ArmourChange;
	}

	public int GetFinesseChange() {
		return FinesseChange;
	}

	public int GetDuration() {
		return duration;
	}

	public bool isStun() {
		return stun;
	}
		
	public int GetUsesLeft() {
		return usesLeft;
	}

	public float GetDamageMultiplier() {
		return damageMultiplier;
	}

	public float GetDamageReductionMultiplier() {
		return damageReductionMultiplier;
	}
	*/



}

public enum ConsumableType {
	none, boba, candy, cookie, wallet, fidgetSpinner, dimSum, glasses, fishSauce
}

public enum Rarity {
	Common, Uncommon, Rare, Legendary
}
