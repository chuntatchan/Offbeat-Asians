using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_IconList : MonoBehaviour {

	[SerializeField]
	private UI_Icon[] list;

	public UI_Icon At(int i) {
		return list [i];
	}

	public int GetLength() {
		return list.Length;
	}
}
