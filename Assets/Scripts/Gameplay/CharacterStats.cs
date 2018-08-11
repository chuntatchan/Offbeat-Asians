using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	[SerializeField]
	private int maxHealth, health, armour, speed, finesse;

	public int GetMaxHealth() {
		return maxHealth;
	}

	public void SetMaxHealth(int i) {
		maxHealth = i;
	}

	public int GetHealth() {
		return health;
	}

	public void SetHealth(int i) {
		health = i;
	}

	public int GetArmour() {
		return armour;
	}

	public void SetArmour(int i) {
		armour = i;
	}

	public int GetSpeed() {
		return speed;
	}

	public void SetSpeed(int i) {
		speed = i;
	}

	public int GetFinesse() {
		return finesse;
	}

	public void SetFinesse(int i) {
		finesse = i;
	}

}
