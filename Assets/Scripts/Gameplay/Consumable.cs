using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour {

	public ConsumableType _consumableType;

}

public enum ConsumableType {
	none, boba, candy, cookie, wallet, fidgetSpinner, dimSum, glasses, fishSauce
}
