using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	public Consumable[] consumableInventory;
	public ItemsList itemList;

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
	}

	public void Add (Consumable newConsumable)
	{
		for (int i = 0; i < consumableInventory.Length; i++) {
			if (consumableInventory [i] == null) {
				consumableInventory [i] = newConsumable;
				return;
			}
		}
	}

	public void Add (ConsumableType newConsumable) {

		Consumable _consumable = itemList.Find (newConsumable);
		print (_consumable.name);

		for (int i = 0; i < consumableInventory.Length; i++) {
			if (consumableInventory [i] == null) {
				consumableInventory [i] = _consumable;
				return;
			}
		}
	}

	public Consumable At(int i) {
		return consumableInventory [i];
	}

	public void Remove (int i)
	{
		consumableInventory [i] = null;
		for (int j = i + 1; j < consumableInventory.Length; j++) {
			if (consumableInventory [j] != null) {
				consumableInventory [j - 1] = consumableInventory [j];
				consumableInventory [j] = null;
			}
		}
	}

}
