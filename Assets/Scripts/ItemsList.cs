using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour {

	public WeaponList allWeapons;
	public consumableContainer[] allConsumables;

	[Space]

	public consumableContainer[] allLegendaryConsumables;
	public consumableContainer[] allRareConsumables;
	public consumableContainer[] allUncommonConsumables;
	public consumableContainer[] allCommonConsumables;

	[System.Serializable]
	public struct consumableContainer
	{
		public ConsumableType type;
		public Consumable consumablePrefab;
	}

	public Consumable Find(ConsumableType type) {
		for (int i = 0; i < allConsumables.Length; i++) {
			if (allConsumables [i].type == type) {
				return allConsumables [i].consumablePrefab;
			}
		}
		print ("Couldn't find type.");
		return null;
	}

}
